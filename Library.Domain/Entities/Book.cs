 using System.Collections.Generic;
 namespace Library.Domain.Entities
 {
 public class Book
 {
 public int BookId { get; set; }
 public string Title { get; set; }
 public string ISBN { get; set; }
public int PublishYear { get; set; }
 public decimal Price { get; set; }
 public ICollection<Author> Authors { get; set; } = new List<Author>();
 }
 }
 src/Library.Domain/Entities/Member.cs
 using System;
 using System.Collections.Gen