 using System.Collections.Generic;
 using System.Threading.Tasks;
 using Library.Domain.Entities;
 namespace Library.Domain.Interfaces
 {
 public interface IAuthorRepository
 {
 Task<List<Author>> GetAllAsync();
 Task<Author> GetByIdAsync(int id);
 Task AddAsync(Author author);
 Task UpdateAsync(Author author);
 Task DeleteAsync(int id);
 }
 }