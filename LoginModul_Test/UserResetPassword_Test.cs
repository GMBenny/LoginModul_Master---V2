using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginModul_Test
{
    [TestClass]
    public class UserResetPassword_Test
    {
        [TestMethod]
        public void U_LoginModul_UserResetPassword_AllInputAreOk_Void()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
                lm.ActivateUser("bejo@eal.dk", verificationCode);
                int userID1 = lm.Login("bejo@eal.dk", "123456"); // Poof it works

                string newVerificationCode = lm.InitiateResetPassword("bejo@eal.dk");
                lm.ResetPassword("bejo@eal.dk", newVerificationCode, "NewPassword", "NewPassword");

                int userID2 = lm.Login("bejo@eal.dk", "NewPassword"); // Poof it works again

                DidItThoughAnException = (userID1 != userID2);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = true;
            }
            Assert.IsFalse(DidItThoughAnException);
        }
        [TestMethod]
        public void U_LoginModul_UserResetPassword_LoginAfterReset_Exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
                lm.ActivateUser("bejo@eal.dk", verificationCode);
                int userID1 = lm.Login("bejo@eal.dk", "123456"); // Poof it works

                string newVerificationCode = lm.InitiateResetPassword("bejo@eal.dk");

                int userID2 = lm.Login("bejo@eal.dk", "123456"); // Poof it works again

                DidItThoughAnException = (userID1 != userID2);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = true;
            }
            Assert.IsTrue(DidItThoughAnException);
        }
    }
}