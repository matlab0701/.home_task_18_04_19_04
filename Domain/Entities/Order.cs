using Domain.Enums;

namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int CourierId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }


    public virtual  User User { get; set; }
    public virtual  Restaurant Restaurant { get; set; }
    public virtual  Courier Courier { get; set; }
    public virtual  List<OrderDetail> OrderDetails { get; set; } 
}
