using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CmdApi.Models;

namespace CmdApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController: ControllerBase
    {
        private readonly  CommandContext _context;

        public CommandsController(CommandContext context) => _context = context;

        // GEt :  Api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands() 
        {
             return _context.CommandItems;
        }

        // GEt :  Api/commands/n
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItem(int id) 
        {
             var commandItem = _context.CommandItems.Find(id);
             if(commandItem == null) 
                return NotFound();
             return commandItem;
        }

         //POST :  Api/commands
        [HttpPost]
        public async Task<ActionResult<Command>> Put([FromBody]Command command)
        {
            _context.CommandItems.Add(command);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCommandItem", new Command{Id= = command.Id}, command);
            return CreatedAtAction(nameof(GetCommandItem), new { id = command.Id }, command);
        }

        //put :  Api/commands/n
        [HttpPut("{id}")]
        public async Task<ActionResult<Command>> PutCommandItem(int id, [FromBody]Command command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            _context.Entry(command).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }
            return NoContent();
        }
         
        [HttpDelete("{id}")]
        public async Task<ActionResult<Command>> DeleteCommandItem(int id)
        {
           var commandItem = await  _context.CommandItems.FindAsync(id);
             if(commandItem == null) 
                return NotFound();
            _context.CommandItems.Remove(commandItem);
             await _context.SaveChangesAsync();
             return  commandItem;
        }
    }
}