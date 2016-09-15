using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginModul_Test
{
    [TestClass]
    public class HelperChkPassword
    {
        [TestMethod]
        public void U_Helper_PasswordChk_OkPassword_Void()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.Helper.chkPassword("123456", new LoginModul.ErrorMessage(FakeHelper.FakeErrorMessage));
                DidItThoughAnException = false;
            }
            catch (Exception)
            {
                DidItThoughAnException = true;
            }
            Assert.IsFalse(DidItThoughAnException);
        }
        [TestMethod]
        public void U_Helper_PasswordChk_PasswordTooShort_Exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.Helper.chkPassword("1234", new LoginModul.ErrorMessage(FakeHelper.FakeErrorMessage));
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err2003");
            }
            Assert.IsTrue(DidItThoughAnException);
        }
        [TestMethod]
        public void U_Helper_PasswordChk_PasswordNull_Exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.Helper.chkPassword(null, new LoginModul.ErrorMessage(FakeHelper.FakeErrorMessage));
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err2003");
            }
            Assert.IsTrue(DidItThoughAnException);
        }
    }
}
