using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginModul_Test
{
    [TestClass]
    public class UserActivate_Test
    {
        [TestMethod]
        public void U_LoginModul_UserActivate_AllInputAreOk_Void()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(), null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
                lm.ActivateUser("bejo@eal.dk", verificationCode);
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
        public void U_LoginModul_UserActivate_VerificationCodeLowCase_Void()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(), null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
                lm.ActivateUser("bejo@eal.dk", verificationCode.ToLower());
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
        public void U_LoginModul_UserActivate_VerificationCodeUpperCase_Void()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(), null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
                lm.ActivateUser("bejo@eal.dk", verificationCode.ToUpper());
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
        public void U_LoginModul_UserActivate_WrongVerificationCode_Exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
                lm.ActivateUser("bejo@eal.dk", "BadCode");
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
        public void U_LoginModul_UserActivate_ActivateActivatedUser_Exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
                lm.ActivateUser("bejo@eal.dk", verificationCode);
                lm.ActivateUser("bejo@eal.dk", verificationCode);
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err4001");
            }
            Assert.IsTrue(DidItThoughAnException);
        }
        [TestMethod]
        public void U_LoginModul_UserActivate_ActivateNotExcistingUser_Exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                lm.ActivateUser("findes_ikke@eal.dk", "4oit5jlgkf");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err1002");
            }
            Assert.IsTrue(DidItThoughAnException);
        }
    }
}
