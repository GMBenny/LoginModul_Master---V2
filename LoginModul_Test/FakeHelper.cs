using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginModul_Test
{
    class FakeHelper
    {
        public static string FakeErrorMessage(int error)
        {
            return "Err" + error.ToString();
        }
        public static void chkEMail(string EMail, LoginModul.ErrorMessage errorMessage)
        {
        }
        public static void chkPassword(string password, LoginModul.ErrorMessage errorMessage)
        {
        }

    }
}
