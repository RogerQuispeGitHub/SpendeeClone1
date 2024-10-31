using SQLite;

namespace SpendeeClone.Models
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
    }

    public enum TransactionType
    {
        Income,
        Expense
    }
}