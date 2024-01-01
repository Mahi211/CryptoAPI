
using CryptoAPI;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Web;

try
{
    Console.WriteLine(bitcoinAPICall());
}
    catch (WebException e)
{
    Console.WriteLine(e.Message);
}

    static string bitcoinAPICall()
    {
        var endpoint = new Uri("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest"); // valde listings mest för test

        var queryString = HttpUtility.ParseQueryString(string.Empty); // dessa querystrings används ej just nu de kom direkt från APIDokumentationen
        queryString["start"] = "1";
        queryString["limit"] = "100";
        queryString["convert"] = "USD";

        // endpoint.Query = queryString.ToString();
        string API_KEY = "63f9b42b-7067-41cf-803e-00025ed9b664"; // min egna api key har 10000 kostnadsfria calls per månad
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", API_KEY);
        client.DefaultRequestHeaders.Add("Accepts", "application/json");
        var result = client.GetAsync(endpoint).Result;
        var json = result.Content.ReadAsStringAsync().Result;
        var listings = JsonSerializer.Deserialize<Listings>(json);
        var bitcoinValue = listings.data[0].quote.USD.price.ToString(); // får fram real bitcoin data i USD valuta.
        return bitcoinValue;

        
    }

      // Börjat arbeta med denna api för bitcoin tracking detta är bara början så fick ut bitcoinvärdet för att testa så att allt
      // la även över json data till classen Listings planerar att antingen göra egen formula för trendiga kryptovalutor eller ta en formula
      // och sedan genom datat få fram köpvärda kryptovalutor eller vissa man bör hålla koll på.