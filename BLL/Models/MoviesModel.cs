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
        

    
        public string Name => Record.Name;

        public string ReleaseDate => !Record.ReleaseDate.HasValue  ? string.Empty : Record.ReleaseDate.Value.ToString("MM/dd/yyyy") ; //public DateTime? ReleaseDate => Record.ReleaseDate 

        public string TotalRevenue => Record.TotalRevenue.ToString("C2");

        public string DirectorName => Record.Director?.Name;
        

    }
}
