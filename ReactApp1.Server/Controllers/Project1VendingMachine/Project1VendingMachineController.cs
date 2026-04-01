using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Project1VendingMachine;

namespace ReactApp1.Server.Controllers.Project1VendingMachine
{
    [Route("api/[controller]")]
    [ApiController]
    public class Project1VendingMachineController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public Project1VendingMachineController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet("Category/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var categories=await dbContext.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpPost("Category/Create")]
        public async Task<IActionResult> Create([FromBody]Category category)
        {
            if (category == null)
                return BadRequest();
            await dbContext.Categories.AddAsync(category);
             await dbContext.SaveChangesAsync();
        //    return Ok(category);
            return CreatedAtAction(nameof(Create), new { id = category.Id }, category);
        }
        [HttpPut("Category/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, UpdateCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Name is required");
            var category = await dbContext.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

            category.Name = dto.Name;

            await dbContext.SaveChangesAsync();

            return Ok(category);
        }
        [HttpDelete("Category/Delete/{id}")]
        public async Task<IActionResult> Delete(int id )
        {
             var category = await dbContext.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

         dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();

            return Ok(category);
        }
    }
}
