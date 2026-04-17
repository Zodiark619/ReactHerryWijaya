using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp1.Server.Data;
using ReactApp1.Server.Models.Project2Exercise;
using ReactApp1.Server.Models.Project2Exercise.Dto;
using ReactApp1.Server.Utility.Project2Exercise;
using System.Net;

namespace ReactApp1.Server.Controllers.Project2Exercise
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderHeaderController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ApiResponse _response;
        public OrderHeaderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _response = new ApiResponse();
        }
        [HttpGet]
        public ActionResult<ApiResponse> GetOrders(string userId="")
        {
        IEnumerable<OrderHeader>  orderHeaderList=_dbContext.OrderHeaders.Include(x=>x.OrderDetails).ThenInclude(x=>x.MenuItem)
                .OrderByDescending(x=>x.OrderHeaderId)  ;
            if (!string.IsNullOrEmpty(userId))
            {
                orderHeaderList=orderHeaderList.Where(x=>x.ApplicationUserId == userId);
            }
        _response.Result = orderHeaderList ; 
            _response.StatusCode=HttpStatusCode.OK;
            return Ok(_response);
        }
        [HttpGet("{orderId:int}")]
        public ActionResult<ApiResponse> GetOrder(int orderId)
        {
            if (orderId == 0)
            {
                _response.IsSuccess = false;
                _response.StatusCode =HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Invaid order id");
                return BadRequest( _response);
            }
         OrderHeader?  orderHeader=_dbContext.OrderHeaders.Include(x=>x.OrderDetails).ThenInclude(x=>x.MenuItem)
                .FirstOrDefault(x=>x.OrderHeaderId==orderId)  ;

            if(orderHeader == null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages.Add("notfound order id");
                return NotFound(_response);

            }


              _response.Result = orderHeader  ; 
            _response.StatusCode=HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public ActionResult<ApiResponse> CreateOrder([FromBody] OrderHeaderCreateDTO orderHeaderDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    OrderHeader orderHeader = new()
                    {
                        PickUpName = orderHeaderDTO.PickUpName,
                        PickUpPhoneNumber = orderHeaderDTO.PickUpPhoneNumber,
                        PickUpEmail = orderHeaderDTO.PickUpEmail,
                        OrderDate = DateTime.Now,
                        OrderTotal = orderHeaderDTO.OrderTotal,
                        Status = SD.status_confirmed,
                        TotalItem = orderHeaderDTO.TotalItem,
                        ApplicationUserId = orderHeaderDTO.ApplicationUserId,
                    };
                    _dbContext.OrderHeaders.Add(orderHeader);
                    _dbContext.SaveChanges();
                    foreach(var orderDetailDTO in orderHeaderDTO.OrderDetailsDTO)
                    {
                        OrderDetail orderDetail = new()
                        {
                            OrderHeaderId=orderHeader.OrderHeaderId,
                            MenuItemId=orderDetailDTO.MenuItemId,
                            Quantity= orderDetailDTO.Quantity,
                            ItemName=orderDetailDTO.ItemName,
                            Price=orderDetailDTO.Price,

                        };
                        _dbContext.OrderDetails.Add(orderDetail);


                    }
                        _dbContext.SaveChanges();
                    _response.Result = orderHeader;
                    orderHeader.OrderDetails = [];
                    _response.StatusCode=HttpStatusCode.Created;
                    return CreatedAtAction(nameof(GetOrder), new { orderId=orderHeader.OrderHeaderId},_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages=ModelState.Values.SelectMany(x=>x.Errors).Select(x=>x.ErrorMessage).ToList();
                    return BadRequest( _response);

                }

            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError,_response);
            }


        }
        [HttpPut("{orderId:int}")]
        public ActionResult<ApiResponse> UpdateOrder(int orderId,[FromBody] OrderHeaderUpdateDTO orderHeaderDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (orderId != orderHeaderDTO.OrderHeaderId)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.ErrorMessages.Add("invalid id");
                        return BadRequest(_response);
                    }
                    OrderHeader? orderHeaderFromDb =  _dbContext.OrderHeaders.FirstOrDefault(x=>x.OrderHeaderId==orderId);

                    if(orderHeaderFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.ErrorMessages.Add("order not found");
                        return NotFound(_response);
                    }
                    if (!string.IsNullOrEmpty(orderHeaderDTO.PickUpName))
                    {
                        orderHeaderFromDb.PickUpName = orderHeaderDTO.PickUpName;
                    }
                    if (!string.IsNullOrEmpty(orderHeaderDTO.PickUpPhoneNumber))
                    {
                        orderHeaderFromDb.PickUpPhoneNumber = orderHeaderDTO.PickUpPhoneNumber;
                    }
                    if (!string.IsNullOrEmpty(orderHeaderDTO.PickUpEmail))
                    {
                        orderHeaderFromDb.PickUpEmail = orderHeaderDTO.PickUpEmail;
                    }
                    if (!string.IsNullOrEmpty(orderHeaderDTO.Status))
                    {
                        if (orderHeaderFromDb.Status.Equals(SD.status_confirmed, StringComparison.InvariantCultureIgnoreCase)
                            && orderHeaderDTO.Status.Equals(SD.status_readyForPickUp, StringComparison.InvariantCultureIgnoreCase))
                        {
                                orderHeaderFromDb.Status = SD.status_readyForPickUp;
                        }
                        if (orderHeaderFromDb.Status.Equals(SD.status_readyForPickUp, StringComparison.InvariantCultureIgnoreCase)
                            && orderHeaderDTO.Status.Equals(SD.status_Completed, StringComparison.InvariantCultureIgnoreCase))
                        {
                                orderHeaderFromDb.Status = SD.status_Completed;
                        }
                        if (orderHeaderDTO.Status.Equals(SD.status_Cancelled, StringComparison.InvariantCultureIgnoreCase)
                             )
                        {
                                orderHeaderFromDb.Status = SD.status_Cancelled;
                        }
                    }
                      _dbContext.SaveChanges();
                       _response.StatusCode=HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages=ModelState.Values.SelectMany(x=>x.Errors).Select(x=>x.ErrorMessage).ToList();
                    return BadRequest( _response);

                }

            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError,_response);
            }


        }
    }
}
