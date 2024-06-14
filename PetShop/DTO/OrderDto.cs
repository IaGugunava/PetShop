namespace PetShop.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PetId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
