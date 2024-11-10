using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RHWebApplication.Shared.Models.CompanyModels;
using RHWebApplication.Shared.Models.PayrollModels;
using RHWebApplication.Shared.Models.UserModels;
using System.ComponentModel.Design;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        if (!this.Companies.Any(c => c.TradeName == "Staff"))
        {
            await SeedStaffCompany();
            await SeedStaffUser();
            Console.WriteLine("STAFF SEEED FINISHED!");
        }

        if(!this.Companies.Any(c => c.TradeName == "Mercado Quero Mais"))
        {
            await SeedQueroMaisCompany();
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

	public async Task SeedQueroMaisCompany()
	{
		var company = new Company("Mercado Quero Mais", "Distribuidora Quero Mais LTDA.", 456);
		await this.Companies.AddAsync(company);
		await this.SaveChangesAsync();

		var jobTitle = new JobTitle()
		{
			Name = "Operador de Caixa",
			Description = "Operar Caixa",
			UnhealthyLevel = 0,
			IsPericulosity = false,
			OverTimeValue = 10,
			BaseSalary = 1800,
			CompanyID = company.Id
		};
		await this.jobTitles.AddAsync(jobTitle);
		await this.SaveChangesAsync();

		var employee1 = new Employee()
		{
			Login = "jorgin12",
			Password = "jorgin12",
			Name = "Carlos Jorge dos Santos",
			Email = "cjorge12@gmail.com",
			CompanyId = company.Id,
			JobId = jobTitle.Id,
			Dependents = 2,
			UserType = "Employee",
			CreationDate = DateTime.UtcNow,
			IsActive = true
		};
		await this.Employees.AddAsync(employee1);
		await this.SaveChangesAsync();

		var employee2 = new Employee()
		{
			Login = "marcos214",
			Password = "marcos214",
			Name = "Marcos Almeida da Silva",
			Email = "marcos214@gmail.com",
			CompanyId = company.Id,
			JobId = jobTitle.Id,
			Dependents = 1,
			UserType = "Employee",
			CreationDate = DateTime.UtcNow,
			IsActive = true
		};
		await this.Employees.AddAsync(employee2);
		await this.SaveChangesAsync();

		var employee3 = new Employee()
		{
			Login = "franmartins",
			Password = "franmartins",
			Name = "Francielli Martins de Oliveira",
			Email = "franmartins@gmail.com",
			CompanyId = company.Id,
			JobId = jobTitle.Id,
			Dependents = 0,
			UserType = "Employee",
			CreationDate = DateTime.UtcNow,
			IsActive = true
		};
		await this.Employees.AddAsync(employee3);
		await this.SaveChangesAsync();

        await SeedPayrollHistoryWith1year(employee1, 7, 250);
		await SeedPayrollHistoryWith1year(employee2, 10, 500);
		await SeedPayrollHistoryWith1year(employee3, 25, 1500);

		var jobTitle2 = new JobTitle()
		{
			Name = "Gerente",
			Description = "Gerenciar Mercado",
			UnhealthyLevel = 0,
			IsPericulosity = false,
			OverTimeValue = 50,
			BaseSalary = 8300,
			CompanyID = company.Id
		};
		await this.jobTitles.AddAsync(jobTitle2);
		await this.SaveChangesAsync();

		var employee4 = new Employee()
		{
			Login = "martazeira",
			Password = "martazeira",
			Name = "Marta Gonçalvez da Silva",
			Email = "martazeira@gmail.com",
			CompanyId = company.Id,
			JobId = jobTitle2.Id,
			Dependents = 3,
			UserType = "Employee",
			CreationDate = DateTime.UtcNow,
			IsActive = true
		};
		await this.Employees.AddAsync(employee4);
		await this.SaveChangesAsync();

		await SeedPayrollHistoryWith1year(employee4, 20, 4000);
	}
    private async Task SeedPayrollHistoryWith1year(Employee employee, int maxOverTime,int maxCommition)
    {
		var date = DateTime.UtcNow;
		var rand = new Random();
		var payrolls = new List<Payroll>();

		for (int i = 1; i <= 12; i++)
		{
			var payroll = new Payroll(employee, rand.Next(0, maxOverTime), rand.Next(0, maxCommition))
			{
				CreationDate = new DateTime(date.Year, i, 10, date.Hour, date.Minute, date.Second, date.Millisecond, DateTimeKind.Utc)
			};
			payrolls.Add(payroll);
		}

		await this.PayrollHistory.AddRangeAsync(payrolls);
		await this.SaveChangesAsync();
	}
}
