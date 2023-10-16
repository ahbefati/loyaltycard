
using Microsoft.EntityFrameworkCore;
namespace loyaltycard.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine { get; set; }
        public string District { get; set; }

        public string City { get; set; }
        public string Status { get; set; }
        
    }

}
