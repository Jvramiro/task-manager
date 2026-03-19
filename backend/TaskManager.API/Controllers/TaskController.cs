using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.DTO;
using TaskManager.API.Models;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/task")]
public class TaskController : ControllerBase
{
    private readonly AppDbContext dbContext;
    public TaskController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await dbContext.Tasks.ToListAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await dbContext.Tasks.FindAsync(id);

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

        await dbContext.Tasks.AddAsync(task);
        await dbContext.SaveChangesAsync();

        return Created($"Task {task.Id} created sucessfully", task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskUpdateDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid Parameters");
        }

        var task = await dbContext.Tasks.FindAsync(id);

        if (task == null){
            return NotFound("Task not Found");
        }

        task.Title = model.Title;
        task.Description = model.Description;
        task.Priority = model.Priority;
        task.Status = model.Status;

        dbContext.Update(task);
        await dbContext.SaveChangesAsync();
        return Ok($"Task {task.Id} updated sucessfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await dbContext.Tasks.FindAsync(id);

        if (task == null){
            return NotFound("Task not Found");
        }

        dbContext.Tasks.Remove(task);
        await dbContext.SaveChangesAsync();
        return Ok($"Task {id} deleted sucessfully");
    }

}