using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Data;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;

namespace Simple_Bank_Application.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;

    public CardRepository(AppDbContext context) => _context = context;
    public async Task<DebitCard> CreateDebitCardAsync(CreateDebitCardDto dto, int userId)
    {
        var user = await _context.Users.FindAsync(userId);

        var debitCard = new DebitCard
        {
            UserId = user.Id,
            Type = dto.Type,
            CardNumber = "0000 0000 0000 0000",
            ExpirationDate = DateTime.Now.AddYears(10),
            CVV = "000",
            CardholderNameAndSurname = user.Name + " " + user.Surname,
            OnlineShopping = dto.OnlineShopping,
            Password = dto.Password,
            LinkedAccountId = dto.LinkedAccountId,

        };

        _context.DebitCards.Add(debitCard);
        await _context.SaveChangesAsync();
        return debitCard;
    }

    public async Task<VirtualCard> CreateVirtualCardAsync(CreateVirtualCardDto dto, int userId)
    {
        var user = await _context.Users.FindAsync(userId);

        var virtualCard = new VirtualCard
        {
            UserId = user.Id,
            Type = dto.Type,
            CardNumber = "0000 0000 0000 0000",
            ExpirationDate = DateTime.Now.AddYears(10),
            CVV = "000",
            CardholderNameAndSurname = user.Name + " " + user.Surname,
            OnlineShopping = dto.OnlineShopping,
            AvailableLimit = 0

        };

        _context.VirtualCards.Add(virtualCard);
        await _context.SaveChangesAsync();
        return virtualCard;
    }


    public async Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync()
    {
        var cards = await _context.DebitCards.ToListAsync();
        return cards;
    }

    public async Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync(int userId)
    {
        var cards = await _context.DebitCards.Where(card => card.UserId == userId).ToListAsync();
        return cards;
    }

    public async Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync()
    {
        var cards = await _context.VirtualCards.ToListAsync();
        return cards;
    }

    public async Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync(int userId)
    {
        var cards = await _context.VirtualCards.Where(card => card.UserId == userId).ToListAsync();
        return cards;
    }

    public async Task<bool> TransferFromAccountToVirtualCardAsync(VirtualCardTransferMoneyDto dto)
    {
        var account = await _context.BankAccounts.FindAsync(dto.FromAccountOrCardId);
        var card = await _context.VirtualCards.FindAsync(dto.TargetAccountOrCardId);

        if (account == null || card == null)
            return false;

        account.Balance -= dto.Amount;
        card.AvailableLimit += dto.Amount;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> TransferFromVirtualCardToAccountAsync(VirtualCardTransferMoneyDto dto)
    {
        var account = await _context.BankAccounts.FindAsync(dto.FromAccountOrCardId);
        var card = await _context.VirtualCards.FindAsync(dto.TargetAccountOrCardId);

        if (account == null || card == null)
            return false;

        account.Balance += dto.Amount;
        card.AvailableLimit -= dto.Amount;
        await _context.SaveChangesAsync();

        return true;
    }
}
