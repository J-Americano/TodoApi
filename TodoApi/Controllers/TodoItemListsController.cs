using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemListsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemListsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItemLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemList>>> GetTodoItemLists()
        {
            return await _context.TodoItemLists.Include(t => t.TodoItems).ToListAsync();
        }

        // GET: api/TodoItemLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemList>> GetTodoItemList(long id)
        {
            var todoItemList = await _context.TodoItemLists.FindAsync(id);

            if (todoItemList == null)
            {
                return NotFound();
            }

            return todoItemList;
        }

        // PUT: api/TodoItemLists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItemList(long id, TodoItemList todoItemList)
        {
            if (id != todoItemList.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItemList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItemLists
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TodoItemList>> PostTodoItemList(TodoItemList todoItemList)
        {
            _context.TodoItemLists.Add(todoItemList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodoItemList", new { id = todoItemList.Id }, todoItemList);
        }

        // DELETE: api/TodoItemLists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItemList>> DeleteTodoItemList(long id)
        {
            var todoItemList = await _context.TodoItemLists.FindAsync(id);
            if (todoItemList == null)
            {
                return NotFound();
            }

            _context.TodoItemLists.Remove(todoItemList);
            await _context.SaveChangesAsync();

            return todoItemList;
        }

        private bool TodoItemListExists(long id)
        {
            return _context.TodoItemLists.Any(e => e.Id == id);
        }
    }
}
