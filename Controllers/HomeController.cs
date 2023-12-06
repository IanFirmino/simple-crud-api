using Data.Todo;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;

namespace Todo.Controllers {

    [ApiController]
    public class HomeController : ControllerBase{
        
        [HttpGet("/{id:int}")]
        public IActionResult GetById([FromServices] AppDbContext context, [FromRoute] int id){
            var todos = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todos == null)
                return NotFound();

            return Ok(todos);
        }


        [HttpGet][Route("/")]
        public IActionResult GetAll([FromServices] AppDbContext context) => Ok(context.Todos.ToList());
        
        [HttpPost("/")]
        public IActionResult Create([FromServices] AppDbContext context, [FromBody] TodoModel todo){
            context.Todos.Add(todo);
            context.SaveChanges();
            return Created($"/{todo.Id}", todo);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Update([FromServices] AppDbContext context, [FromBody] TodoModel todo, [FromRoute] int id){
            var todoModel = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todoModel == null)
                return NotFound();

            todoModel.Title = todo.Title;
            todoModel.Done = todo.Done;
            context.Todos.Update(todoModel);
            context.SaveChanges();
            return Ok(todoModel);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete([FromServices] AppDbContext context, [FromRoute] int id){
            var todoModel = context.Todos.FirstOrDefault(x => x.Id == id);
            if (todoModel == null)
                NotFound();

            context.Todos.Remove(todoModel);
            context.SaveChanges();
            return Ok();
        }
           
    }


}