using Microsoft.AspNetCore.Mvc;
using MusicProAPI.Modelos;
using MusicProAPI;
using Newtonsoft.Json;
using FolderERP.ANA.Model;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Net.Http;
using static FolderERP.ANA.Model.TasaMoneda;
using System.Text.Json;

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
			string url = "https://api.cmfchile.cl/api-sbifv3/recursos_api/dolar?apikey=" + ApiKey + "&formato=json";

			var conection = new HttpClient();

			var response = await conection.GetAsync(url);

			string content = "";

			if (response.IsSuccessStatusCode)
			{
				content = await response.Content.ReadAsStringAsync();
			}

			var deserializedData = JsonConvert.DeserializeObject<ListDolar>(content);

			return Content(content);
		}


	}

	
}
