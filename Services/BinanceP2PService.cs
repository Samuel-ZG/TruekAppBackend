using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace TruekAppAPI.Services
{
    public class BinanceP2PService
    {
        private static readonly HttpClient client;

        static BinanceP2PService()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };
            client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; TruekAppBot/1.0)");
        }

        public async Task<decimal?> ObtenerPrecioUSDTaBOBAsync()
        {
            var url = "https://p2p.binance.com/bapi/c2c/v2/friendly/c2c/adv/search";

            var body = new
            {
                page = 1,
                rows = 10,
                payTypes = Array.Empty<string>(),
                asset = "USDT",
                fiat = "BOB",
                tradeType = "BUY"
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Respuesta cruda Binance P2P:");
            Console.WriteLine(responseBody);

            var jsonDoc = JsonNode.Parse(responseBody);

            var offers = jsonDoc?["data"]?.AsArray();

            if (offers == null || offers.Count == 0)
                return null;

            decimal sumaPrecios = 0;
            int contador = 0;

            foreach (var offer in offers)
            {
                var adv = offer?["adv"];
                if (adv != null)
                {
                    var priceStr = adv["price"]?.GetValue<string>();
                    if (decimal.TryParse(priceStr, out decimal price))
                    {
                        sumaPrecios += price;
                        contador++;
                    }
                }
            }

            if (contador == 0) return null;

            decimal precioPromedio = sumaPrecios / contador;

            return precioPromedio;
        }
    }
}
