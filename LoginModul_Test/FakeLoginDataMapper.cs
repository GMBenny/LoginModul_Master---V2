using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginModul;

namespace LoginModul_Test
{
    public class FakeLoginDataMapper : LoginModul.ILoginDataMapper
    {
        Dictionary<long, Login> _db;
        public FakeLoginDataMapper()
        {
            _db = new Dictionary<long, Login>();
        }

        public void Create(Login login)
        {
            _db.Add(login.UserID, login);
        }

        public void Delete(Login login)
        {
            _db.Remove(login.UserID);
        }

        public bool DoesVerificationCodeExist(string verificationCode)
        {
            return (_db.Where(p => p.Value.VerificationCode == verificationCode).Count() > 0);
        }

        public Login Read(long Key)
        {
            return _db.FirstOrDefault(p => p.Value.UserID == Key).Value;
        }

        public Login Read(string EMail)
        {
            return _db.FirstOrDefault(p => p.Value.UnconfirmedEMail == EMail || p.Value.ConfirmedEMail == EMail).Value;
        }

        public long ReadMaxID()
        {
            if (_db.Count == 0)
                return 999;

            return _db.OrderByDescending(p => p.Value.UserID).First().Value.UserID;
        }

        public void Update(Login login)
        {
            _db[login.UserID] = login;
        }
    }
}
