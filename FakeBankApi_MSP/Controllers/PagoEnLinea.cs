using FakeBankApi_MSP.Modelos;
using Microsoft.AspNetCore.Mvc;
using MusicProAPI;
using RestSharp;

namespace FakeBankApi_MSP.Controllers
{
	[ApiController]
	[Route("PagoEnLinea")]
	public class PagoEnLinea
	{
		GlobalMetods metods = new GlobalMetods();

		[HttpPost]
		[Route("Pagar")]
		public dynamic Pagar(string Rut_Persona, string NumeroTarjeta, string Clavetarjeta, int Cvv, string NumeroCuenta, string Monto, string Moneda)
		{
			ObtenerTazaMoneda tasa = new ObtenerTazaMoneda();

			if (!metods.validarRut(Rut_Persona))
			{
				return new
				{
					mesage = "El formato de rut es invalido, formato requerido: 99999999-9"
				};
			}

			string[] listPersona = metods.getContentFile("Personas");

			bool PersonaEncontrado = false;

			for (int i = 0; i < listPersona.Count(); i++)
			{
				string[] splitArr = listPersona[i].Split("||");

				if (splitArr[0] == Rut_Persona)
				{
					PersonaEncontrado = true;
				}
			}

			if (!PersonaEncontrado)
			{
				return new
				{
					message = "La persona con rut '" + Rut_Persona + "' no existe en los registros",
				};
			}

			string[] listCuentas = metods.getContentFile("Cuentas");
			bool cuentaEncontrada = false;
			for (int i = 0; i < listCuentas.Count(); i++)
			{
				string[] splitArr = listCuentas[i].Split("||");

				if (splitArr[0] == Rut_Persona && splitArr[1] == NumeroCuenta)
				{
					if (!Convert.ToBoolean(splitArr[3]))
					{
						return new
						{
							mesage = "La cuenta '" + NumeroCuenta + "' se encuentra inhabilitada"
						};
					}

					cuentaEncontrada = true;
					break;
				}
			}

			if (!cuentaEncontrada)
			{
				return new
				{
					mesage = "La cuenta '" + NumeroCuenta + "' no existe en los registros"
				};
			}

			string[] listTarjetas = metods.getContentFile("Tarjetas");

			bool TarjetaEncontrada = false;

			for (int i = 0; i < listTarjetas.Count(); i++)
			{
				string[] splitArr = listTarjetas[i].Split("||");

				if (splitArr[0] == Rut_Persona && splitArr[1] == NumeroTarjeta && Convert.ToInt32(splitArr[2]) == Cvv && splitArr[3] == Clavetarjeta)
				{

				}

				TarjetaEncontrada = true;
			}

			if (!TarjetaEncontrada)
			{
				return new
				{
					mesage = "La tarjeta '" + NumeroTarjeta + "' no existe en los registros"
				};
			}

			FondosCuenta fondos = new FondosCuenta();
			fondos.NumeroCuenta = NumeroCuenta;

			string[] list = metods.getContentFile("FondosCuentas");

			List<string> content = new List<string>();

			bool encontrado = false;

			for (int i = 0; i < list.Count(); i++)
			{
				string[] splitArr = list[i].Split("||");

				if (splitArr[0] == NumeroCuenta)
				{
					if (Convert.ToInt32(splitArr[1] == "" ? "0" : splitArr[1]) == 0)
					{
						return new
						{
							message = "No hay fondos en la cuenta"
						};
					}
					else if (Convert.ToInt64(Monto) > Convert.ToInt64(splitArr[1] == "" ? "0" : splitArr[1]))
					{
						return new
						{
							message = "Saldo insuficinte en la cuenta"
						};
					}

					long MontoFinal = 0;

					if (Moneda != "peso")
					{
						var MontoConvert = metods.ConvertionOfMoney(Moneda, Monto);

						MontoFinal = Convert.ToInt32(MontoConvert);
					}
					else
					{
						MontoFinal = Convert.ToInt64(Monto);
					}

					fondos.FondosCuentaBancaria = (Convert.ToInt64(splitArr[1] == "" ? "0" : splitArr[1]) - Convert.ToInt64(MontoFinal)).ToString();
					content.Add(String.Format("{0}||{1}", fondos.NumeroCuenta, fondos.FondosCuentaBancaria));
					encontrado = true;
					break;
				}

				content.Add(list[i]);
			}

			if (!encontrado)
			{
				return new
				{
					message = "No hay fondos en la cuenta"
				};
			}
			else
			{
				metods.updateLineFile("FondosCuentas", content);

				metods.saveLineFile("Transacciones", String.Format("{0}||{1}||{2}||{3}||{4}", Rut_Persona, NumeroCuenta, Monto, "Abono", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")));
			}

			return new
			{
				message = "Se ha efectuado el pago correctamente"
			};
		}
	}
}
