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

public class TareasControllerTests
{
    private readonly Mock<ITareaService> _mockService;
    private readonly TareasController _controller;

    public TareasControllerTests()
    {
        _mockService = new Mock<ITareaService>();
        _controller = new TareasController(_mockService.Object);
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
        var resultado = await _controller.GetTareaById(1);

        // Assert
        resultado.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task PostTarea_DeberiaRetornarCreatedAtAction()
    {
        // Arrange
        var nuevaTarea = new TareaDto { Nombre = "Nueva Tarea" };
        _mockService.Setup(service => service.AddTarea(nuevaTarea)).Returns(Task.CompletedTask);

        // Act
        var resultado = await _controller.PostTarea(nuevaTarea);

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
