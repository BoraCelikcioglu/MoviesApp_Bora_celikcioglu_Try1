using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IDirectorsService
    {
        public IQueryable<DirectorsModel> Query();


        public ServiceBase Create(Director record);
        public ServiceBase Update(Director record);
        public ServiceBase Delete(int id);
    }
    public class DirectorsService : ServiceBase, IDirectorsService
    {
        public DirectorsService(Db db) : base(db)
        {
        }

        public IQueryable<DirectorsModel> Query()
        {
            return _db.Directors.OrderBy(d => d.Name).Select(d => new DirectorsModel() { Record = d });
        }

        public ServiceBase Create(Director record)
        {
            if (_db.Directors.Any(d => d.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Director with the same name exists!!!");
            record.Name = record.Name?.Trim();
            _db.Directors.Add(record);
            _db.SaveChanges(); // commit to the database
            return Success("Director created successfully.");
        }

        public ServiceBase Update(Director record)
        {
            if (_db.Directors.Any(d => d.Id != record.Id && d.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("Director with the same name exists!!!");
            // var entity = _db.Directors.Find(record.Id); 
            var entity = _db.Directors.SingleOrDefault(d => d.Id == record.Id);
            if (entity is null)
                return Error("Director can't be found!!!");
            entity.Name = record.Name?.Trim();
            _db.Directors.Update(entity);
            _db.SaveChanges();
            return Success("Director updated successfully.");

        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Directors.Include(d => d.Movies).SingleOrDefault(d => d.Id == id);
            if (entity is null)
                return Error("Director can't be found!!!");
            if (entity.Movies.Any()) // count >0
                return Error("Director has relational movies");
            _db.Directors.Remove(entity);
            _db.SaveChanges();
            return Success("Director deleted succesfully.");
        }




    }
}
