using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class DirectorsModel
    {
        public Director Record { get; set; }


        public int Id => Record.Id;

        public string Name => Record.Name;


        public string Surname => Record.Surname;

        public bool IsRetired => Record.IsRetired;


        public List<Movie> Movies => Record.Movies;






       

    }
}
