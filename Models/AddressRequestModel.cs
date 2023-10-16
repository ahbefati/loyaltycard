namespace loyaltycard.Models

{
    public class AddressRequestModel:Address
    {
        public int CustomerId { get; set; }
        public int BranchId { get; set; }

    }
}
