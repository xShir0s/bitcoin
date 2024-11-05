using System.Net;
using System.Text.Json;

namespace bictoin
{

    public partial class MainPage : ContentPage
    {
        public class BitcoinRate
        {
            public string? code { get; set; } //Kod waluty 
            public string? description { get; set; } //Opis waluty 
            public double? rate_float { get; set; }
            //Kurs w formie liczbowej 
        }
        public class BitcoinRate2
        {
            public BitcoinRate? USD { get; set; }
            public BitcoinRate? GBP { get; set; }
            public BitcoinRate? EUR { get; set; }
        }
        public class Bitcoin
        {
            public string? chartName { get; set; }
            public BitcoinRate2 bpi { get; set; }
        }
        public class USD
        {
            public string? code { get; set; }
            public IList<Rate> rates { get; set; }
        }
        public class Rate
        {
            public double? ask { get; set; }
            public double? bid { get; set; }
        }
        public MainPage()
        {
            InitializeComponent();
            string json;
            double usd = 0; // Zmienna przechowująca kurs USD z API NBP 
            string dzis = DateTime.Now.ToString("yyyy-MM-dd");
            string urlUSD = "https://api.nbp.pl/api/exchangerates/rates/c/usd/" + dzis + "/?format=json";
            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(urlUSD);
            }
            USD dolar = JsonSerializer.Deserialize<USD>(json);
            usd = (double)dolar.rates[0].ask; // Pobranie kursu sprzedaży 
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            using (var webClient = new WebClient())
            {
                json = webClient.DownloadString(url);
            }
            Bitcoin bitcoin = JsonSerializer.Deserialize<Bitcoin>(json);
            lblUSD.Text = bitcoin.bpi.USD.code + ": " + bitcoin.bpi.USD.rate_float?.ToString("# ###.####");
            lblGBP.Text = bitcoin.bpi.GBP.code + ": " + bitcoin.bpi.GBP.rate_float?.ToString("# ###.####");
            lblEUR.Text = bitcoin.bpi.EUR.code + ": " + bitcoin.bpi.EUR.rate_float?.ToString("# ###.####");
            lblPLN.Text = "PLN: " + (usd * bitcoin.bpi.USD.rate_float)?.ToString("# ###.####");
            // Bitcoin bitcoin = JsonSerializer.Deserialize<Bitcoin>(json);
            //  lblUSD.Text = bitcoin.bpi.USD.code + ": " + bitcoin.bpi.USD.rate_float?.ToString("# ###.####");
            // lblUSD.Text = "";
        }


    }
}