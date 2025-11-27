namespace ERP.UI.Blazor.Models;



public class AddEmployeeDto
{
    public string? EmployeeCode { get; set; } = null;
    public string? FirstName { get; set; } = null;
    public string? LastName { get; set; } = null;
    public string? NationalId { get; set; } = null;
    public string? ContactNumber { get; set; } = null;

    public string? Gender { get; set; }

    public string? WorkingStatus { get; set; } = null;

    public string? FatherName { get; set; } = null;
    public string? MaritalStatus { get; set; } = null;
    public int? ChildrenCount { get; set; }

    public string? ShenasnameNumber { get; set; } = null;
    public string? ShenasnameSerialLetter { get; set; } = null;
    public string? ShenasnameSerie { get; set; } = null;
    public string? ShenasnameSerial { get; set; } = null;

    public DateTime? BirthDate { get; set; } = null;
    public Guid? BirthPlaceId { get; set; } = null; // fk

    public Guid? ShenasnameIssuedPlaceId { get; set; } = null;  // fk

    public string? InsurranceCode { get; set; } = null;
    public string? InsurranceStatus { get; set; } = null;
    public bool HasInsurance { get; set; } = true;
    public int? ExtraInsurranceCount { get; set; } = null;

    public Guid? DepartmentId { get; set; } = null;  // fk
    public Guid? JobTitleId { get; set; } = null;   // fk
    public string? EmploymentType { get; set; } = null;
    public DateTime? StartingDate { get; set; } = null;

    public string? LandPhoneNumber { get; set; } = null;
    public string? Address { get; set; } = null;
    public string? PostalCode { get; set; } = null;

    public string? MostRecentDegree { get; set; } = null;
    public string? Major { get; set; } = null;
}
