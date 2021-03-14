using CommonLib.Produce;
using CommonLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GoGreen.Options;
using Microsoft.Extensions.Options;

namespace CommonLib.Services
{
    public class GreenAPIService : IProduceService
    {
        public HttpClient _client { get; }

        public GreenAPIService(HttpClient client, IOptionsMonitor<GreenAPIOptions> options)
        {
            try
            {
                client.BaseAddress = new Uri(options.Get("GreenAPI").BaseAddress);

                client.DefaultRequestHeaders.Add("Accept", "text/plain");
            }
            catch (OptionsValidationException)
            {

            }

            _client = client;
        }

        public async Task<IEnumerable<ProduceDTO>> GetAll()
        {
            HttpResponseMessage response = await _client.GetAsync(
                "/Produce/GetAll");

            response.EnsureSuccessStatusCode();

            using Stream responseStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync
                <IEnumerable<ProduceDTO>>(responseStream);
        }

        public async Task<ProduceDTO> Get(int Id)
        {
            HttpResponseMessage response = await _client.GetAsync(
                $"/Produce/{Id}");

            response.EnsureSuccessStatusCode();

            using Stream responseStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync
                <ProduceDTO>(responseStream);
        }

        public async Task<ProduceDTO> Save(ProduceDTO produce)
        {
            StringContent produceJson = new StringContent(
               JsonSerializer.Serialize(produce),
               Encoding.UTF8,
               "application/json");

            HttpResponseMessage response =
                await _client.PostAsync($"/Produce", produceJson);

            response.EnsureSuccessStatusCode();

            using Stream responseStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync
                <ProduceDTO>(responseStream);
        }

        public async Task<int> Delete(int id)
        {
            HttpResponseMessage response =
                await _client.DeleteAsync($"/Produce/{id}");

            response.EnsureSuccessStatusCode();

            using Stream responseStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync
                <int>(responseStream);
        }
    }
}