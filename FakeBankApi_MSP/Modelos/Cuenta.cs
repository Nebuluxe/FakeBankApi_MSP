namespace FakeBankApi_MSP.Modelos
{
	public class Cuenta
	{
		public string Rut_Persona { get; set; } = string.Empty;
		public string NumeroCuenta { get; set; } = string.Empty;
		public string FechaCreacion { get; set; } = string.Empty;
		public bool Activa { get; set; }
	}
}
