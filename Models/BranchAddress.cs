using Microsoft.EntityFrameworkCore;
namespace loyaltycard.Models
{
    public class BranchAddress
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int AddressId { get; set; }
        public string Status { get; set; }







    }
}
