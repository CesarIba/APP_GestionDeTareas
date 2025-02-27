using Xunit;
using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Mvc;
using BackGestionTareas.Controllers;
using BackGestionTareas.Services;
using System.Threading.Tasks;

public class TareasControllerTests
{
    [Fact]
    public async Task EliminarTarea_DeberiaRetornarNoContent_SiExisteAsync()
    {
        // Arrange
        var mockService = new Mock<ITareaService>();
        int tareaId = 1;
        mockService.Setup(service => service.DeleteTarea(tareaId));

        var controller = new TareasController(mockService.Object);

        // Act
        var resultado = await controller.DeleteTarea(tareaId);

        // Assert
        resultado.Should().BeOfType<NoContentResult>();
    }
}
