using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginModul
{
    public delegate void EMailCheck(string EMail, ErrorMessage errorMessage);
    public delegate void PasswordCheck(string password, ErrorMessage errorMessage);
    public delegate string ErrorMessage(int errNr);

    public class LoginModul
    {
        ILoginDataMapper _logindm;
        ILoginLogDataMapper _logdm;
        EMailCheck _emailChk;
        PasswordCheck _passwordChk;
        ErrorMessage _errorMessage;

        public LoginModul(ILoginDataMapper logindm, ILoginLogDataMapper logdm, EMailCheck emailChk, PasswordCheck passwordChk, ErrorMessage errorMessage)
        {
            _logindm = logindm;
            _logdm = logdm;
            _errorMessage = errorMessage;
            _emailChk = emailChk;
            _passwordChk = passwordChk;
        }
        public void ActivateUser(string email, string verificationCode)
        {
            Helper.CreateLogEntry(_logdm, "LoginModul.ActivateUser", "INFO", "Start");

            email = email.Trim().ToLower();
            verificationCode = verificationCode.Trim().ToUpper();

            Login l = _logindm.Read(email);
            if (l == null)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.ActivateUser", "USER", "EMail: " + email + " does not exists");

                throw new Exception(_errorMessage(1002));
            }
            if (l.IsStateConfirmMissing())
            {
                if (l.VerificationCode.Equals(verificationCode))
                {
                    l.UnconfirmedEMail = null;
                    l.ConfirmedEMail = email;
                    l.VerificationCode = null;
                    l.BadLogins = 0;
                    _logindm.Update(l);
                }
                else
                {
                    Helper.CreateLogEntry(_logdm, "LoginModul.ActivateUser", "USER", "Wrong VerificationCode: " + verificationCode + " on " + email);
                    throw new Exception(_errorMessage(4002));
                }
            }
            else
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.ActivateUser", "USER", "No VerificationCode on " + email);
                throw new Exception(_errorMessage(4001));
            }
            Helper.CreateLogEntry(_logdm, "LoginModul.ActivateUser", "INFO", "slut - " + email + " is activated with + " + verificationCode);
        }
        public void ResetPassword(string email, string verificationCode, string newPassword1, string newPassword2)
        {
            Helper.CreateLogEntry(_logdm, "LoginModul.ResetPassword", "INFO", "Start");

            email = email.Trim().ToLower();
            verificationCode = verificationCode.Trim().ToUpper();

            _emailChk(email, _errorMessage);
            Login l = _logindm.Read(email);
            if (l == null)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.ResetPassword", "USER", "EMail does not exists: " + email);
                throw new Exception(_errorMessage(1002));
            }
            if (l.IsStateResetPassword() == false)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.ResetPassword", "USER", "Not on password reset state: " + email);
                throw new Exception(_errorMessage(4003));
            }
            if (l.VerificationCode != verificationCode)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.ResetPassword", "USER", "Bad VerificationCode on: " + email);
                throw new Exception(_errorMessage(4004));
            }
            if (!newPassword1.Equals(newPassword2))
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.ResetPassword", "USER", "Passwords does not match on " + email);
                throw new Exception(_errorMessage(2002));
            }
            l.HashedPassword = Helper.HashText(newPassword1);
            l.VerificationCode = null;
            l.BadLogins = 0;
            _logindm.Update(l);

            Helper.CreateLogEntry(_logdm, "LoginModul.ResetPassword", "INFO", "Slut - " + email + " has new reset password");
        }
        public string InitiateResetPassword(string email)
        {
            Helper.CreateLogEntry(_logdm, "LoginModul.InitiateResetPassword", "INFO", "Start");

            email = email.Trim().ToLower();

            _emailChk(email, _errorMessage);
            Login l = _logindm.Read(email);
            if (l == null)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.InitiateResetPassword", "USER", "EMail does not exists: " + email);
                throw new Exception(_errorMessage(1002));
            }
            if (l.IsStateConfirmedUser() == false)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.InitiateResetPassword", "USER", "EMail is not a confirmed user: " + email);
                throw new Exception(_errorMessage(4001));
            }
            l.VerificationCode = Helper.GenerateVerificationCode(_logindm);
            l.HashedPassword = -1; // Can't login
            l.BadLogins = 0;
            _logindm.Update(l);

            Helper.CreateLogEntry(_logdm, "LoginModul.InitiateResetPassword", "INFO", "Slut - " + email + " is set in reset password state and " + l.VerificationCode + " returned");

            return l.VerificationCode;
        }
        public string CreateLogin(string email, string password1, string password2)
        {
            Helper.CreateLogEntry(_logdm, "LoginModul.CreateLogin", "INFO", "Start");

            email = email.Trim().ToLower();

            _emailChk(email, _errorMessage);
            _passwordChk(password1, _errorMessage);
            _passwordChk(password2, _errorMessage);
            if (!password1.Equals(password2))
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.CreateLogin", "USER", "Password does not match on " + email);
                throw new Exception(_errorMessage(2002));
            }
            Login l = _logindm.Read(email);
            if (l != null)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.CreateLogin", "USER", "EMail does already exist: " + email);
                throw new Exception(_errorMessage(1001));
            }
            long uid = Helper.GenerateNextUserID(_logindm);
            string verificationCode = Helper.GenerateVerificationCode(_logindm);
            _logindm.Create(new Login() { ConfirmedEMail = null, UnconfirmedEMail = email, HashedPassword = Helper.HashText(password1), UserID = uid, VerificationCode = verificationCode, BadLogins = 0 });

            Helper.CreateLogEntry(_logdm, "LoginModul.CreateLogin", "INFO", "Slut - " + email + " created and verificationCode = " + verificationCode + " is returned");

            return verificationCode;
        }
        public void DeleteLogin(string email, string password)
        {
            Helper.CreateLogEntry(_logdm, "LoginModul.DeleteLogin", "INFO", "Start");

            email = email.Trim().ToLower();

            _emailChk(email, _errorMessage);
            _passwordChk(password, _errorMessage);

            Login l = _logindm.Read(email);
            if (l == null)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.DeleteLogin", "USER", "EMail does not exists: " + email);
                throw new Exception(_errorMessage(1002));
            }
            if (Helper.HashText(password) != l.HashedPassword)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.DeleteLogin", "USER", "Not correct password on: " + email);
                throw new Exception(_errorMessage(2001));
            }
            else
            {
                _logindm.Delete(l);
            }

            Helper.CreateLogEntry(_logdm, "LoginModul.DeleteLogin", "INFO", "Slut - " + email + " is deleted");
        }
        public long Login(string email, string password)
        {
            Helper.CreateLogEntry(_logdm, "LoginModul.Login", "INFO", "Start");

            email = email.Trim().ToLower();

            _emailChk(email, _errorMessage);
            _passwordChk(password, _errorMessage);

            Login l = _logindm.Read(email);
            if (l == null)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.Login", "USER", "EMail does not exists: " + email);
                throw new Exception(_errorMessage(3001));
            }
            if (l.IsStateLockedUser() == true)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.Login", "USER", "Too many bad logins on: " + email);
                throw new Exception(_errorMessage(3002));
            }
            if (Helper.HashText(password) != l.HashedPassword)
            {
                l.BadLogins++;
                _logindm.Update(l);
                Helper.CreateLogEntry(_logdm, "LoginModul.Login", "USER", "Not correct password on: " + email);
                throw new Exception(_errorMessage(3001));
            }
            if (l.IsStateConfirmedUser() == false)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.Login", "USER", "EMail not confirmed: " + email);
                throw new Exception(_errorMessage(4002));
            }

            Helper.CreateLogEntry(_logdm, "LoginModul.Login", "INFO", "Slut - " + email + " logged in and UserID " + l.UserID + " returned");

            return l.UserID;
        }
        public void ChangePassword(string email, string oldPassword, string newPassword1, string newPassword2)
        {
            Helper.CreateLogEntry(_logdm, "LoginModul.ChangePassword", "INFO", "Start");

            email = email.Trim().ToLower();

            _emailChk(email, _errorMessage);
            _passwordChk(oldPassword, _errorMessage);
            _passwordChk(newPassword1, _errorMessage);
            _passwordChk(newPassword2, _errorMessage);

            Login l = _logindm.Read(email);
            if (l == null)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.ChangePassword", "USER", "EMail does not exist: " + email);
                throw new Exception(_errorMessage(1002));
            }
            if (l.IsStateConfirmedUser() == false)
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.ChangePassword", "USER", "EMail is not confirmed: " + email);
                throw new Exception(_errorMessage(4002));
            }
            if (!newPassword1.Equals(newPassword2))
            {
                Helper.CreateLogEntry(_logdm, "LoginModul.ChangePassword", "USER", "The two passwords does not match on: " + email);
                throw new Exception(_errorMessage(2002));
            }

            l.HashedPassword = Helper.HashText(newPassword1);
            _logindm.Update(l);

            Helper.CreateLogEntry(_logdm, "LoginModul.ChangePassword", "INFO", "Slut - " + email + " has changed password");
        }
    }
}
