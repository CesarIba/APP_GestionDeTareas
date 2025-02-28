using BackGestionTareas.Models;
using BackGestionTareas.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackGestionTareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ITareaService _tareaService;
        private readonly IValidator<TareaDto> _validator;

        public TareasController(ITareaService tareaService, IValidator<TareaDto> validator)
        {
            _tareaService = tareaService;
            _validator = validator;
        }
        [HttpPost]
        public async Task<ActionResult<TareaDto>> PostTarea([FromBody] TareaDto nuevaTarea)
        {
            var validationResult = await _validator.ValidateAsync(nuevaTarea);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new
                {
                    propertyName = e.PropertyName,
                    errorMessage = e.ErrorMessage
                });

                return BadRequest(new { message = "Errores de validación", errors });
            }

            var tarea = await _tareaService.AddTarea(nuevaTarea);
            return CreatedAtAction(nameof(GetTareaById), new { id = tarea.Id }, tarea);
        }

        [HttpGet]
        public async Task<ActionResult<List<TareaDto>>> GetTareas()
        {
            var tareas = await _tareaService.GetAllTareas();
            return Ok(tareas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TareaDto>> GetTareaById(int id)
        {
            var tarea = await _tareaService.GetTareaById(id);
            if (tarea == null) 
                return NotFound($"No se encontro la tarea");

            return Ok(tarea);
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

        [HttpGet("error")]
        public IActionResult LanzarError()
        {
            throw new Exception("Este es un error de prueba.");
        }

        [HttpPost("validar")]
        public IActionResult Validar([FromBody] TareaDto dto, IValidator<TareaDto> validator)
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return Ok("Validación exitosa");
        }
    }
}
