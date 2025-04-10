namespace GestordeGuarderias.Domain.Entities
{
    public class Actividad 
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public required DateTime Fecha { get; set; }
        public required TimeSpan Hora { get; set; }
        public int GuarderiaId { get; set; }
        public required Guarderia Guarderia { get; set; }
        public ICollection<ActividadNino> ActividadesNinos { get; set; } = new List<ActividadNino>();

    }

}