using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RHWebApplication.Shared.Models.UserModels;
using RHWebApplication.Models.Models.PayrollModels;

namespace RHWebApplication.Shared.Models.PayrollModels;
public class Payroll
{
#pragma warning disable CS8618
    public Payroll() { }
    public Payroll(Employee employee, float overTime, decimal commission)
    {
        Employee = employee;
        OverTime = overTime;
        Commission = commission;
        OverTimeAditionals = CalcOverTimeValue();
        PericulosityValue = CalcPericulosityValue();
        UnhealthyValue = CalcUnhealthyValue();
        INSSDeduction = CalcINSS();
        IRRFDeduction = CalcularIRRF();
        Deductions = CalcDeductions();
        Gross = CalcGross();
        Net = CalcNet();
        CreationDate = DateTime.Now;

    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public int EmployeeId { get; set; }
    public virtual Employee Employee { get; init; }
    public float OverTime { get; set; }
    public decimal OverTimeAditionals { get; set; }
    public decimal PericulosityValue { get; set; }
    public decimal UnhealthyValue { get; set; }
    public decimal Commission { get; set; }
    public decimal INSSDeduction { get; set; }
    public decimal IRRFDeduction { get; set; }
    public decimal Deductions { get; set; }
    public decimal Net { get; set; } // Salario Liquido
    public decimal Gross { get; set; }
    public DateTime CreationDate { get; init; }

    private decimal CalcPericulosityValue()
    {
        if (Employee.Job.IsPericulosity)
        {
            var PericulosityValue = Employee.Job.BaseSalary * (decimal)0.3f;
            return PericulosityValue;
        }

        return 0;
    }
    private decimal CalcUnhealthyValue()
    {
        float UnhealthyPorcent;
        if (Employee.Job.UnhealthyLevel == 1)
        {
            UnhealthyPorcent = 0.1f;
        }
        else if (Employee.Job.UnhealthyLevel == 2)
        {
            UnhealthyPorcent = 0.2f;
        }
        else if (Employee.Job.UnhealthyLevel == 3)
        {
            UnhealthyPorcent = 0.4f;
        }
        else
            UnhealthyPorcent = 0;

        var unhealthyValue = Employee.Job.BaseSalary * (decimal)UnhealthyPorcent;
        return unhealthyValue;
    }
    private decimal CalcGross()
    {
        var gross = Employee.Job.BaseSalary + Commission + CalcOverTimeValue() + PericulosityValue + UnhealthyValue;
        return gross;
    }
    private decimal CalcOverTimeValue()
    {                            
        var overTimeValue = Employee.Job.OverTimeValue * (decimal)OverTime;
        return overTimeValue;
    }
    private decimal CalcDeductions()
    {
        var deductions = CalcINSS() + CalcularIRRF();
        return deductions;
    }
    private decimal CalcNet()
    {
        var net = CalcGross() - CalcDeductions();
        return net;
    }
    private decimal CalcINSS()
    {
        decimal inss = CalcGross() switch
        {
            <= PayrollConstants.INSSFirstRange => CalcGross() * PayrollConstants.INSSFirstRate,
            <= PayrollConstants.INSSSecondRange => (PayrollConstants.INSSFirstRange * PayrollConstants.INSSFirstRate) +
                                                    ((CalcGross() - PayrollConstants.INSSFirstRange) * PayrollConstants.INSSSecondRate),
            <= PayrollConstants.INSSThirdRange => (PayrollConstants.INSSFirstRange * PayrollConstants.INSSFirstRate) +
                                                   ((PayrollConstants.INSSSecondRange - PayrollConstants.INSSFirstRange) * PayrollConstants.INSSSecondRate) +
                                                   ((CalcGross() - PayrollConstants.INSSSecondRange) * PayrollConstants.INSSThirdRate),
            <= PayrollConstants.INSSFourthRange => (PayrollConstants.INSSFirstRange * PayrollConstants.INSSFirstRate) +
                                                    ((PayrollConstants.INSSSecondRange - PayrollConstants.INSSFirstRange) * PayrollConstants.INSSSecondRate) +
                                                    ((PayrollConstants.INSSThirdRange - PayrollConstants.INSSSecondRange) * PayrollConstants.INSSThirdRate) +
                                                    ((CalcGross() - PayrollConstants.INSSThirdRange) * PayrollConstants.INSSFourthRate),
            _ => (PayrollConstants.INSSFirstRange * PayrollConstants.INSSFirstRate) +
                 ((PayrollConstants.INSSSecondRange - PayrollConstants.INSSFirstRange) * PayrollConstants.INSSSecondRate) +
                 ((PayrollConstants.INSSThirdRange - PayrollConstants.INSSSecondRange) * PayrollConstants.INSSThirdRate) +
                 ((PayrollConstants.INSSFourthRange - PayrollConstants.INSSThirdRange) * PayrollConstants.INSSFourthRate)
        };

        return inss;
    }

    private decimal CalcularIRRF()
    {
        decimal dependentsDeductions = Employee.Dependents * PayrollConstants.IRRFDependentDeduction;
        decimal baseCalc = CalcGross() - CalcINSS() - dependentsDeductions;

        decimal irrf = baseCalc switch
        {
            <= PayrollConstants.IRRFFirstRange => 0,
            <= PayrollConstants.IRRFSecondRange => (baseCalc * PayrollConstants.IRRFFirstRate) - PayrollConstants.IRRFFirstDeduction,
            <= PayrollConstants.IRRFThirdRange => (baseCalc * PayrollConstants.IRRFSecondRate) - PayrollConstants.IRRFSecondDeduction,
            <= PayrollConstants.IRRFFourthRange => (baseCalc * PayrollConstants.IRRFThirdRate) - PayrollConstants.IRRFThirdDeduction,
            _ => (baseCalc * PayrollConstants.IRRFFourthRate) - PayrollConstants.IRRFFourthDeduction
        };

        return irrf;
    }
}
