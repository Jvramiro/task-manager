using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTO;
using TaskManager.API.Models;
using TaskManager.API.Repositories;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/task")]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository taskRepository;
    public TaskController(ITaskRepository taskRepository)
    {
        this.taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await taskRepository.GetAllAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await taskRepository.GetByIdAsync(id);

        if (task == null){
            return NotFound("Task not Found");
        }

        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskCreateDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid Parameters");
        }

        var task = new TaskItem()
        {
            Title = model.Title,
            Description = model.Description,
            Priority = model.Priority,
            Status = model.Status
        };

        await taskRepository.CreateAsync(task);

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskUpdateDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid Parameters");
        }

        var task = new TaskItem()
        {
            Title = model.Title,
            Description = model.Description,
            Priority = model.Priority,
            Status = model.Status
        };

        var updatedTask = await taskRepository.UpdateAsync(id, task);

        if (updatedTask == null){
            return NotFound("Task not Found");
        }

        return Ok(updatedTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await taskRepository.DeleteAsync(id);

        if (!deleted){
            return NotFound("Task not Found");
        }

        return NoContent();
    }
}
