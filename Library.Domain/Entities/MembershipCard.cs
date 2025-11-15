 using System;
 namespace Library.Domain.Entities
 {
 public class MembershipCard
 {
 public int MembershipCardId { get; set; }
 public string CardNumber { get; set; }
 public DateTime IssueDate { get; set; }
 public DateTime ExpiryDate { get; set; }
 public string Status { get; set; }
public int MemberId { get; set; }
 public Member Member { get; set; }
 }
 }