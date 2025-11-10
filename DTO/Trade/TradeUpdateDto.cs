namespace TruekAppAPI.DTO.Trade
{
    public class TradeUpdateDto
    {
        public int OfferedListingId { get; set; }
        public int TargetListingId { get; set; }
        public string? Message { get; set; }

        // Nuevos campos opcionales para TrueCoins
        public double? OfferedTrueCoins { get; set; }
        public double? RequestedTrueCoins { get; set; }
    }
}