using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHWebApplication.Models.Models.PayrollModels;
public static class PayrollConstants
{
    // Faixas salariais para INSS
    public const decimal INSSFirstRange = 1320.00m;
    public const decimal INSSSecondRange = 2571.29m;
    public const decimal INSSThirdRange = 3856.94m;
    public const decimal INSSFourthRange = 7507.49m;

    // Percentuais de INSS
    public const decimal INSSFirstRate = 0.075m;
    public const decimal INSSSecondRate = 0.09m;
    public const decimal INSSThirdRate = 0.12m;
    public const decimal INSSFourthRate = 0.14m;

    // Deduções dependentes para IRRF
    public const decimal IRRFDependentDeduction = 189.59m;

    // Faixas salariais de IRRF
    public const decimal IRRFFirstRange = 2259.20m;
    public const decimal IRRFSecondRange = 2826.65m;
    public const decimal IRRFThirdRange = 3751.05m;
    public const decimal IRRFFourthRange = 4664.68m;

    // Percentuais de IRRF
    public const decimal IRRFFirstRate = 0.075m;
    public const decimal IRRFSecondRate = 0.15m;
    public const decimal IRRFThirdRate = 0.225m;
    public const decimal IRRFFourthRate = 0.275m;

    // Deduções para IRRF
    public const decimal IRRFFirstDeduction = 169.44m;
    public const decimal IRRFSecondDeduction = 381.44m;
    public const decimal IRRFThirdDeduction = 636.13m;
    public const decimal IRRFFourthDeduction = 869.36m;
}

