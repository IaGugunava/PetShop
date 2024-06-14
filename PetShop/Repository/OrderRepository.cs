using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;

namespace PetShop.Repository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly IConfiguration _configuration;
        private readonly PetShopContext _context;
        public OrderRepository(IConfiguration configuration, PetShopContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(x => x.OrderId == id);
        }

        public void CreateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            _context.Orders.Add(order);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void DeleteOredrById(int id)
        {
            Order order = _context.Orders.SingleOrDefault(p => p.OrderId == id);
            _context.Remove(order);
        }
    }
}
