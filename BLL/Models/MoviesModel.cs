using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class MoviesModel
    {
        public Movie Record {  get; set; }
        public int Id => Record.Id;

    
        public string Name => Record.Name;

        public DateTime? ReleaseDate => Record.ReleaseDate;

        public decimal TotalRevenue => Record.TotalRevenue; 
        public int? DirectorId => Record.DirectorId;

        public string DirectorName => Record.Director.Name;
        

    }
}
