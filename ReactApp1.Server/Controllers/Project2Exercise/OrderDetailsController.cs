using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Project2Exercise;
using ReactApp1.Server.Models.Project2Exercise.Dto;
using ReactApp1.Server.Utility.Project2Exercise;
using System.Net;

namespace ReactApp1.Server.Controllers.Project2Exercise
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ApiResponse _response;
        public OrderDetailsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new ApiResponse();
        }
        [HttpPut("{orderDetailsId:int}")]
        public ActionResult<ApiResponse> UpdateOrderDetails(int orderDetailsId, [FromBody] OrderDetailsUpdateDTO orderDetailsDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (orderDetailsId != orderDetailsDTO.OrderDetailId)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.ErrorMessages.Add("invalid id");
                        return BadRequest(_response);
                    }
                    OrderDetail? orderDetailsFromDb = _dbContext.OrderDetails.FirstOrDefault(x => x.OrderDetailId == orderDetailsId);

                    if (orderDetailsFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.ErrorMessages.Add("order detila not found");
                        return NotFound(_response);
                    }
                  

                    orderDetailsFromDb.Rating = orderDetailsDTO.Rating;


                    _dbContext.SaveChanges();
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                    return BadRequest(_response);

                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, _response);
            }


        }
    }
}
