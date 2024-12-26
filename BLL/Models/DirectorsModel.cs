using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DirectorsModel
    {
        public Director Record { get; set; }



        public string Name => Record.Name;
        public string Surname => Record.Surname;

        [DisplayName("Full Name")]
        public string FullName => Record.Name + " " + Record.Surname;

        [DisplayName("Status")]
        public string IsRetired => Record.IsRetired ? "Retired" : "Not Retired";


        public string Movies => string.Join("<br>", Record.Movies?.Select(m => m.Name));








    }
}
