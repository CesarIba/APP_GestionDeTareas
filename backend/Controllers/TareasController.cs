﻿using BackGestionTareas.Context;
using BackGestionTareas.Models;
using BackGestionTareas.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackGestionTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ITareaService _tareaService;
        private readonly ApplicationDbContext _context;

        public TareasController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }
        [HttpPost]
        public async Task<ActionResult<TareaDto>> PostTarea([FromBody] TareaDto tarea)
        {
            await _tareaService.AddTarea(tarea);
            return CreatedAtAction(nameof(GetTareaById), new { id = tarea.Id }, tarea);
        }

        [HttpGet]
        public async Task<ActionResult<List<TareaDto>>> GetTareas()
        {
            return await _tareaService.GetAllTareas();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TareaDto>> GetTareaById(int id)
        {
            var tarea = await _tareaService.GetTareaById(id);
            if (tarea == null) return NotFound();

            return tarea;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTarea(int id, [FromBody] UpdateTareaDto tareaDto)
        {
            await _tareaService.UpdateTarea(id, tareaDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            await _tareaService.DeleteTarea(id);
            return NoContent();
        }        
    }
}
