using DapperForAccessDatabase.Models;

namespace DapperForAccessDatabase.Interfaces;

public interface ICustomerRepository
{
    List<Customers> GetAll();
    void Add(List<Customers> customer);
}