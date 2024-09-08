using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entity;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly FreshKit _dbContext;
        public OrderController(FreshKit freshKit)
        {
            _dbContext = freshKit;
        }
        [HttpGet("ConfirmOrder")]
        public IActionResult ConfirmOrder([FromBody] List<Orderlits> orders)
        {
            OrderUserInfo userInfo = new OrderUserInfo();
            userInfo.UserID = orders.FirstOrDefault().UserID;
            _dbContext.OrderUserInfo.Add(userInfo);
            bool isSave = _dbContext.SaveChanges() > 0;
            if (isSave)
            {
                orders.ForEach(o => o.OrderUserInfo = userInfo.OrderUserInfoID);
                _dbContext.Orderlits.AddRange(orders);
                bool Result = _dbContext.SaveChanges() > 0;
                return Ok(Result);
            }
            return BadRequest();
        }

        [HttpPost("AddItemInCart")]
        public IActionResult AddItemInCart([FromBody] Cart cart)
        {
            _dbContext.Carts.Add(cart);
            bool Result = _dbContext.SaveChanges() > 0;

            return Ok(Result);
        }
        [HttpGet]
        public IActionResult ViewCart(int UserID)
        {
            List<CartInformationShow> cartInformationShows = new List<CartInformationShow>();
            List<Cart> cartlist = _dbContext.Carts.Where(x => x.UserID.Equals(UserID) && x.DateTime.Date == DateTime.UtcNow.Date).ToList();
            if (cartlist.Count() > 0)
            {
                foreach (Cart cart in cartlist)
                {
                    CartInformationShow informationShow = new CartInformationShow();
                    informationShow.UserID = cart.UserID;
                    informationShow.DateTime = cart.DateTime;
                    informationShow.CartID = cart.CartId;
                    var shirst = _dbContext.Shakes.Find(cart.ShirstId);
                    if (shirst != null)
                    {
                        informationShow.ShirtName = shirst.Name;
                        informationShow.ShirtID = shirst.ShirstId;
                        informationShow.Price = shirst.Price;
                        informationShow.Quantity = cart.orderQuantity;
                        informationShow.Color = shirst.Color;
                        informationShow.Size = shirst.Size;

                    }
                    cartInformationShows.Add(informationShow);


                }
            }
            return Ok(cartInformationShows);
        }
        [HttpGet]
        public IActionResult RemoveItemFromCart(int CartID)
        {
            var data = _dbContext.Carts.Find(CartID);
            if (data != null)
            {
                _dbContext.Carts.Remove(data);
                bool Result = _dbContext.SaveChanges() > 0;
                return Ok(Result);
            }
            return NotFound();
        }

        public IActionResult ConfirmOrder(int UserOrderInfoID)
        {
            var orderlist = _dbContext.Orderlits.Where(x => x.OrderUserInfo == UserOrderInfoID).ToList();
            if (orderlist != null)
            {
                orderlist.ForEach(x => x.OrderStatus = "CONFIRM");
                _dbContext.Orderlits.UpdateRange(orderlist);
                bool Result = _dbContext.SaveChanges() > 0;
                return Ok(Result);
            }
            return BadRequest();
        }
    }
}
