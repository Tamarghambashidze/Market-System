﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    internal class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
