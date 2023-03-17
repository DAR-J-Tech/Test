using Newtonsoft.Json;
using PruebaPractica.Models;

namespace PruebaPractica.Services
{
    public class ApiService : IApiService
    {
        #region Constructor
        private static string baseUrl;

        public ApiService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }
        #endregion

        #region Buscador
        public async Task<List<Buscador>> GetBuscador()
        {                        
            List<Buscador> lista = new List<Buscador>();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            var response = await cliente.GetAsync("v1/us/daily.json");
            if (response.IsSuccessStatusCode)
            {
                var jsonRes = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                var result = JsonConvert.DeserializeObject<List<Buscador>>(jsonRes);
                foreach (var item in result)
                {
                    lista.Add(item);
                }
            }
            return lista;
        }
        #endregion

        #region Ciudadano
        public async Task<List<Ciudadano>> GetCiudadano()
        {
            List<Ciudadano> lista = new List<Ciudadano>();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(baseUrl);
            var response = await cliente.GetAsync("v1/states/daily.json");
            if (response.IsSuccessStatusCode)
            {
                var jsonRes = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Ciudadano>>(jsonRes);
                foreach (var item in result)
                {
                    lista.Add(item);
                }
            }
            return lista;
        }

        #endregion
    }
}
