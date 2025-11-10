using TruekAppAPI.Models;

namespace TruekAppAPI.DTO.Trade;

public class TradeDto
{
    public int Id { get; set; }
    public int RequesterUserId { get; set; }
    public int OwnerUserId { get; set; }
    public int TargetListingId { get; set; }
    public int? OfferedListingId { get; set; }
    public TradeStatus Status { get; set; }
    public string? Message { get; set; }
    public DateTime CreatedAt { get; set; }

    // Nuevos campos para TrueCoins
    public double? OfferedTrueCoins { get; set; }
    public double? RequestedTrueCoins { get; set; }
}
