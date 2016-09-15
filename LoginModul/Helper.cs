using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginModul
{
    public static class Helper
    {
        public static void CreateLogEntry(ILoginLogDataMapper logdm, string Location, string Severity, string Message)
        {
            if (logdm != null)
            {
                //try
                //{
                LoginLog ll = new LoginLog();
                ll.Location = Location;
                ll.LoginLogID = 1;
                ll.LogTime = System.DateTime.Now;
                ll.Message = Message;
                ll.Severity = Severity;

                logdm.Create(ll);
                //}
                //catch (Exception)
                //{
                //}
            }
        }

        public static void chkEMail(string EMail, ErrorMessage errorMessage)
        {
            if (EMail == null)
                throw new Exception(errorMessage(1003));
            if (EMail.Length < 5)
                throw new Exception(errorMessage(1003));
            if (EMail.Contains(' '))
                throw new Exception(errorMessage(1003));
            if (EMail.Contains(';'))
                throw new Exception(errorMessage(1003));
            if (EMail.Contains('@') == false)
                throw new Exception(errorMessage(1003));
            if (EMail.IndexOf('@') < 1)
                throw new Exception(errorMessage(1003));
            if (EMail.Substring(EMail.IndexOf('@')).Contains('.') == false)
                throw new Exception(errorMessage(1003));
            if (EMail.Substring(EMail.IndexOf('@')).IndexOf('.') < 2)
                throw new Exception(errorMessage(1003));
        }
        public static string GenerateVerificationCode(ILoginDataMapper _dm)
        {
            bool doesVerificationCodeExist;
            string v;
            do
            {
                v = System.Guid.NewGuid().ToString().ToUpper().Replace("-", "").Substring(0, 10);
                doesVerificationCodeExist = _dm.DoesVerificationCodeExist(v);
            } while (doesVerificationCodeExist == true);
            return v.ToUpper();
        }
        public static int GenerateNextUserID(ILoginDataMapper _dm)
        {
            int m = _dm.ReadMaxID();
            if (m < 0)
                m = 100;
            else
                m++;
            return m;
        }
        public static void chkPassword(string password, ErrorMessage errorMessage)
        {
            if (password == null)
                throw new Exception(errorMessage(2003));
            if (password.Length < 6)
                throw new Exception(errorMessage(2003));
        }
        public static long HashText(string text)
        {
            if (text == null || text.Length == 0)
                return 1;

            string data = text.Trim();
            // To store the result in
            long hashCode = 0;
            // Aim to use all 64 bit of the long
            int bitMoveInt = (int)(64 / data.Length);
            // Start with bit 0
            int bitMove = 0;
            // How many of the bits are used per character. Some bits are individual and
            // some are shared among neighbor characters: (64/chars+4)
            int modulus = (int)Math.Pow(2, (bitMoveInt + 4));

            // Loop through each character
            foreach (char c in data)
            {
                // Convert the character to a number
                long intc = (long)c;
                // Calculate the amount of bits to be used from the character
                intc = intc % modulus;
                // Right shift the bits to its designated position
                long newC = intc << bitMove;
                // XOR the data together with result
                hashCode ^= newC;
                // Calculate the next designated position
                bitMove += bitMoveInt;
            }

            // Return the result
            return hashCode;
        }
        public static string ErrorMessageDK(int error)
        {
            string retString = "";

            switch (error)
            {
                // Email fejl
                case 1001:
                    retString = "Email adressen findes allerede";
                    break;
                case 1002:
                    retString = "Email adressen findes ikke";
                    break;
                case 1003:
                    retString = "Fejl i EMail";
                    break;
                // Password fejl
                case 2001:
                    retString = "Forkert password";
                    break;
                case 2002:
                    retString = "De to passwords er ikke ens";
                    break;
                case 2003:
                    retString = "Password er for kort";
                    break;
                // Login fejl
                case 3001:
                    retString = "Forkert email/password";
                    break;
                case 3002:
                    retString = "Formange forkerte login forsøg";
                    break;
                // Aktiveringsfejl
                case 4001:
                    retString = "Bruger er aktiveret";
                    break;
                case 4002:
                    retString = "Bruger er ikke aktiveret";
                    break;
                case 4003:
                    retString = "Bruger er i password reset state";
                    break;
                case 4004:
                    retString = "Forkert aktiveringskode";
                    break;

            }

            return "Err" + error.ToString() + ": " + retString;
        }
        public static string ErrorMessageEng(int error)
        {
            string retString = "";

            switch (error)
            {
                // Email fejl
                case 1001:
                    retString = "Email already exists";
                    break;
                case 1002:
                    retString = "Email does not exists";
                    break;
                case 1003:
                    retString = "Bad EMail";
                    break;
                // Password fejl
                case 2001:
                    retString = "Bad password";
                    break;
                case 2002:
                    retString = "Password did not match";
                    break;
                case 2003:
                    retString = "Password is too short";
                    break;
                // Login fejl
                case 3001:
                    retString = "Wrong email/password";
                    break;
            }

            return "Err" + error.ToString() + ": " + retString;
        }
    }
}
