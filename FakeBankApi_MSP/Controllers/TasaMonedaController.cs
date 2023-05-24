using Microsoft.AspNetCore.Mvc;
using MusicProAPI;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Net.Http;
using System.Text.Json;
using FakeBankApi_MSP.Modelos;

namespace FakeBankApi_MSP.Controllers
{
	[ApiController]
	[Route("TasaMoneda")]
	public class TasaMonedaController : Controller
	{
		GlobalMetods metods = new GlobalMetods();

		ObtenerTazaMoneda tasa;
		public string ApiKey = "f4b20e88262ba1da76d52a0c05475e26276151a5";

		[HttpGet]
		[Route("GetTasa")]
		public async Task<IActionResult> GetTasa()
		{
			//var val = await tasa.GetTasaDolar();

            string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/dolar?apikey=" + ApiKey + "&formato=json";

			var conection = new HttpClient();

			var response = await conection.GetAsync(url);

			string content = "";

			if (response.IsSuccessStatusCode)
			{
				content = await response.Content.ReadAsStringAsync();
			}

			var deserializedData = JsonConvert.DeserializeObject<TasaMoneda.ListDolar>(content);


            return Content(content);
		}


	}

	
}
