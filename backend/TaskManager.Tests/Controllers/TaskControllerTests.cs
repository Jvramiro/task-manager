using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManager.API.Controllers;
using TaskManager.API.DTO;
using TaskManager.API.Models;
using TaskManager.API.Repositories;
using Xunit;

namespace TaskManager.Tests.Controllers;

public class TaskControllerTests
{
    private readonly Mock<ITaskRepository> mockRepo;
    private readonly TaskController controller;
    private readonly TaskItem mockTask;
    private readonly List<TaskItem> mockTasks;
    private readonly TaskCreateDTO mockTaskCreateDTO;
    private readonly TaskUpdateDTO mockTaskUpdateDTO;

    public TaskControllerTests()
    {
        mockRepo = new Mock<ITaskRepository>();
        controller = new TaskController(mockRepo.Object);

        mockTask = new TaskItem()
        {
            Id = 1,
            Title = "Task Title",
            Description = "Description",
            Priority = API.Enums.Priority.Normal,
            Status = API.Enums.Status.NotStarted
        };

        mockTasks = new List<TaskItem>()
        {
            mockTask
        };

        mockTaskCreateDTO = new TaskCreateDTO(
            "DTO Title",
            "Description",
            API.Enums.Priority.High,
            API.Enums.Status.NotStarted
        );

        mockTaskUpdateDTO = new TaskUpdateDTO(
            "Updated Title",
            "Description",
            API.Enums.Priority.High,
            API.Enums.Status.NotStarted
        );
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WithTasks()
    {
        // Arrange
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(mockTasks);

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WithEmptyList_WhenNoTasksExist()
    {
        // Arrange
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TaskItem>());

        // Act
        var result = await controller.GetAll();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenTaskFound()
    {
        // Arrange
        var taskId = 1;
        mockRepo.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync(mockTask);

        // Act
        var result = await controller.GetById(taskId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenTaskNotFound()
    {
        // Arrange
        var taskId = 123;
        mockRepo.Setup(r => r.GetByIdAsync(taskId)).ReturnsAsync((TaskItem?) null);

        // Act
        var result = await controller.GetById(taskId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedAtAction_WhenValid()
    {
        // Arrange
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<TaskItem>())).ReturnsAsync(mockTask);

        // Act
        var result = await controller.Create(mockTaskCreateDTO);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenModelStateInvalid()
    {
        // Arrange
        controller.ModelState.AddModelError("Title", "Required");

        // Act
        var result = await controller.Create(mockTaskCreateDTO);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Update_ShouldReturnOk_WhenTaskFound()
    {
        // Arrange
        var taskId = 1;
        mockRepo.Setup(r => r.UpdateAsync(taskId, It.IsAny<TaskItem>())).ReturnsAsync(mockTask);

        // Act
        var result = await controller.Update(taskId, mockTaskUpdateDTO);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Update_ShouldReturnNotFound_WhenTaskNotFound()
    {
        // Arrange
        var taskId = 123;
        mockRepo.Setup(r => r.UpdateAsync(taskId, It.IsAny<TaskItem>())).ReturnsAsync((TaskItem?) null);

        // Act
        var result = await controller.Update(taskId, mockTaskUpdateDTO);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenTaskFound()
    {
        // Arrange
        var taskId = 1;
        mockRepo.Setup(r => r.DeleteAsync(taskId)).ReturnsAsync(true);

        // Act
        var result = await controller.Delete(taskId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ShouldReturnNotFound_WhenTaskNotFound()
    {
        // Arrange
        var taskId = 123;
        mockRepo.Setup(r => r.DeleteAsync(taskId)).ReturnsAsync(false);

        // Act
        var result = await controller.Delete(taskId);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
