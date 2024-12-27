using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IMoviesService
    {

        public IQueryable<MoviesModel> Query();

        public ServiceBase Create(Movie record);
        public ServiceBase Update(Movie record);
        public ServiceBase Delete(int id);
    }

    public class MoviesService : ServiceBase, IService<Movie, MoviesModel>
    {
        public MoviesService(Db db) : base(db)
        {

        }
        public IQueryable<MoviesModel> Query()
        {
            return _db.Movies.Include(m => m.Director).Include(m => m.MovieGenres).
                ThenInclude(mg => mg.Genre).OrderByDescending(m => m.ReleaseDate).
                ThenByDescending(m => m.TotalRevenue).
                ThenBy(m => m.Name).Select(m => new MoviesModel() { Record = m });
            //return _db.Movies.OrderBy(m => m.Name).Select(m => new MoviesModel() { Record = m });
        }
        public ServiceBase Create(Movie record)
        {
            var director = _db.Directors.FirstOrDefault(d => d.Id == record.DirectorId); // Assuming DirectorId is a foreign key.
            if (_db.Movies.Any(m => m.Id == record.Id))
                return Error("That movie exists!!!");
            record.Id = record.Id;
             //
            //record.Name = record.Name?.Trim();
            _db.Movies.Add(record);
            _db.SaveChanges();
            return Success("movie added succesfully");
        }
        public ServiceBase Update(Movie record)
        {

            if (_db.Movies.Any(m => m.Id != record.Id && m.Name.ToUpper()== record.Name.ToUpper().Trim()))
                return Error("That movie exists!!!");
            var entity = _db.Movies.Include(m=>m.MovieGenres).SingleOrDefault(m=>m.Id == record.Id);
            if (entity == null)
                return Error("Movie not found");
            _db.MovieGenres.RemoveRange(entity.MovieGenres);
            entity.Name = record.Name?.Trim();
            entity.ReleaseDate = record.ReleaseDate;
            entity.TotalRevenue = record.TotalRevenue;
            entity.DirectorId=record.DirectorId;    
            entity.MovieGenres = record.MovieGenres;
            _db.Movies.Update(entity);
            _db.SaveChanges();
            return Success("Movie updated");
        }
        public ServiceBase Delete(int id)
        {
            var entity = _db.Movies.Include(m=>m.Director).SingleOrDefault(x => x.Id == id);
            if (entity is null)
                return Error("Movie not found");

            if (entity.Director is not null)

                return Error("Movie has relational directors");
           
            
            _db.Movies.Remove(entity);
            _db.SaveChanges();
            return Success("movie deleted");

        }

        

        
    }



}
