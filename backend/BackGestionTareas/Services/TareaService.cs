using BackGestionTareas.Models;
using BackGestionTareas.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackGestionTareas.Services
{
    public interface ITareaService
    {
        Task<List<TareaDto>> GetAllTareas();
        Task<TareaDto> GetTareaById(int id);
        Task<TareaDto> AddTarea(TareaDto tarea);
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
        public async Task<TareaDto> AddTarea(TareaDto nuevaTarea)
        {
            var tarea = new TareaDto
            {
                Nombre = nuevaTarea.Nombre,
                Estado = nuevaTarea.Estado
            };

            await _tareaRepository.AddTarea(tarea);

            return new TareaDto
            {
                Id = tarea.Id,
                Nombre = tarea.Nombre,
                Estado = tarea.Estado
            };
        }
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
