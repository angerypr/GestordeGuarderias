namespace GestordeGuarderias.Application.DTOs
{
    public class ActividadDTO
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public required DateTime Fecha { get; set; }
        public required TimeSpan Hora { get; set; }
        public Guid GuarderiaId { get; set; }
    }

}