using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
     public class GenericRepository<T> : IGenericRepository<T> where T : class

    {
        private readonly AppDbContext dbContext;
        public GenericRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task Add(T item)
        {
            await dbContext.AddAsync(item);
         
        }

        public void Delete(T item)
        {
            dbContext.Remove(item);
           
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            { return  (IEnumerable<T>)await dbContext.Employees.Include(E => E.Department).ToListAsync(); }
            return await dbContext.Set<T>().ToListAsync();
           
        }

        public async Task<T> GetById(int id)
        {
           return await dbContext.Set<T>().FindAsync(id);  
        }

        public void Update(T item)
        {
            dbContext.Update(item) ;
           
        }
    }
}
