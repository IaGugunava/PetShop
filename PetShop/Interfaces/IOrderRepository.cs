using PetShop.Models;

namespace PetShop.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrderById(int id);
        IEnumerable<Order> GetAllOrders();
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOredrById(int id);

    }
}
