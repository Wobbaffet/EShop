using EShop.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EShop.WepApp.Service
{
    public class EshopService
    {
        public HttpClient Client { get; }

        public EshopService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:44387/api/customer");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client = client;
        }

        public async Task< List<Customer>> GetCustomers()
        {
            var response = await Client.GetAsync("");
            response.EnsureSuccessStatusCode();//da li je sdobro sve proslo
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Customer>>(responseStream);//??
            //ovo neki sablon, tako ona rekla

        }


    }
}
