using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPIproject.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Column("sender_id")]
        public int SenderId { get; set; }

        [Column("receiver_id")]
        public int ReceiverId { get; set; }

        public decimal Amount { get; set; }
        public string? Type { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
