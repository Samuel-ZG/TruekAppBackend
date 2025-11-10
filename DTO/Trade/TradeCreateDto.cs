using System.ComponentModel.DataAnnotations;

namespace TruekAppAPI.DTO.Trade;

public class TradeCreateDto
{
    [Required] public int TargetListingId { get; set; }
    public int? OfferedListingId { get; set; }
    public string? Message { get; set; }

    // Nuevos campos opcionales para TrueCoins
    public double? OfferedTrueCoins { get; set; }
    public double? RequestedTrueCoins { get; set; }
}