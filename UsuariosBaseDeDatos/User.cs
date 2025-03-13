using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosBaseDeDatos
{
    internal class User
    {
        private string username;
        private string password;
        private string secret;

        public User(string username, string password, string secret)
        {
            this.username = username;
            this.password = password;
            this.secret = secret;
        }

        public string GetUserName()
        {
            return username;
        }

        public string GetPassword()
        {
            return password;
        }

        public bool VerifyPassword(string password)
        {
            return password == this.password ? true : false;
        }

        public string GetSecret()
        {
            return secret;
        }
    }
}
