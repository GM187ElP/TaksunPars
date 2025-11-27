namespace ERP.UI.Blazor.Models;

public class DownloadPayslipDto
{
    public Guid Id { get; set; }

    // Personnel info
    public string PersonnelCode { get; set; }
    public Guid PersonnelId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Department { get; set; }
    public string AccountNumber { get; set; }

    // Date fields
    public string Year { get; set; }
    public string Month { get; set; }

    // Salaries
    public long DailySalary { get; set; }
    public int WorkingDays { get; set; }
    public long MonthlyBaseSalary { get; set; }

    // Benefits
    public long WorkerBenefit { get; set; }
    public long HousingAllowance { get; set; }
    public long ChildAllowance { get; set; }
    public long FamilyOrFuelAllowance { get; set; }
    public long LunchAllowance { get; set; }
    public long MissionAllowance { get; set; }
    public long MobileAllowance { get; set; }
    public long CommissionOvertime { get; set; }
    public long ResponsibilityAllowance { get; set; }
    public long OtherBenefits { get; set; }
    public long TotalSalaryAndBenefits { get; set; }

    // Deductions
    public long InsuranceWorkerShare { get; set; }
    public long SupplementaryInsurance { get; set; }
    public long SalaryTax { get; set; }
    public long OnePerThousandInsurance { get; set; }

    // Loans
    public long FundLoanDeducted { get; set; }
    public long FundLoanRemaining { get; set; }
    public long CompanyLoanDeducted { get; set; }
    public long CompanyLoanRemaining { get; set; }

    // Other
    public long DebtToCompany { get; set; }
    public int PaidLeaveInDays { get; set; }
    public int UnpaidLeaveInDays { get; set; }

    // Reserved + Deductions
    public long CommissionReserve { get; set; }
    public long TotalDeductions { get; set; }

    // Final results
    public long GrossReceivable { get; set; }
    public long InsuranceAndTaxDeductions { get; set; }
    public long NetReceivable { get; set; }
    public long CompanyDeductions { get; set; }
    public long NetPayable { get; set; }
}
