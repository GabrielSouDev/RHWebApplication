using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RHWebApplication.Shared.Models.UserModels;

namespace RHWebApplication.Shared.Models.CompanyModels;

public class Company
{
    public Company() { }
    public Company(string tradeName, string corporateName, int cnpj)
    {
		TradeName = tradeName;
		CorporateName = corporateName;
		CNPJ = cnpj;
		JobTitles = new List<JobTitle>();
		Admins = new List<Admin>();
	}
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }
	public string TradeName { get; set; } = string.Empty; //nome fantasia
	public string CorporateName { get; set; } = string.Empty; //razão social
    public int CNPJ { get; set; }
	public virtual List<JobTitle> JobTitles { get; set; } = new();
	public virtual List<Admin> Admins { get; set; } = new();
}
