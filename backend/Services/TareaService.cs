using BackGestionTareas.Models;
using BackGestionTareas.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackGestionTareas.Services
{
    public interface ITareaService
    {
        Task<List<TareaDto>> GetAllTareas();
        Task<TareaDto> GetTareaById(int id);
        Task AddTarea(TareaDto tarea);
        Task UpdateTarea(int id, UpdateTareaDto completada);
        Task DeleteTarea(int id);
    }
    public class TareaService : ITareaService
    {
        private readonly ITareaRepository _tareaRepository;
        public TareaService(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }
        public async Task<List<TareaDto>> GetAllTareas() => await _tareaRepository.GetAllTareas();
        public async Task<TareaDto> GetTareaById(int id) => await _tareaRepository.GetTareaById(id);
        public async Task AddTarea(TareaDto tarea) => await _tareaRepository.AddTarea(tarea);
        public async Task UpdateTarea(int id, UpdateTareaDto  completada)
        {
            var tarea = await _tareaRepository.GetTareaById(id);
            if (tarea != null)
            {
                tarea.Estado = completada.Estado;
                await _tareaRepository.UpdateTarea(tarea);
            }
        }
        public async Task DeleteTarea(int id) => await _tareaRepository.DeleteTarea(id);
    }
}
