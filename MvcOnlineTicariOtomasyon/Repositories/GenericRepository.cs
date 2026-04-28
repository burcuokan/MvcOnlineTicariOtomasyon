using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Repositories
{
    public class GenericRepository<T> where T : class, new() 
    {
        Context c = new Context();
        DbSet<T> _object;

        public GenericRepository()
        {
            _object = c.Set<T>();
        }

        public List<T> List()
        {
            return _object.ToList();
        }
        public void TAdd(T p)
        {
            _object.Add(p);
            c.SaveChanges();
        }
        public void TDelete(T p)
        {
            _object.Remove(p);
            c.SaveChanges();
        }
        public void TUpdate(T p)
        {
            c.Entry(p).State = EntityState.Modified;
            c.SaveChanges();
        }
        public T TGet(int id)
        {
            return _object.Find(id);
        }
        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }
        public List<SelectListItem> GetDropdownList(Func<T, string> textSelector, Func<T, string> valueSelector)
        {
            return _object.ToList().Select(x => new SelectListItem
            {
                Text = textSelector(x),
                Value = valueSelector(x)
            }).ToList();
        }
    }
}