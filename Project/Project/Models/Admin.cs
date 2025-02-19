using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    internal class Admin : User
    {
        public DateTime LastLogIn { get; set; }
        public override string ToString()
        {
            return base.ToString() + $" - Last log in: {LastLogIn}";
        }
    }
}
