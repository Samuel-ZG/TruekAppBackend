namespace TruekAppAPI.DTO.Market;

public class BinanceP2PPriceDto
{
    public decimal Price { get; set; }
    public string Asset { get; set; } = "USDT";
    public string Fiat { get; set; } = "BOB";
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}