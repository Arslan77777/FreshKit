using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Entity
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserRole { get; set; } = "USER";// Second admin
    }
    public class Shirsts
    {
        [Key]
        public int ShirstId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string AvailableQuantity { get; set; }
        public string Size { get; set; }
    }
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int ShirstId { get; set; }
        public int UserID { get; set; }
        public int orderQuantity { get; set; }
        public DateTime DateTime { get; set; }=DateTime.UtcNow;
    }
    public class Orderlits
    {
        [Key]
        public int OrderlitsID { get; set; }
        public int ShirstId { get; set; }
        public int UserID { get; set; }
        public int orderQuantity { get; set; }
        public int OrderUserInfo { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        public string OrderStatus { get; set; } = "PENDING";
    }
    public class OrderUserInfo
    {
        [Key]
        public int OrderUserInfoID { get; set; }
        public int UserID { get; set; }

    }
    public class CartInformationShow
    {
        public int CartID { get; set; }
        public int ShirtID { get; set; }
        public int UserID { get; set; }
        public DateTime DateTime { get; set; }
        public string ShirtName { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
    }
}
