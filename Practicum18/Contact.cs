using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practicum18
{
    class Contact
    {
        public string Naam { get; set; }
        public string TelefoonNummer { get; set; }
        public string EmailAdress { get; set; }

        public override string ToString()
        {
            return "Naam: " + this.Naam + "\tTelefoon#: " + this.TelefoonNummer + "\tEmail: " + this.EmailAdress + Environment.NewLine;
        }
    }
}
