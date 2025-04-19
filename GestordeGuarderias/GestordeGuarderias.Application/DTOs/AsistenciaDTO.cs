namespace GestordeGuarderias.Application.DTOs
{
    public class AsistenciaDTO
    {
        public Guid Id { get; set; }
        public required bool Presente { get; set; }
        public required DateTime Fecha { get; set; }
        public Guid NinoId { get; set; }
        public NinoDTO? Nino { get; set; }
        public Guid GuarderiaId { get; set; }
        public GuarderiaDTO? Guarderia { get; set; }
    }
}