using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackGestionTareas.Controllers;
using BackGestionTareas.Models;
using BackGestionTareas.Services;
using System.Threading;
using FluentValidation;
using FluentValidation.Results;

public class TareasControllerTests
{
    private readonly Mock<ITareaService> _mockService;
    private readonly Mock<IValidator<TareaDto>> _mockValidator;
    private readonly TareasController _controller;

    public TareasControllerTests()
    {
        _mockService = new Mock<ITareaService>();
        _mockValidator = new Mock<IValidator<TareaDto>>();
        _controller = new TareasController(_mockService.Object, _mockValidator.Object);
    }

    [Fact]
    public async Task GetTareas_DeberiaRetornarListaDeTareas()
    {
        // Arrange
        var tareas = new List<TareaDto> { new TareaDto { Id = 2, Nombre = "Test", Estado=false } };
        _mockService.Setup(service => service.GetAllTareas()).ReturnsAsync(tareas);

        // Act
        var resultado = await _controller.GetTareas();

        // Assert
        resultado.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetTarea_DeberiaRetornarTareaSiExiste()
    {
        // Arrange
        var tarea = new TareaDto { Id = 2, Nombre = "Test", Estado = false };
        _mockService.Setup(service => service.GetTareaById(2)).ReturnsAsync(tarea);

        // Act
        var resultado = await _controller.GetTareaById(2);

        // Assert
        //Assert.NotNull(resultado.Result);
        resultado.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetTarea_DeberiaRetornarNotFoundSiNoExiste()
    {
        // Arrange
        _mockService.Setup(service => service.GetTareaById(1)).ReturnsAsync((TareaDto)null);

        // Act
        var resultado = await _controller.GetTareaById(999);

        // Assert
        resultado.Result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task PostTarea_DeberiaRetornarCreatedAtAction()
    {
        // Arrange
        var nuevaTarea = new TareaDto { Nombre = "Nueva Tarea", Estado = false };
        _mockValidator.Setup(v => v.ValidateAsync(It.IsAny<TareaDto>(), default)).ReturnsAsync(new ValidationResult());
        _mockService.Setup(service => service.AddTarea(It.IsAny<TareaDto>()))
            .ReturnsAsync((TareaDto tarea) => new TareaDto { Id = 2, Nombre = tarea.Nombre, Estado = tarea.Estado });


        // Act
        var resultado = await _controller.PostTarea(nuevaTarea);
        resultado.Should().NotBeNull();
        // Assert
        resultado.Result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task PatchTarea_DeberiaRetornarNoContent()
    {
        // Arrange
        var updateDto = new UpdateTareaDto { Estado = true };
        _mockService.Setup(service => service.UpdateTarea(1, It.IsAny<UpdateTareaDto>())).Returns(Task.CompletedTask);

        // Act
        var resultado = await _controller.UpdateTarea(1, updateDto);

        // Assert
        resultado.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteTarea_DeberiaRetornarNoContentSiExiste()
    {
        // Arrange
        _mockService.Setup(service => service.DeleteTarea(1)).Returns(Task.CompletedTask);

        // Act
        var resultado = await _controller.DeleteTarea(1);

        // Assert
        resultado.Should().BeOfType<NoContentResult>();
    }
}
