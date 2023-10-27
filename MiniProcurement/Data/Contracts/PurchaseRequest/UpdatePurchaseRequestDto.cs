namespace MiniProcurement.Data.Contracts.PurchaseRequest;

public class UpdatePurchaseRequestDto
{
    public required string DocumentNumber { get; set; }
    public DateTime UpdatedOn { get; set; } = DateTime.Now;
}