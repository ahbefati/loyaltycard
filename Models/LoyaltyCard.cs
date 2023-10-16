using Microsoft.EntityFrameworkCore;
namespace loyaltycard.Models;

public class LoyaltyCard
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string LoyaltyCardNo { get; set; }
    public int Amount { get; set; }
    public DateOnly RegisterDate { get; set; }
    public string Status { get; set; }

}
