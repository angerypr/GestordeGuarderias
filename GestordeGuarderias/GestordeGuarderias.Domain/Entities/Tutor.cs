namespace GestordeGuarderias.Domain.Entities
{
    public class Tutor 
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Telefono { get; set; }
        public required string Cedula { get; set; }
        public required string CorreoElectronico { get; set; }
        public required ICollection<Nino> Ninos { get; set; } = new List<Nino>();
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}