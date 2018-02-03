using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practicum18
{
    class User
    {
        public User()
        {
            Contacten = new List<Contact>();
            Login = string.Empty;

            ContactFileLocation = Guid.NewGuid().ToString() + ".txt";

        }



        public List<Contact> Contacten { get; set; }
        public string Login { get; set; }


        private string paswoord;

        public string Paswoord
        {
            get { return paswoord; }
            set { paswoord = value; }
        }

        public string ContactFileLocation { get; set; }

        public override string ToString()
        {
            return this.Login + "/" + this.paswoord + "/" + this.ContactFileLocation + Environment.NewLine;
        }
    }
}
