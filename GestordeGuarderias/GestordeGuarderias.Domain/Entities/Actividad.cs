namespace GestordeGuarderias.Domain.Entities
{
    public class Actividad 
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public required DateTime Fecha { get; set; }
        public required TimeSpan Hora { get; set; }
        public Guid GuarderiaId { get; set; }
        public Guarderia? Guarderia { get; set; }
    }

}