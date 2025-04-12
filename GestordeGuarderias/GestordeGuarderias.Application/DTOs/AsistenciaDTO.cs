namespace GestordeGuarderias.Application.DTOs
{
    public class AsistenciaDTO
    {
        public Guid Id { get; set; }
        public required bool Presente { get; set; }
        public required DateTime Fecha { get; set; }
        public required Guid NinoId { get; set; }
        public required Guid GuarderiaId { get; set; }
    }
}