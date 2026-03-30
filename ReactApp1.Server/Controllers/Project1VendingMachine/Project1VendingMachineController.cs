using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Data;

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
        public IActionResult GetAll()
        {
            var categories=dbContext.Categories.ToList();
            return Ok(categories);
        }
    }
}
