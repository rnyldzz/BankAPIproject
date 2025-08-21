using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPIproject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Balance { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

    }
}
