using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginModul_Test
{
    [TestClass]
    public class UserLogin_Test
    {
        [TestMethod]
        public void U_LoginModul_UserActivate_AllInputAreOk_Void()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
                lm.ActivateUser("bejo@eal.dk", verificationCode);
                long userID = lm.Login("bejo@eal.dk", "123456");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = true;
            }
            Assert.IsFalse(DidItThoughAnException);
        }
        [TestMethod]
        public void U_LoginModul_UserActivate_WrongPassword_Void()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
                lm.ActivateUser("bejo@eal.dk", verificationCode);
                long userID = lm.Login("bejo@eal.dk", "111111");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err3001");
            }
            Assert.IsTrue(DidItThoughAnException);
        }
        [TestMethod]
        public void U_LoginModul_UserActivate_UserNotActivated_Void()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
                long userID = lm.Login("bejo@eal.dk", "123456");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err4002");
            }
            Assert.IsTrue(DidItThoughAnException);
        }

        [TestMethod]
        public void U_LoginModul_UserActivate_TooManyBadLogins_Exception()
        {
            bool DidItThoughAnException = false;
            LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
            string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");

            try
            {
                long userID = lm.Login("bejo@eal.dk", "uij54hgt");
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
                long userID = lm.Login("bejo@eal.dk", "uij54hgt");
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
                long userID = lm.Login("bejo@eal.dk", "uij54hgt");
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
                long userID = lm.Login("bejo@eal.dk", "uij54hgt");
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
                long userID = lm.Login("bejo@eal.dk", "123456");
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
                string newVerificationCode = lm.InitiateResetPassword("bejo@eal.dk");
                lm.ResetPassword("bejo@eal.dk", newVerificationCode, "NewPassword", "NewPassword");

                long userID = lm.Login("bejo@eal.dk", "NewPassword");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err3002");
            }
            Assert.IsFalse(DidItThoughAnException);
        }
    }
}
