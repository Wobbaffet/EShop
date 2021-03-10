using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EShop.WepApp.Services
{
    public class EShopServices
    {
        public HttpClient Client { get; set; }
        public EShopServices(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:44328/api/customer");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client = client;
        }

        public async Task<string> GetView()
        {
            var response = await Client.GetAsync("");
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<string>(responseStream);
        }
    }
}
