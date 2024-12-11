
using EmployeeFormApi.Model;
using Microsoft.EntityFrameworkCore;
namespace EmployeeFormApi.Context
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
    }
  
}
