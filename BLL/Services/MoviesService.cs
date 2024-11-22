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

    public class MoviesService : ServiceBase, IMoviesService
    {
        public MoviesService(Db db) : base(db)
        {

        }
        public IQueryable<MoviesModel> Query()
        {
            return _db.Movies.OrderBy(m => m.Name).Select(m => new MoviesModel() { Record = m });
        }
        public ServiceBase Create(Movie record)
        {
            if (_db.Movies.Any(m => m.Id == record.Id))
                return Error("That movie exists!!!");
            record.Id = record.Id;
            //record.Name = record.Name?.Trim();
            _db.Movies.Add(record);
            _db.SaveChanges();
            return Success("movie added succesfully");
        }
        public ServiceBase Update(Movie record)
        {

            if (_db.Movies.Any(m => m.Id != record.Id && m.Name.ToUpper()== record.Name.ToUpper().Trim()))
                return Error("That movie exists!!!");
            var entity = _db.Movies.SingleOrDefault(m=>m.Id == record.Id);
            if (entity == null)
                return Error("Movie not found");
            entity.Name = record.Name?.Trim();
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
