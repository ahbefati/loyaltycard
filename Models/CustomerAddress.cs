using Microsoft.EntityFrameworkCore;
using loyaltycard.Models;
namespace loyaltycard.Models
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int AddressId{ get; set; }
        public string Status { get; set; }


    }
}
