﻿namespace PetShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int PetId { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
