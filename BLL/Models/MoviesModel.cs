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
    public class MoviesModel
    {
        public Movie Record {  get; set; }
        
        public string Name => Record.Name;

        [DisplayName("Release Date")]
        public string ReleaseDate => !Record.ReleaseDate.HasValue  ? string.Empty : Record.ReleaseDate.Value.ToString("MM/dd/yyyy") ; //public DateTime? ReleaseDate => Record.ReleaseDate 

        [DisplayName("Total Revenue")]
        public string TotalRevenue => Record.TotalRevenue.HasValue ? Record.TotalRevenue.
            Value.ToString("C2") : "0";

        public string DirectorName => Record.Director?.Name + " " + Record.Director?.Surname;

        //Way 1:
        //[DisplayName("Movie Genres")]
        //public List<Genre> MovieGenreList => Record.MovieGenres?.Select(mg => mg.Genre).ToList(); 

        //Way 2:
        public string Genres => string.Join("<br>",Record.MovieGenres?.Select(mg => mg.Genre?.Name));


        [DisplayName("Genres")]
        public List<int> GenreIds
        { 
          get => Record.MovieGenres?.Select(mg => mg.GenreId).ToList();
          set => Record.MovieGenres = value.Select(v => new MovieGenre() { GenreId = v }).
                ToList();
        }


    }
}
