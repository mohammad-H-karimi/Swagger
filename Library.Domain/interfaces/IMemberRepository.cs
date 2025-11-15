 using System.Collections.Generic;
 using System.Threading.Tasks;
 using Library.Domain.Entities;
 namespace Library.Domain.Interfaces
 {
 public interface IMemberRepository
 {
 Task<List<Member>> GetAllAsync();
 Task<Member> GetByIdAsync(int id);
 Task AddAsync(Member member);
 Task UpdateAsync(Member member);
 Task DeleteAsync(int id);
 }
 }