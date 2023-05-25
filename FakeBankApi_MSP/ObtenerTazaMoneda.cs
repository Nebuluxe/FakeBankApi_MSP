using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FakeBankApi_MSP
{   //apikey: e361f12c3cbd381e9e14e9a5a9b3b68d5f1dcb64, solicitante del ApiKey Joaquin Diaz, Pagina de api, 'https://api.cmfchile.cl/documentacion/UF.html' otra ocion 'https://api.sbif.cl/api/contactanos.jsp'
	//se puede solicitar otro ApiKey ingresando a la documentacion de esat api 

	//UF en el maestro de monedas: SincronizarMoneda = 1

	//public string key = "f4b20e88262ba1da76d52a0c05475e26276151a5";

	//string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/euro?apikey=" + ApiKey + "&formato=json";
	//string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/dolar?apikey=" + ApiKey + "&formato=json";
	//string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/uf?apikey=" + ApiKey + "&formato=json";

	public class ObtenerTazaMoneda : Controller
	{
		public string ApiKey = "f4b20e88262ba1da76d52a0c05475e26276151a5";

		public async Task<string> GetTasaEuro()
		{
            var httpClient = new HttpClient();

            string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/euro?apikey=" + ApiKey + "&formato=json";

            HttpResponseMessage response = await httpClient.GetAsync(url);

            string responseData = "";

            if (response.IsSuccessStatusCode)
            {
                responseData = await response.Content.ReadAsStringAsync();
            }

            return responseData;
        }

		public async Task<string> GetTasaDolar()
		{
            var httpClient = new HttpClient();

            string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/dolar?apikey=" + ApiKey + "&formato=json";

            var response = await httpClient.GetAsync(url);

            string responseData = "";

            if (response.IsSuccessStatusCode)
            {
                responseData = await response.Content.ReadAsStringAsync();
            }

            return responseData;
        }

		public async Task<string> GetTasaUF()
		{
            var httpClient = new HttpClient();

            string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/uf?apikey=" + ApiKey + "&formato=json";

			HttpResponseMessage response = await httpClient.GetAsync(url);

			string responseData = "";

            if (response.IsSuccessStatusCode)
			{
				responseData = await response.Content.ReadAsStringAsync();
			}

            return responseData;
		}
	}

}
