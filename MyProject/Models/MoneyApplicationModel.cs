using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [MaxLength(100)]
        [Required]
        public string CategoryName { get; set; }
        public Boolean IsActive { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Category()
        {
            Transactions = new List<Transaction>();
        }
    }

    public class PaymentMode
    {
        [Key]
        public int PaymentModeId { get; set; }
        [MaxLength(100)]
        [Required]
        public string PaymentsMode { get; set; }
        public Boolean IsActive { get; set; }
        public List<Transaction> Transactions { get; set; }
        public PaymentMode()
        {
            Transactions = new List<Transaction>();
        }

    }

    public class Transaction
    {
        public int TransactionId { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public PaymentMode Payment { get; set; }
        [Required]
        public DateTime TxnDate { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        public int Amount { get; set; }
        [MaxLength(50)]
        [Required]
        public string TransactionType { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public Boolean IsActive { get; set; }
    }

    public class TransactionInputDTO
    {
        public string TransactionType { get; set; }
        public DateTime TxnDate { get; set; }
        public  string Category { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string PaymentMode { get; set; }
        public Boolean IsActive { get; set; }
    }
     
    public class TableData
    {
        public string Category { get; set; }
        public DateTime TDate { get; set; }
        public string PMode { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string TransactionType { get; set; }
        public int TransactionId { get; set; }
    }
    public class SearchDTO
    {
        public string Title { get; set; }
        public string Value { get; set; }
    }

    public class HeaderDTO
    {
        public int TotalIncome { get; set; }
        public int TotalExpense { get; set; }
        public int TotalTxn { get; set; }

    }
    public class CatList
    {
        public List<string> CategoryList { get; set; }
    }
    public class CatSearchDTO
    {
        public string Title { get; set; }
    }
}
