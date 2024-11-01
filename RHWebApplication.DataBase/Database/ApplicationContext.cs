using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RHWebApplication.Shared.Models.CompanyModels;
using RHWebApplication.Shared.Models.PayrollModels;
using RHWebApplication.Shared.Models.UserModels;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RHWebApplication.Database;
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<JobTitle> jobTitles { get; set; }
    public DbSet<Payroll> PayrollHistory { get; set; }

    public string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RHDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString).EnableSensitiveDataLogging()
                .UseLazyLoadingProxies();
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>().ToTable("Companies");

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Employee>().ToTable("Employees").HasBaseType<User>();
        modelBuilder.Entity<Admin>().ToTable("Admins").HasBaseType<User>();
        modelBuilder.Entity<Staff>().ToTable("Staffs").HasBaseType<User>();

        // Relações
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Job)
            .WithMany(j => j.Employees)
            .HasForeignKey(e => e.JobId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Company)
            .WithMany(c => c.Users)
            .HasForeignKey(u => u.CompanyId);

        modelBuilder.Entity<Employee>()
            .HasMany(e => e.PayrollHistory)
            .WithOne(p => p.Employee)
            .HasForeignKey(p => p.EmployeeId);

        modelBuilder.Entity<Company>()
            .HasMany(c => c.JobTitles)
            .WithOne(j => j.Company)
            .HasForeignKey(j => j.CompanyID);

        // Propriedades com precisão
        modelBuilder.Entity<JobTitle>()
            .Property(j => j.BaseSalary)
            .HasPrecision(18, 2);

        modelBuilder.Entity<JobTitle>()
            .Property(p => p.OverTimeValue)
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
            .Property(p => p.INSSDeduction)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payroll>()
            .Property(p => p.IRRFDeduction)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payroll>()
            .Property(p => p.Net)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Payroll>()
            .Property(p => p.OverTimeAditionals)
            .HasPrecision(18, 2);

                modelBuilder.Entity<Payroll>()
            .Property(p => p.PericulosityValue)
            .HasPrecision(18, 2);

                modelBuilder.Entity<Payroll>()
            .Property(p => p.UnhealthyValue)
            .HasPrecision(18, 2);
    }
    public async Task CreateTable()
    {
        await this.Database.MigrateAsync();
        if (!this.Companies.Any())
        {
            await SeedStaffCompany();
            await SeedStaffUser();
            Console.WriteLine("STAFF SEEED FINISHED!");
        }
    }
    public async Task SeedStaffCompany()
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();

            // SQL para inserir dados
            var sql = "INSERT INTO Companies (CNPJ, CorporateName, TradeName) VALUES (@CNPJ, @CorporateName, @TradeName)";

            using (var command = new SqlCommand(sql, connection))
            {
                // Adicionando parâmetros
                command.Parameters.AddWithValue("@CNPJ", 123);
                command.Parameters.AddWithValue("@CorporateName", "Staff");
                command.Parameters.AddWithValue("@TradeName", "Staff");

                await command.ExecuteNonQueryAsync();
                await command.DisposeAsync();
            }
            await connection.DisposeAsync();
        }
    }
    public async Task SeedStaffUser()
    {

        using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            var sql = "INSERT INTO Users (Login, Password, Name, Email, UserType, CompanyId, CreationDate, IsActive) VALUES (@Login,       @Password,@Name, @Email, @UserType, @CompanyId, @CreationDate, @IsActive)";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Login", "staff");
                command.Parameters.AddWithValue("@Password", "staff");
                command.Parameters.AddWithValue("@Name", "Staff User");
                command.Parameters.AddWithValue("@Email", "staff@email.com");
                command.Parameters.AddWithValue("@UserType", "Staff");
                command.Parameters.AddWithValue("@CompanyId", 1);
                command.Parameters.AddWithValue("@CreationDate", DateTime.UtcNow);
                command.Parameters.AddWithValue("@IsActive", true);

                await command.ExecuteNonQueryAsync();
                await command.DisposeAsync();
            }
            await connection.DisposeAsync();
        }

        using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            var sql = "INSERT INTO Staffs (Id) VALUES (@Id)";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Id", 1);
                await command.ExecuteNonQueryAsync();
                await command.DisposeAsync();
            }
            await connection.DisposeAsync();
        }
    }
}
