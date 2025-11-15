 using System.Collections.Generic;
 using System.Threading.Tasks;
 using Library.Domain.Entities;
 namespace Library.Domain.Interfaces
 {
 public interface IBookRepository
 {
 Task<List<Book>> GetAllAsync();
 Task<Book> GetByIdAsync(int id);
 Task AddAsync(Book book);
 Task UpdateAsync(Book book);
 Task DeleteAsync(int id);
 }
 }