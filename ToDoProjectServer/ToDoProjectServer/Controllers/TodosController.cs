using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using ToDoProjectServer.Context;
using ToDoProjectServer.Model;

namespace ToDoProjectServer.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class TodosController : ControllerBase
{
    TodoAppContext context = new();

    [HttpPost]
    public IActionResult AddItem(Todo item)
    {
        context.Todos.Add(item);
        context.SaveChanges();

        var result = context.Todos.ToList();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult DeleteItem(Todo item)
    {
        var data = context.Todos.Find(item.Id);

        context.Todos.Remove(data);
        context.SaveChanges();

        var result = context.Todos.ToList();
        return Ok(result);
    }
    [HttpPost]
    public IActionResult EditItem(Todo item)
    {
        context.Todos.Update(item);
        context.SaveChanges();

        var result = context.Todos.ToList();
        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = context.Todos.ToList();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult ChangeCompleted(int id)
    {
        var Todos = context.Todos.ToList();

        var check = context.Todos.Find(id);
        if (check == null)
        {
            return NotFound();
        }
        Todos.Where(p => p.Id == id).FirstOrDefault().IsCompleted = !Todos.Where(p => p.Id == id).FirstOrDefault().IsCompleted;
        
        context.SaveChanges();
        return Ok(Todos);
    }
}
