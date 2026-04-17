using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Project2Exercise;
using ReactApp1.Server.Models.Project2Exercise.Dto;
using System.Net;

namespace ReactApp1.Server.Controllers.Project2Exercise
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _env;
        private readonly ApiResponse _response;
        public MenuItemController(ApplicationDbContext dbContext, IWebHostEnvironment env)
        {
            _dbContext = dbContext;
            _env = env;
            _response = new ApiResponse();
        }


        [HttpGet]
        public IActionResult GetMenuItems()
        {
           List<MenuItem> menuItems = _dbContext.MenuItems.ToList();
            List<OrderDetail> orderDetailsWithRatings=_dbContext.OrderDetails.Where(x=>x.Rating!=null).ToList();
            foreach (var menuItem in menuItems)
            {
                var ratings =orderDetailsWithRatings.Where(x=>x.MenuItemId==menuItem.Id).Select(x=>x.Rating.Value);
                double avgRating = ratings.Any() ? ratings.Average() : 0;
                menuItem.Rating = avgRating;
            }
            _response.Result = menuItems;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);

        }
        [HttpGet("{id:int}", Name = "GetMenuItem")]
        public IActionResult GetMenuItem(int id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            MenuItem? menuItem = _dbContext.MenuItems.FirstOrDefault(x => x.Id == id);
            List<OrderDetail> orderDetailsWithRatings = _dbContext.OrderDetails.Where(x => x.Rating != null &&x.MenuItemId==menuItem.Id).ToList();
            
                var ratings = orderDetailsWithRatings .Select(x => x.Rating.Value);
                double avgRating = ratings.Any() ? ratings.Average() : 0;
                menuItem.Rating = avgRating;
            
            _response.Result = menuItem;

            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);

        }



        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateMenuItem([FromForm] MenuItemCreateDTO menuItemCreateDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItemCreateDTO.File == null || menuItemCreateDTO.File.Length == 0)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.ErrorMessages = ["File is required"];
                        return BadRequest(_response);


                    }
                    var imagesPath = Path.Combine(_env.WebRootPath, "images");
                    if (!Directory.Exists(imagesPath))
                    {
                        Directory.CreateDirectory(imagesPath);
                    }
                    var filePath = Path.Combine(imagesPath, menuItemCreateDTO.File.FileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await menuItemCreateDTO.File.CopyToAsync(stream);
                    }
                    MenuItem menuItem = new MenuItem()
                    {
                        Name = menuItemCreateDTO.Name,
                        Description = menuItemCreateDTO.Description,
                        Price = menuItemCreateDTO.Price,
                        Category = menuItemCreateDTO.Category,
                        SpecialTag = menuItemCreateDTO.SpecialTag,
                        Image = "images/" + menuItemCreateDTO.File.FileName,
                    };
                    _dbContext.MenuItems.Add(menuItem);
                    _dbContext.SaveChanges();

                    _response.Result = menuItem;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetMenuItem", new { id = menuItem.Id }, _response);
                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = [ex.ToString()];
            }
            return BadRequest(_response);
        }
        [HttpPut]
        public async Task<ActionResult<ApiResponse>> UpdateMenuItem(int id, [FromForm] MenuItemUpdateDTO menuItemUpdateDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItemUpdateDTO == null || menuItemUpdateDTO.Id != id)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.ErrorMessages = ["File is required"];
                        return BadRequest(_response);


                    }

                    MenuItem? menuItemFromDb = await _dbContext.MenuItems.FirstOrDefaultAsync(x => x.Id == id);
                    if (menuItemFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(_response);
                    }
                    menuItemFromDb.Name = menuItemUpdateDTO.Name;
                    menuItemFromDb.Description = menuItemUpdateDTO.Description;
                    menuItemFromDb.Price = menuItemUpdateDTO.Price;
                    menuItemFromDb.Category = menuItemUpdateDTO.Category;
                    menuItemFromDb.SpecialTag = menuItemUpdateDTO.SpecialTag;

                    if (menuItemUpdateDTO.File != null && menuItemUpdateDTO.File.Length > 0)
                    {
                        var imagesPath = Path.Combine(_env.WebRootPath, "images");
                        if (!Directory.Exists(imagesPath))
                        {
                            Directory.CreateDirectory(imagesPath);
                        }
                        var filePath = Path.Combine(imagesPath, menuItemUpdateDTO.File.FileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        var filePath_oldFile = Path.Combine(_env.WebRootPath, menuItemFromDb.Image);
                        if (System.IO.File.Exists(filePath_oldFile))
                        {
                            System.IO.File.Delete(filePath_oldFile);
                        }
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await menuItemUpdateDTO.File.CopyToAsync(stream);
                        }
                        menuItemFromDb.Image = "images/" + menuItemUpdateDTO.File.FileName;
                    }







                    _dbContext.MenuItems.Update(menuItemFromDb);
                    _dbContext.SaveChanges();

                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = [ex.ToString()];
            }
            return BadRequest(_response);
        }
        [HttpDelete ]
        public async Task<ActionResult<ApiResponse>> DeleteMenuItem(int id )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if( id==0)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode=HttpStatusCode.BadRequest;
                        _response.ErrorMessages = ["File is required"];
                        return BadRequest(_response);   


                    }

                    MenuItem? menuItemFromDb =await _dbContext.MenuItems.FirstOrDefaultAsync(x => x.Id == id);
                    if(menuItemFromDb == null)
                    {
                        _response.IsSuccess=false;
                        _response.StatusCode=HttpStatusCode.NotFound;
                        return NotFound(_response);
                    }
                     
                        var filePath_oldFile = Path.Combine(_env.WebRootPath, menuItemFromDb.Image);
                        if (System.IO.File.Exists(filePath_oldFile))
                        {
                            System.IO.File.Delete(filePath_oldFile);
                        }
                    



                   
                    
                    
                    _dbContext.MenuItems.Remove(menuItemFromDb);
                    _dbContext.SaveChanges();
                     
                    _response.StatusCode =HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess=false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess=false;
                _response.ErrorMessages = [ex.ToString()];
            }
            return BadRequest (_response);  
        }



    }
}
