using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginModul_Test
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void I_LoginModul_PerfectTest()
        {
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new LoginModul.MsSqlLoginDataMapper("Persist Security Info = False; Integrated Security = true; Initial Catalog = Prod_data; Server = localhost"),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("sletmig@eal.dk", "123456", "123456");
                lm.ActivateUser("sletmig@eal.dk", verificationCode);
                long i1 = lm.Login("sletmig@eal.dk", "123456");
                lm.ChangePassword("sletmig@eal.dk", "123456", "GlPassword", "GlPassword");
                long i2 = lm.Login("sletmig@eal.dk", "GlPassword");
                string a = lm.InitiateResetPassword("sletmig@eal.dk");
                lm.ResetPassword("sletmig@eal.dk", a, "123-445", "123-445");
                long i3 = lm.Login("sletmig@eal.dk", "123-445");
                lm.DeleteLogin("sletmig@eal.dk", "123-445");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
            }
        }

        [TestMethod]
        public void I_LoginModul_UpperAndLowerCase()
        {
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new LoginModul.MsSqlLoginDataMapper("Persist Security Info = False; Integrated Security = true; Initial Catalog = Prod_data; Server = localhost"),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("slETmig@eal.dk", "123456", "123456");
                verificationCode = verificationCode.ToLower();
                lm.ActivateUser("sletMIg@eal.dk", verificationCode);
                long i1 = lm.Login("Sletmig@eal.dk", "123456");
                lm.ChangePassword("sletmiG@eal.dk", "123456", "GlPassword", "GlPassword");
                long i2 = lm.Login("sLetmIg@eal.dk", "GlPassword");
                string a = lm.InitiateResetPassword("slEtmIg@eal.dk");
                a = a.ToLower();
                lm.ResetPassword("sLETmig@eal.dk", a, "123-445", "123-445");
                long i3 = lm.Login("sleTMig@eal.dk", "123-445");
                lm.DeleteLogin("sletmIG@eal.dk", "123-445");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
            }
        }
        [TestMethod]
        public void I_LoginModul_UserActivate_TooManyBadLogins_Exception()
        {
            bool DidItThoughAnException = false;
            LoginModul.LoginModul lm = new LoginModul.LoginModul(new LoginModul.MsSqlLoginDataMapper("Persist Security Info = False; Integrated Security = true; Initial Catalog = Prod_data; Server = localhost"),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
            string verificationCode = lm.CreateLogin("inttest@eal.dk", "123456", "123456");
            lm.ActivateUser("inttest@eal.dk", verificationCode);

            try
            {
                long userID = lm.Login("inttest@eal.dk", "uij54hgt");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err3001");
            }
            Assert.IsTrue(DidItThoughAnException);

            try
            {
                long userID = lm.Login("inttest@eal.dk", "uij54hgt");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err3001");
            }
            Assert.IsTrue(DidItThoughAnException);

            try
            {
                long userID = lm.Login("inttest@eal.dk", "uij54hgt");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err3001");
            }
            Assert.IsTrue(DidItThoughAnException);

            try
            {
                long userID = lm.Login("inttest@eal.dk", "uij54hgt");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err3002");
            }
            Assert.IsTrue(DidItThoughAnException);

            try
            {
                long userID = lm.Login("inttest@eal.dk", "123456");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err3002");
            }
            Assert.IsTrue(DidItThoughAnException);

            try
            {
                string newVerificationCode = lm.InitiateResetPassword("inttest@eal.dk");
                lm.ResetPassword("inttest@eal.dk", newVerificationCode, "NewPassword", "NewPassword");

                long userID = lm.Login("inttest@eal.dk", "NewPassword");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print("6 => " + e.Message);
                DidItThoughAnException = true;
            }
            Assert.IsFalse(DidItThoughAnException);

            try
            {
                lm.DeleteLogin("inttest@eal.dk", "NewPassword");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print("Delete: " + e.Message);
                DidItThoughAnException = true;
            }
            Assert.IsFalse(DidItThoughAnException);
        }
    }
}