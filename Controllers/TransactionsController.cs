using Microsoft.AspNetCore.Mvc;
using BankAPIproject.Data;
using BankAPIproject.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAPIproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferMoney([FromBody] TransferRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.FromAccountId) || string.IsNullOrEmpty(request.ToAccountId) || request.Amount <= 0)
            {
                return BadRequest("Geçersiz transfer isteği.");
            }

            var sender = await _context.Users.FirstOrDefaultAsync(u => u.Name == request.FromAccountId);
            var receiver = await _context.Users.FirstOrDefaultAsync(u => u.Name == request.ToAccountId);

            if (sender == null || receiver == null)
            {
                return NotFound("Gönderen veya alıcı kullanıcı bulunamadı.");
            }

            if (sender.Balance < request.Amount)
            {
                return BadRequest("Yetersiz bakiye.");
            }

            // Bakiye güncellemesi
            sender.Balance -= request.Amount;
            receiver.Balance += request.Amount;

            // İşlemi kaydet
            var transaction = new Transaction
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Amount = request.Amount,
                Type = "Transfer",
                CreatedAt = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Para transferi başarıyla gerçekleşti." });
        }

        [HttpPost("pay_debt")]
        public async Task<IActionResult> PayDebt([FromBody] DebtPaymentRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.AccountId) || request.Amount <= 0)
            {
                return BadRequest("Geçersiz borç ödeme isteği.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == request.AccountId);
            var bankUser = await _context.Users.FirstOrDefaultAsync(u => u.Name == "Bank"); // Bankanın ismi 'Bank' olsun

            if (user == null || bankUser == null)
            {
                return NotFound("Kullanıcı veya banka hesabı bulunamadı.");
            }

            if (user.Balance < request.Amount)
            {
                return BadRequest("Yetersiz bakiye.");
            }

            // Bakiye güncellemesi
            user.Balance -= request.Amount;
            bankUser.Balance += request.Amount;

            // İşlemi kaydet
            var transaction = new Transaction
            {
                SenderId = user.Id,
                ReceiverId = bankUser.Id,
                Amount = request.Amount,
                Type = "DebtPayment",
                CreatedAt = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Borç ödeme başarıyla gerçekleşti." });
        }
    }

    
    public class TransferRequest
    {
        public string? FromAccountId { get; set; }
        public string? ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }

    public class DebtPaymentRequest
    {
        public  string? AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
