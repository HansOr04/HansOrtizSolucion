using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using APPConsultasHansOrtiz.Models;

namespace APPConsultasHansOrtiz.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string API_URL = "https://localhost:7019";

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(API_URL)
            };
        }

        public async Task<List<HO_Contacto>> GetAllContactos()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/HO_Contacto");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    };
                    return JsonSerializer.Deserialize<List<HO_Contacto>>(content, options) ?? new List<HO_Contacto>();
                }
                throw new Exception($"Error al obtener contactos: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio: {ex.Message}");
            }
        }

        public async Task<HO_Contacto> GetContactoById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/HO_Contacto/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<HO_Contacto>(content);
                }
                throw new Exception($"Error al obtener el contacto: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio: {ex.Message}");
            }
        }

        public async Task<HO_Contacto> CreateContacto(HO_Contacto contacto)
        {
            try
            {
                var json = JsonSerializer.Serialize(contacto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/HO_Contacto", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<HO_Contacto>(responseContent);
                }
                throw new Exception($"Error al crear el contacto: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio: {ex.Message}");
            }
        }

        public async Task<bool> UpdateContacto(int id, HO_Contacto contacto)
        {
            try
            {
                var json = JsonSerializer.Serialize(contacto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"/api/HO_Contacto/{id}", content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio: {ex.Message}");
            }
        }

        public async Task<bool> DeleteContacto(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/HO_Contacto/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el servicio: {ex.Message}");
            }
        }
    }
}