using BackGestionTareas.Context;
using BackGestionTareas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackGestionTareas.Repositories
{
    public interface ITareaRepository
    {
        Task<TareaDto> GetTareaById(int id);
        Task<List<TareaDto>> GetAllTareas();
        Task AddTarea(TareaDto tarea);
        Task UpdateTarea(TareaDto tarea);
        Task DeleteTarea(int id);
    }

    public class TareaRepository : ITareaRepository
    {
        private readonly ApplicationDbContext _context;

        public TareaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TareaDto>> GetAllTareas() => await _context.Tarea.ToListAsync();
        public async Task<TareaDto> GetTareaById(int id) => await _context.Tarea.FindAsync(id);
        public async Task AddTarea(TareaDto tarea)
        {
            await _context.Tarea.AddAsync(tarea);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTarea(TareaDto tarea)
        {
            _context.Tarea.Update(tarea);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTarea(int id)
        {
            var tarea = await GetTareaById(id);
            if (tarea != null)
            {
                _context.Tarea.Remove(tarea);
                await _context.SaveChangesAsync();
            }
        }
    }
}
