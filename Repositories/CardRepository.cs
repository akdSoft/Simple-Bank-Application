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
    public async Task<DebitCard> CreateDebitCardAsync(DebitCard card)
    {
        _context.DebitCards.Add(card);
        await _context.SaveChangesAsync();
        return card;
    }

    public async Task<VirtualCard> CreateVirtualCardAsync(VirtualCard card)
    {
        _context.VirtualCards.Add(card);
        await _context.SaveChangesAsync();
        return card;
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

    public async Task<VirtualCard> GetVirtualCardByIdAsync(int virtualCardId) =>
        await _context.VirtualCards.FindAsync(virtualCardId);

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

        var account = await _context.BankAccounts.FindAsync(dto.TargetAccountOrCardId);
        var card = await _context.VirtualCards.FindAsync(dto.FromAccountOrCardId);

        if (account == null || card == null)
            return false;

        account.Balance += dto.Amount;
        card.AvailableLimit -= dto.Amount;
        await _context.SaveChangesAsync();

        return true;
    }
}