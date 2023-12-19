using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Contact
    {

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Phone { get; set; }

        [Key]
        public string Email { get; set; }
    }
}
