using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RHWebApplication.Shared.Models.UserModels;

namespace RHWebApplication.Shared.Models.PayrollModels;
public class Payroll
{
    #pragma warning disable CS8618
    public Payroll() {}
    public Payroll(Employee employee, float overTime, decimal commission)
    {
        Employee = employee;
        OverTime = overTime;
        Commission = commission;
        CreationDate = DateTime.Now;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public int EmployeeId { get; set; }
    public virtual Employee Employee { get; init; }
    public decimal Gross { get; set; } // Salario Bruto
    public float OverTime { get; set; }
    public decimal Commission { get; set; }
    public decimal Additionals { get; set; }
    public decimal Deductions { get; set; }
    public decimal Net { get; set; } // Salario Liquido
    public DateTime CreationDate { get; init; }
}

/*
Benefícios, como auxílio-transporte, vale-alimentação e plano de saúde;
 */

/*
**INSS
 Faixa Salarial (R$)	Alíquota (%)
Até R$ 1.320,00	7,5%
De R$ 1.320,01 até R$ 2.571,29	9%
De R$ 2.571,30 até R$ 3.856,94	12%
De R$ 3.856,95 até R$ 7.507,49	14%
 */

/*
 * IRRF
Faixa Salarial (R$)	Alíquota (%)	Parcela a Deduzir (R$)
Até R$ 2.112,00	Isento	-
De R$ 2.112,01 até R$ 2.826,65	7,5%	R$ 158,40
De R$ 2.826,66 até R$ 3.751,05	15%	R$ 370,40
De R$ 3.751,06 até R$ 4.664,68	22,5%	R$ 651,73
Acima de R$ 4.664,68	27,5%	R$ 884,96
*/
