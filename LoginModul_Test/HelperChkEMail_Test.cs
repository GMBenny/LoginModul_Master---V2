using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoginModul_Test
{
    [TestClass]
    public class HelperChkEMail_Test
    {
        [TestMethod]
        public void U_HelperChkEMail_AllInputOk_void()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.Helper.chkEMail("b@b.dk", new LoginModul.ErrorMessage(FakeHelper.FakeErrorMessage));
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
        public void U_HelperChkEMail_BadEmail1_exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.Helper.chkEMail("bb.dk", new LoginModul.ErrorMessage(FakeHelper.FakeErrorMessage));
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err1003");
            }
            Assert.IsTrue(DidItThoughAnException);
        }
        [TestMethod]
        public void U_HelperChkEMail_BadEmail2_exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.Helper.chkEMail("b@bdk", new LoginModul.ErrorMessage(FakeHelper.FakeErrorMessage));
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err1003");
            }
            Assert.IsTrue(DidItThoughAnException);
        }
        [TestMethod]
        public void U_HelperChkEMail_BadEmail3_exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.Helper.chkEMail("", new LoginModul.ErrorMessage(FakeHelper.FakeErrorMessage));
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err1003");
            }
            Assert.IsTrue(DidItThoughAnException);
        }
        [TestMethod]
        public void U_HelperChkEMail_BadEmail4_exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.Helper.chkEMail(null, new LoginModul.ErrorMessage(FakeHelper.FakeErrorMessage));
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err1003");
            }
            Assert.IsTrue(DidItThoughAnException);
        }
        [TestMethod]
        public void U_HelperChkEMail_BadEmail5_exception()
        {
            bool DidItThoughAnException = false;
            try
            {
                LoginModul.Helper.chkEMail("bbdk", new LoginModul.ErrorMessage(FakeHelper.FakeErrorMessage));
                DidItThoughAnException = false;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                DidItThoughAnException = e.Message.Substring(0, 7).Equals("Err1003");
            }
            Assert.IsTrue(DidItThoughAnException);
        }
    }
}
