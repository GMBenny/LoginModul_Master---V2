using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginModul
{
    public interface ILoginDataMapper: IDataMapper<Login>
    {
        long ReadMaxID();
        Login Read(string EMail);
        bool DoesVerificationCodeExist(string verificationCode);
    }
}
