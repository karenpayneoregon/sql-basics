using DapperLibrary1.Models;

namespace DapperLibrary1.Interfaces;

public interface IBaseRepository
{
    List<Person> GetAll();
    Task<List<Person>> GetAllAsync();   
    Task<Person> Get(int id);
    Task<List<Person>> WhereIn(int[] ids);
    Task<(bool, Exception ex)> Update(Person person);
    Task Add(Person person);
    Task<(bool, Exception ex)> AddRange(List<Person> list);
    Task<bool> Remove(Person person);
}