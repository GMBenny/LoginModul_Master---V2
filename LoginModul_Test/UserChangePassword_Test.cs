using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginModul_Test
{
    [TestClass]
    public class UserChangePassword_Test
    {
        [TestMethod]
        public void U_LoginModul_UserChangePassword_AllInputAreOk_Void()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
                lm.ActivateUser("bejo@eal.dk", verificationCode);
                long userID = lm.Login("bejo@eal.dk", "123456");
                lm.ChangePassword("bejo@eal.dk", "123456", "56789jkl", "56789jkl");
                long userID2  = lm.Login("bejo@eal.dk", "56789jkl");
                DidItThoughAnException = (userID != userID2);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = true;
            }
            Assert.IsFalse(DidItThoughAnException);
        }
        [TestMethod]
        public void U_LoginModul_UserChangePassword_CantLoginWithOldPassword_Exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
                lm.ActivateUser("bejo@eal.dk", verificationCode);
                long userID = lm.Login("bejo@eal.dk", "123456");
                lm.ChangePassword("bejo@eal.dk", "123456", "56789jkl", "56789jkl");
                long userID2 = lm.Login("bejo@eal.dk", "123456");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err3001");
            }
            Assert.IsTrue(DidItThoughAnException);
        }

    }
}
