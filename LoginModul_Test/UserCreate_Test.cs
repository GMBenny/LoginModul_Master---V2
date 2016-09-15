using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LoginModul_Test
{
    [TestClass]
    public class UserCreate_Test
    {
        [TestMethod]
        public void U_LoginModul_CreateLogin_AllInputAreOk_Void()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123456");
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
        public void U_LoginModul_CreateLogin_UnequalPassword_Exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.LoginModul lm = new LoginModul.LoginModul(new FakeLoginDataMapper(),null, new LoginModul.EMailCheck(FakeHelper.chkEMail), new LoginModul.PasswordCheck(FakeHelper.chkPassword), FakeHelper.FakeErrorMessage);
                string verificationCode = lm.CreateLogin("bejo@eal.dk", "123456", "123498");
                DidItThoughAnException = false;
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

