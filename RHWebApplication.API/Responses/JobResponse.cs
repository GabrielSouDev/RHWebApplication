namespace RHWebApplication.API.Responses;
public class JobResponse
{
    public JobResponse(int Id, string Title, string Description, bool IsUnhealthy, bool IsPericulosity, decimal BaseSalary)
    {
        this.Id = Id;
        this.Title = Title;
        this.Description = Description;
        this.IsUnhealthy = IsUnhealthy;
        this.IsPericulosity = IsPericulosity;
        this.BaseSalary = BaseSalary;
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsUnhealthy { get; set; }
    public bool IsPericulosity { get; set; }
    public decimal BaseSalary { get; set; }
}