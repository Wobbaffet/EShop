using EShop.WepApp.APIHelpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EShop.WepApp.Services
{
    public class EShopServices
    {
        public HttpClient Client { get; set; }
        public EShopServices(HttpClient client)
        {
            client.BaseAddress = new Uri("https://www.googleapis.com/books/v1/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client = client;
        }
        public async Task<MainClass> GetBooksFromAPI(string name)
        {
            if (name == null)
                name = "ASP.NET";

            var response = await Client.GetStringAsync($"volumes?q={name}&fields=items.volumeInfo");
            
            return JsonConvert.DeserializeObject<MainClass>(response);
        }
    }
}
