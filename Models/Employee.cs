using Microsoft.EntityFrameworkCore;
namespace loyaltycard.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BranchId{get; set;}
        public string Status { get; set; }



    }
}
