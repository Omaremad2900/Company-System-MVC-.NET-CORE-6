using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {

        private readonly AppDbContext _appDbContext;
        public EmployeeRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }

       

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _appDbContext.Employees.Where(E => E.Address == address);
        }


        public async Task <IEnumerable <Employee>> GetEmployeesByName(string name)
        {
           return await _appDbContext.Employees.Where(EF => EF.Name.ToLower().Contains(name.ToLower())).Include(E => E.Department).ToListAsync();
        }
    }
}
