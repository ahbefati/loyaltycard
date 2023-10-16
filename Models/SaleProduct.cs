using Microsoft.EntityFrameworkCore;
namespace loyaltycard.Models
{
    public class SaleProduct
    {
        public int Id { get; set; }
        public string SaleId { get; set; }
        public int ProductId { get; set; }
        public int Cost { get; set; }
        public int Bonus { get; set; }
        public string Status { get; set; }


    }
}
