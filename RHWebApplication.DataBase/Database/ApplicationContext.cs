using Microsoft.EntityFrameworkCore;
using RHWebApplication.Shared.Models.UserModels;
using RHWebApplication.Shared.Models.PayrollModels;
using RHWebApplication.Shared.Models.JobModels;

namespace RHWebApplication.Database;
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Payroll> PayrollHistory { get; set; }

    public string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RHDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString)
            .UseLazyLoadingProxies();
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configurações para o relacionamento TPT Employee:User e Admin:User
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Employee>().ToTable("Employees");
        modelBuilder.Entity<Admin>().ToTable("Admins");

        modelBuilder.Entity<Employee>().HasBaseType<User>();
        modelBuilder.Entity<Admin>().HasBaseType<User>();

        modelBuilder.Entity<Employee>().HasOne(e => e.Job).WithMany(j => j.Employees).HasForeignKey(e => e.JobId);

        modelBuilder.Entity<Employee>().HasMany(e => e.PayrollHistory)
            .WithOne(p => p.Employee).HasForeignKey(p => p.EmployeeId);


        // Configuração para a entidade Job
        modelBuilder.Entity<Job>()
            .Property(j => j.BaseSalary)
            .HasPrecision(18, 2); // 18 é a precisão, 2 é a escala

        // Configuração para a entidade Payroll
        modelBuilder.Entity<Payroll>()
            .Property(p => p.Additionals)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payroll>()
            .Property(p => p.Commission)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payroll>()
            .Property(p => p.Deductions)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payroll>()
            .Property(p => p.Gross)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payroll>()
            .Property(p => p.Net)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payroll>()
            .Property(p => p.OverTime)
            .HasColumnType("float");
    }
}
