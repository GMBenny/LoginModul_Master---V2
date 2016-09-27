﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginModul_Master
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoginModul.LoginModul lm = new LoginModul.LoginModul(new LoginModul.MsSqlLoginDataMapper("Persist Security Info = False; Integrated Security = true; Initial Catalog = Prod_data; Server = localhost"), new LoginModul.EMailCheck(LoginModul.Helper.chkEMail), new LoginModul.PasswordCheck(LoginModul.Helper.chkPassword), LoginModul.Helper.ErrorMessageEng);
            //string verificationCode = lm.CreateLogin("sletmig@eal.dk", "123456", "123456");
            //lm.ActivateUser("sletmig@eal.dk", verificationCode);
            //int i1 = lm.Login("sletmig@eal.dk", "123456");
            //lm.ChangePassword("sletmig@eal.dk", "123456", "GlPassword", "GlPassword");
            //int i2 = lm.Login("sletmig@eal.dk", "GlPassword");
            //string a = lm.InitiateResetPassword("sletmig@eal.dk");
            //lm.ResetPassword("sletmig@eal.dk", a, "123-445", "123-445");
            //int i3 = lm.Login("sletmig@eal.dk", "123-445");
            //lm.DeleteLogin("sletmig@eal.dk", "123-445");

            LoginModul.ILoginDataMapper ldm = new LoginModul.MsSqlLoginDataMapper("Persist Security Info = False; Integrated Security = true; Initial Catalog = FamilyPlanner; Server = localhost");
            LoginModul.ILoginLogDataMapper lldm = new LoginModul.MsSqlLoginLogDataMapper("Persist Security Info = False; Integrated Security = true; Initial Catalog = FamilyPlanner; Server = localhost");

            LoginModul.LoginModul lm = new LoginModul.LoginModul(ldm, lldm, new LoginModul.EMailCheck(LoginModul.Helper.chkEMail), new LoginModul.PasswordCheck(LoginModul.Helper.chkPassword), LoginModul.Helper.ErrorMessageDK);
            string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
            lm.ActivateUser("bejo@eal.dk", verificationCode);

            long userID1 = lm.Login("bejo@eal.dk", "uij54hgt");

            long userID2 = lm.Login("bejo@eal.dk", "uij54hgt");

            long userID3 = lm.Login("bejo@eal.dk", "uij54hgt");

            long userID4 = lm.Login("bejo@eal.dk", "uij54hgt");

            long userID5 = lm.Login("bejo@eal.dk", "123456");

            string newVerificationCode = lm.InitiateResetPassword("bejo@eal.dk");
            lm.ResetPassword("bejo@eal.dk", newVerificationCode, "NewPassword", "NewPassword");

            long userID = lm.Login("bejo@eal.dk", "NewPassword");

            lm.DeleteLogin("bejo@eal.dk", "NewPassword");
        }

    }
}