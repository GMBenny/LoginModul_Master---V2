using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginModul_Test
{
    [TestClass]
    public class UserChangeEMail_Test
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
                int userID = lm.Login("bejo@eal.dk", "123456");
                //lm.ChangeEmail("bejo@eal.dk", "bbb@eal.dk", "123456");
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = true;
            }
            Assert.IsFalse(DidItThoughAnException);
        }
    }
}
