using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Project1VendingMachine;
using ReactApp1.Server.Models.Project1VendingMachine.Stripe;
using Stripe;
using Stripe.Checkout;


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












        #region stripe
        [HttpPost("payment/create-session")]
        public IActionResult CreateSession([FromBody] CheckoutRequest request)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                Quantity = 1,
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    UnitAmount = 1000, // $10.00
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Test Item"
                    }
                }
            }
        },
                SuccessUrl = "http://localhost:3000/success",
                CancelUrl = "http://localhost:3000/cancel"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Ok(new { sessionId = session.Id });
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                "your_webhook_secret"
            );

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Session;

                // ✅ This is where payment is CONFIRMED
                // Save order, update DB, etc.
            }

            return Ok();
        }

        #endregion

        #region category
        [HttpGet("Category/GetAll")]
        public async Task<IActionResult> CategoryGetAll()
        {
            var categories=await dbContext.Categories.ToListAsync();
            return Ok(categories);
        }
        [HttpGet("Category/Get/{id}")]
        public async Task<IActionResult> CategoryGet(int id)
        {
            var item = await dbContext.Categories
                 
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null) return NotFound();

            return Ok(item);
        }
        [HttpPost("Category/Create")]
        public async Task<IActionResult> CategoryCreate([FromBody]Category category)
        {
            if (category == null)
                return BadRequest();
            await dbContext.Categories.AddAsync(category);
             await dbContext.SaveChangesAsync();
        //    return Ok(category);
            return CreatedAtAction(nameof(CategoryGet), new { id = category.Id }, category);
        }
        [HttpPut("Category/Edit/{id}")]
        public async Task<IActionResult> CategoryEdit(int id, UpdateCategoryDto dto)
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
        public async Task<IActionResult> CategoryDelete(int id )
        {
             var category = await dbContext.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

         dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();

            return Ok(category);
        }
        #endregion
        #region item
        [HttpGet("Item/GetAll")]
        public async Task<IActionResult> ItemGetAll()
        {
            var items = await dbContext.Items.Include(x=>x.Category)
                .Select(x=>new GetItemDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    CategoryName    =x.Category.Name
                })
                .ToListAsync();

            return Ok(items);
        }
        [HttpGet("Item/Get/{id}")]
        public async Task<IActionResult> ItemGet(int id)
        {
            var item = await dbContext.Items
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null) return NotFound();

            return Ok(item);
        }
        [HttpPost("Item/Create")]
        public async Task<IActionResult> ItemCreate([FromBody] CreateItemDto itemDto)
        {
            if (itemDto == null)
                return BadRequest();
            var item = new Item
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
                CategoryId = itemDto.CategoryId
            };
            await dbContext.Items.AddAsync(item);
            await dbContext.SaveChangesAsync();
            //    return Ok(category);
            return CreatedAtAction(nameof(ItemGet), new { id = item.Id }, item);
        }
        [HttpPut("Item/Edit/{id}")]
        public async Task<IActionResult> ItemEdit(int id, CreateItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)
                
                )
                return BadRequest("Name is required");
            if (dto.Price <= 0)
                return BadRequest("Price must be greater than 0");

            if (dto.CategoryId <= 0)
                return BadRequest("Category is required");
            var item = await dbContext.Items.FindAsync(id);

            if (item == null)
                return NotFound();

            item.Name = dto.Name;
            item.Price = dto.Price;
            item.CategoryId = dto.CategoryId;
            await dbContext.SaveChangesAsync();

            return Ok(item);
        }
        [HttpDelete("Item/Delete/{id}")]
        public async Task<IActionResult> ItemDelete(int id)
        {
            var item = await dbContext.Items.FindAsync(id);

            if (item == null)
                return NotFound();

            dbContext.Items.Remove(item);
            await dbContext.SaveChangesAsync();

            return Ok(item);
        }
        #endregion
        ////////////////////////////////////

    }
}
