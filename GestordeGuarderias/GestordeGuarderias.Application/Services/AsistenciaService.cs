using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Domain.Interfaces;

namespace GestordeGuarderias.Application.Services
{
    public class AsistenciaService : IAsistenciaService
    {
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INinoRepository _ninoRepository;
        private readonly IGuarderiaRepository _guarderiaRepository;

        public AsistenciaService(
            IAsistenciaRepository asistenciaRepository,
            IUnitOfWork unitOfWork,
            INinoRepository ninoRepository,
            IGuarderiaRepository guarderiaRepository)
        {
            _asistenciaRepository = asistenciaRepository;
            _unitOfWork = unitOfWork;
            _ninoRepository = ninoRepository;
            _guarderiaRepository = guarderiaRepository;
        }

        public async Task<IEnumerable<AsistenciaDTO>> GetAllAsync()
        {
            var asistencias = await _asistenciaRepository.GetAllWithRelationsAsync();

            return asistencias.Select(a => new AsistenciaDTO
            {
                Id = a.Id,
                Fecha = a.Fecha,
                Presente = a.Presente,
                Nino = new NinoDTO
                {
                    Id = a.Nino.Id,
                    Nombre = a.Nino.Nombre,
                    Apellido = a.Nino.Apellido
                },
                Guarderia = new GuarderiaDTO
                {
                    Id = a.Guarderia!.Id,
                    Nombre = a.Guarderia.Nombre
                }
            });
        }
        public async Task<AsistenciaDTO?> GetByIdAsync(Guid id)
        {
            var asistencia = await _asistenciaRepository.GetByIdWithRelationsAsync(id);
            if (asistencia == null) return null;

            return new AsistenciaDTO
            {
                Id = asistencia.Id,
                Fecha = asistencia.Fecha,
                Presente = asistencia.Presente,
                Nino = new NinoDTO
                {
                    Id = asistencia.Nino.Id,
                    Nombre = asistencia.Nino.Nombre,
                    Apellido = asistencia.Nino.Apellido
                },
                Guarderia = new GuarderiaDTO
                {
                    Id = asistencia.Guarderia.Id,
                    Nombre = asistencia.Guarderia.Nombre
                }
            };
        }


        public async Task<AsistenciaDTO> CreateAsync(AsistenciaDTO dto)
        {
            var nino = await _ninoRepository.GetByIdAsync(dto.NinoId);
            var guarderia = await _guarderiaRepository.GetByIdAsync(dto.GuarderiaId);

            if (nino == null || guarderia == null)
                throw new Exception("Niño o Guardería no encontrada");

            var asistencia = new Asistencia
            {
                Id = Guid.NewGuid(),
                Fecha = dto.Fecha,
                Presente = dto.Presente,
                NinoId = dto.NinoId,
                GuarderiaId = dto.GuarderiaId,
                Nino = nino,
                Guarderia = guarderia
            };

            await _asistenciaRepository.AddAsync(asistencia);
            await _unitOfWork.CompleteAsync();

            return new AsistenciaDTO
            {
                Id = asistencia.Id,
                Fecha = asistencia.Fecha,
                Presente = asistencia.Presente,
                NinoId = asistencia.NinoId,
                GuarderiaId = asistencia.GuarderiaId
            };
        }

        public async Task<bool> UpdateAsync(Guid id, AsistenciaDTO dto)
        {
            var asistencia = await _asistenciaRepository.GetByIdAsync(id);
            if (asistencia == null) return false;

            asistencia.Fecha = dto.Fecha;
            asistencia.Presente = dto.Presente;

            await _asistenciaRepository.UpdateAsync(asistencia);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var asistencia = await _asistenciaRepository.GetByIdAsync(id);
            if (asistencia == null) return false;

            await _asistenciaRepository.DeleteAsync(asistencia);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
