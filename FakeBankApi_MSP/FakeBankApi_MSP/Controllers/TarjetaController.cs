using Microsoft.AspNetCore.Mvc;
using MusicProAPI.Modelos;

namespace MusicProAPI.Controllers
{
	[ApiController]
	[Route("Tarjeta")]
	public class TarjetaController
	{
		GlobalMetods metods = new GlobalMetods();

		[HttpGet]
		[Route("GetTarjetas")]
		public dynamic GetTarjetas()
		{
			string[] list = metods.getContentFile("Tarjetas");

			if (list.Count() == 0)
			{
				return new
				{
					mesage = "No hay tarjetas registradas"
				};
			}

			List<Tarjeta> tarjetas = new List<Tarjeta>();

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");
				Tarjeta tarjeta = new Tarjeta();

				tarjeta.Rut_Persona = Convert.ToInt32(splitArr[0]);
				tarjeta.NumeroTarjeta = Convert.ToInt32(splitArr[1]);
				tarjeta.NumeroCuenta = Convert.ToInt32(splitArr[2]);

				tarjetas.Add(tarjeta);
			}

			return tarjetas;
		}

		[HttpGet]
		[Route("GetTarjetasPersona")]
		public dynamic GetTarjetasPersona(int Rut_Persona)
		{
			string[] list = metods.getContentFile("Tarjetas");

			if (list.Count() == 0)
			{
				return new
				{
					mesage = "No hay tarjetas registradas"
				};
			}

			string[] listUsuarios = metods.getContentFile("Usuarios");

			bool userEncontrada = false;

			for (int i = 0; i < listUsuarios.Count(); i++)
			{
				string[] splitArr = listUsuarios[i].Split("||");

				if (Convert.ToInt32(splitArr[0]) == Rut_Persona)
				{
					userEncontrada = true;
				}
			}

			if (!userEncontrada)
			{
				return new
				{
					message = "La persona con rut '" + Rut_Persona + "' no existe en los registros",
				};
			}

			List<Tarjeta> tarjetas = new List<Tarjeta>();

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (Convert.ToInt32(splitArr[0]) == Rut_Persona)
				{
					Tarjeta tarjeta = new Tarjeta();

					tarjeta.Rut_Persona = Convert.ToInt32(splitArr[0]);
					tarjeta.NumeroTarjeta = Convert.ToInt32(splitArr[1]);
					tarjeta.NumeroCuenta = Convert.ToInt32(splitArr[2]);

					tarjetas.Add(tarjeta);
				}
			}

			return tarjetas;
		}

		[HttpGet]
		[Route("GetTarjeta")]
		public dynamic GetTarjeta(int Rut_Persona, int NumeroTarjeta)
		{
			string[] list = metods.getContentFile("Tarjetas");

			if (list.Count() == 0)
			{
				return new
				{
					mesage = "No hay tarjetas registradas"
				};
			}

			string[] listUsuarios = metods.getContentFile("Usuarios");

			bool userEncontrada = false;

			for (int i = 0; i < listUsuarios.Count(); i++)
			{
				string[] splitArr = listUsuarios[i].Split("||");

				if (Convert.ToInt32(splitArr[0]) == Rut_Persona)
				{
					userEncontrada = true;
				}
			}

			if (!userEncontrada)
			{
				return new
				{
					message = "La persona con rut '" + Rut_Persona + "' no existe en los registros",
				};
			}

			Tarjeta tarjeta = new Tarjeta();

			bool encontrado = false;

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (Convert.ToInt32(splitArr[0]) == Rut_Persona && Convert.ToInt32(splitArr[1]) == NumeroTarjeta)
				{
					tarjeta.Rut_Persona = Convert.ToInt32(splitArr[0]);
					tarjeta.NumeroTarjeta = Convert.ToInt32(splitArr[1]);
					tarjeta.NumeroCuenta = Convert.ToInt32(splitArr[2]);

					encontrado = true;
					break;
				}
			}

			if (!encontrado)
			{
				return new
				{
					mesage = "La tarjeta '" + NumeroTarjeta + "' no existe en los registros"
				};
			}

			return tarjeta;
		}

		[HttpPost]
		[Route("CrearTarjeta")]
		public dynamic CrearTarjeta(int Rut_Persona, int NumeroTarjeta, int NumeroCuenta)
		{
			string[] listUsuarios = metods.getContentFile("Usuarios");

			bool userEncontrada = false;

			for (int i = 0; i < listUsuarios.Count(); i++)
			{
				string[] splitArr = listUsuarios[i].Split("||");

				if (Convert.ToInt32(splitArr[0]) == Rut_Persona)
				{
					userEncontrada = true;
				}
			}

			if (!userEncontrada)
			{
				return new
				{
					message = "La persona con rut '" + Rut_Persona + "' no existe en los registros",
				};
			}

			Tarjeta tarjeta = new Tarjeta();
			tarjeta.Rut_Persona = Rut_Persona;
			tarjeta.NumeroTarjeta = NumeroTarjeta;
			tarjeta.NumeroCuenta = NumeroCuenta;

			metods.saveLineFile("Tarjetas", String.Format("{0}||{1}||{2}", tarjeta.Rut_Persona, tarjeta.NumeroTarjeta, tarjeta.NumeroCuenta));

			return new
			{
				message = "Tarjeta registrada",
				result = tarjeta
			};
		}

		[HttpPut]
		[Route("ModificarTarjeta")]
		public dynamic ModificarTarjeta(int Rut_Persona, int NumeroTarjeta, int NumeroCuenta)
		{
			string[] list = metods.getContentFile("Tarjetas");

			if (list.Count() == 0)
			{
				return new
				{
					mesage = "No hay categorias registradas"
				};
			}

			string[] listUsuarios = metods.getContentFile("Usuarios");

			bool userEncontrada = false;

			for (int i = 0; i < listUsuarios.Count(); i++)
			{
				string[] splitArr = listUsuarios[i].Split("||");

				if (Convert.ToInt32(splitArr[0]) == Rut_Persona)
				{
					userEncontrada = true;
				}
			}

			if (!userEncontrada)
			{
				return new
				{
					message = "La persona con rut '" + Rut_Persona + "' no existe en los registros",
				};
			}

			bool encontrado = false;
			List<string> content = new List<string>();
			Tarjeta tarjeta = new Tarjeta();

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (Convert.ToInt32(splitArr[0]) == Rut_Persona)
				{

					content.Add(String.Format("{0}||{1}||{2}", Rut_Persona, NumeroTarjeta == 0 ? splitArr[1] : NumeroTarjeta, NumeroCuenta == 0 ? splitArr[2] : NumeroCuenta));

					encontrado = true;

					tarjeta.Rut_Persona = Rut_Persona;
					tarjeta.NumeroTarjeta = NumeroTarjeta;
					tarjeta.NumeroCuenta = NumeroCuenta;

					continue;
				}

				content.Add(list[i]);
			}

			if (!encontrado)
			{
				return new
				{
					mesage = "La tarjeta '" + NumeroTarjeta + "' no existe en los registros"
				};
			}

			metods.updateLineFile("Tarjetas", content);

			return new
			{
				mesage = "Tarjeta modificada",
				result = tarjeta
			};
		}

		[HttpDelete]
		[Route("EliminarTarjeta")]
		public dynamic EliminarTarjeta(int NumeroCuenta)
		{
			string[] list = metods.getContentFile("Tarjetas");

			if (list.Count() == 0)
			{
				return new
				{
					mesage = "No hay categorias registrados"
				};
			}

			bool encontrado = false;
			List<string> content = new List<string>();

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (Convert.ToInt32(splitArr[1]) != NumeroCuenta)
				{
					content.Add(list[i]);
				}
				else
				{
					encontrado = true;
				}
			}

			if (!encontrado)
			{
				return new
				{
					mesage = "La tarjeta '" + NumeroCuenta + "' no existe en los registros"
				};
			}

			metods.updateLineFile("Tarjetas", content);

			return new
			{
				mesage = "La tarjeta '" + NumeroCuenta + "' fue eliminado exitosamente"
			};
		}

	}
}
