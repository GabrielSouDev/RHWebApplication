using Microsoft.EntityFrameworkCore;
using RHWebApplication.Shared.Models.UserModels;
using RHWebApplication.Shared.Models.PayrollModels;

namespace RHWebApplication.Database;
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Payroll> Payrolls { get; set; }

    public string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RHDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString)
            .UseLazyLoadingProxies();
        }
    }
}
