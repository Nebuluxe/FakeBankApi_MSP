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

		private readonly IHttpClientFactory httpClientFactory;

		public ObtenerTazaMoneda(IHttpClientFactory httpClientFactory)
		{
			this.httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> GetTasaEuro()
		{
			var httpClient = httpClientFactory.CreateClient();

			string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/euro?apikey=" + ApiKey + "&formato=json";

			HttpResponseMessage response = await httpClient.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				string responseData = await response.Content.ReadAsStringAsync();
				return Content(responseData, "application/json");
			}
			else
			{
				return View("Error");
			}
		}

		public async Task<IActionResult> GetTasaDolar()
		{
			string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/euro?apikey=" + ApiKey + "&formato=json";

			var conection = new HttpClient();

			var response = await conection.GetAsync(url);

			string content = "";

			if (response.IsSuccessStatusCode)
			{
				content = await response.Content.ReadAsStringAsync();
			}

			return Content(content);
		}

		public async Task<IActionResult> GetTasaUF()
		{
			var httpClient = httpClientFactory.CreateClient();

			string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/uf?apikey=" + ApiKey + "&formato=json";

			HttpResponseMessage response = await httpClient.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				string responseData = await response.Content.ReadAsStringAsync();
				return Content(responseData, "application/json");
			}
			else
			{
				return View("Error");
			}
		}
	}

}
