namespace ERP.UI.Blazor.Models;


public class GetPayslipDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";

    public Guid Id { get; set; }

    // کد پرسنلی - Personnel Reference
    public string? EmployeeCode { get; set; } // Common between personnel and payslip

    // سال و ماه
    public string? Year { get; set; } // سال
    public string? Month { get; set; } // ماه

    // حقوق و دستمزد
    public long? DailySalary { get; set; } // حقوق روزانه
    public int? WorkingDays { get; set; } // تعداد روز کاری
    public long? MonthlyBaseSalary { get; set; } // حقوق ثابت ماهانه

    // مزایا
    public long? WorkerBenefit { get; set; } // بن کارگری
    public long? HousingAllowance { get; set; } // حق مسکن
    public long? ChildAllowance { get; set; } // حق اولاد
    public long? FamilyOrFuelAllowance { get; set; } // حق عائله مندی - سوخت
    public long? LunchAllowance { get; set; } // حق ناهار
    public long? MissionAllowance { get; set; } // حق ماموریت
    public long? MobileAllowance { get; set; } // موبایل
    public long? CommissionOvertime { get; set; } // پورسانت - اضافه کاری
    public long? ResponsibilityAllowance { get; set; } // حق مسئولیت
    public long? OtherBenefits { get; set; } // سایر مزایا

    // جمع حقوق و مزایا
    public long? TotalSalaryAndBenefits { get; set; } // جمع حقوق و مزایا

    // کسورات
    public long? InsuranceWorkerShare { get; set; } // بیمه سهم کارگر
    public long? SupplementaryInsurance { get; set; } // بیمه تکمیلی
    public long? SalaryTax { get; set; } // مالیات حقوق
    public long? OnePerThousandInsurance { get; set; } // یک در هزار بیمه

    // وام‌ها
    public long? FundLoanDeducted { get; set; } // وام صندوق کسر شده
    public long? FundLoanRemaining { get; set; } // وام صندوق مانده
    public long? CompanyLoanDeducted { get; set; } // وام شرکت کسر شده
    public long? CompanyLoanRemaining { get; set; } // وام شرکت مانده

    // بدهی و مرخصی
    public long? DebtToCompany { get; set; } // بدهی به شرکت
    public int? PaidLeaveInDays { get; set; } // مرخصی با حقوق (روز)
    public int? UnpaidLeaveInDays { get; set; } // مرخصی بدون حقوق (روز)

    // ذخیره و جمع
    public long? CommissionReserve { get; set; } // ذخیر پورسانت
    public long? TotalDeductions { get; set; } // جمع کسورات

    // دریافتی‌ها
    public long? GrossReceivable { get; set; } // ناخالص دریافتی
    public long? InsuranceAndTaxDeductions { get; set; } // کسورات بیمه و مالیات
    public long? NetReceivable { get; set; } // خالص دریافتی
    public long? CompanyDeductions { get; set; } // کسورات شرکت
    public long? NetPayable { get; set; } // خالص پرداختی


    // مرجع پرسنلی
    public Guid EmployeeId { get; set; }
}