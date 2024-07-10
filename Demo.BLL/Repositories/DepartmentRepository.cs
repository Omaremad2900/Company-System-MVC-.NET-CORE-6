using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Contexts;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository


    {
        public DepartmentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
