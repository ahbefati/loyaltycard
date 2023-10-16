using Microsoft.EntityFrameworkCore;
namespace loyaltycard.Models;

public class Sale
{
    public string Id { get; set; }
    public DateOnly Date { get; set; }
    public int BranchId { get; set; }
    public int EmployeeId { get; set; }
    public int CustomerId { get; set; }
    public string Status { get; set; }


}
