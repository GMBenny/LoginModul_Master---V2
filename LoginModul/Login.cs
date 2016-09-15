using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginModul
{
    public class Login
    {
        public int UserID { get; set; }
        public string ConfirmedEMail { get; set; }
        public string UnconfirmedEMail { get; set; }
        public long HashedPassword { get; set; }
        public int BadLogins { get; set; }
        public string VerificationCode { get; set; }

        public bool IsStateLockedUser()
        {
            if (BadLogins >= 3)
                return true;
            else
                return false;
        }

        public bool IsStateConfirmMissing()
        {
            if ((ConfirmedEMail == null || ConfirmedEMail == "") && (UnconfirmedEMail != null || UnconfirmedEMail != "") && HashedPassword >= 0 && (VerificationCode != null || VerificationCode != ""))
                return true;
            else
                return false;
        }
        public bool IsStateConfirmedUser()
        {
            if ((ConfirmedEMail != null && ConfirmedEMail != "") && (UnconfirmedEMail == null || UnconfirmedEMail == "") && HashedPassword >= 0 && (VerificationCode == null || VerificationCode == ""))
                return true;
            else
                return false;
        }
        public bool IsStateResetPassword()
        {
            if ((ConfirmedEMail != null || ConfirmedEMail != "") && (UnconfirmedEMail == null || UnconfirmedEMail == "") && HashedPassword < 0 && (VerificationCode != null || VerificationCode != ""))
                return true;
            else
                return false;
        }
        public bool IsStateChangeEMail()
        {
            if ((ConfirmedEMail != null || ConfirmedEMail != "") && (UnconfirmedEMail != null || UnconfirmedEMail != "") && HashedPassword >= 0 && (VerificationCode != null || VerificationCode != ""))
                return true;
            else
                return false;
        }
    }
}
