using FakeBankApi_MSP.Modelos;
using System.IO;

namespace MusicProAPI
{
	public class GlobalMetods
	{
		public string[] getContentFile(string NomDoc)
		{
			if (!Directory.Exists("C:\\txtFakeBank"))
			{
				Directory.CreateDirectory("C:\\txtFakeBank");
			}

			if (!System.IO.File.Exists("C:\\txtFakeBank\\" + NomDoc + ".txt"))
			{
				System.IO.File.Create("C:\\txtFakeBank\\" + NomDoc + ".txt").Close();
			}

			return System.IO.File.ReadAllLines("C:\\txtFakeBank\\" + NomDoc + ".txt");
		}

		public void saveLineFile(string NomDoc, string lineContent)
		{
			if (!Directory.Exists("C:\\txtFakeBank"))
			{
				Directory.CreateDirectory("C:\\txtFakeBank");
			}

			if (!System.IO.File.Exists("C:\\txtFakeBank\\" + NomDoc + ".txt"))
			{
				System.IO.File.Create("C:\\txtFakeBank\\" + NomDoc + ".txt").Close();
			}

			StreamWriter sw = System.IO.File.AppendText("C:\\txtFakeBank\\" + NomDoc + ".txt");
			sw.WriteLine(lineContent);
			sw.Close();
		}

		public void updateLineFile(string NomDoc, List<string> content)
		{
			if (!Directory.Exists("C:\\txtFakeBank"))
			{
				Directory.CreateDirectory("C:\\txtFakeBank");
			}

			if (!System.IO.File.Exists("C:\\txtFakeBank\\" + NomDoc + ".txt"))
			{
				System.IO.File.Create("C:\\txtFakeBank\\" + NomDoc + ".txt").Close();
			}

			System.IO.File.WriteAllLines("C:\\txtFakeBank\\" + NomDoc + ".txt", content);
		}

		public bool validarRut(string rut)
		{

			bool validacion = false;
			string saveOriginalRut = rut;
			try
			{
				rut = rut.ToUpper();
				rut = rut.Replace(".", "");
				rut = rut.Replace("-", "");
				int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

				char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

				int m = 0, s = 1;
				for (; rutAux != 0; rutAux /= 10)
				{
					s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
				}
				if (dv == (char)(s != 0 ? s + 47 : 75))
				{
					validacion = true;
				}

				string format = saveOriginalRut.Substring(saveOriginalRut.Length - 2, 1);
				if (format != "-")
				{
					validacion = false;
				}

			}
			catch (Exception)
			{
			}
			return validacion;
		}

		public string formatearRut(string rut)
		{
			int cont = 0;
			string format;
			if (rut.Length == 0)
			{
				return "";
			}
			else
			{
				rut = rut.Replace(".", "");
				rut = rut.Replace("-", "");
				format = "-" + rut.Substring(rut.Length - 1);
				for (int i = rut.Length - 2; i >= 0; i--)
				{
					format = rut.Substring(i, 1) + format;
					cont++;
					if (cont == 3 && i != 0)
					{
						format = "." + format;
						cont = 0;
					}
				}
				return format;
			}
		}

		public bool ValidarFormatoFecha(string fecha)
		{
			string formato = "dd/MM/yyyy"; 
			DateTime fechaValidada;

			if (DateTime.TryParseExact(fecha, formato, null, System.Globalization.DateTimeStyles.None, out fechaValidada))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}
