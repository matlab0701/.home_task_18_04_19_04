using Domain.Enums;

namespace Domain.DTOs.Orders;

public class OrderStatusCountDto
{
        public OrderStatus OrderStatus { get; set; }
        public int Count { get; set; }

}
