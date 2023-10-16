using Microsoft.EntityFrameworkCore;
namespace loyaltycard.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateOnly DayOfBirth { get; set; }
        public string Mail { get; set; }
        public string Status { get; set; }
    }
}
