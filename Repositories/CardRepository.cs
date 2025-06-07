using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Data;
using Simple_Bank_Application.Models;
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

    public async Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync() =>
        await _context.DebitCards.ToListAsync();

    public async Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync(int userId) =>
        await _context.DebitCards.Where(card => card.UserId == userId).ToListAsync();

    public async Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync() =>
        await _context.VirtualCards.ToListAsync();


    public async Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync(int userId) =>
        await _context.VirtualCards.Where(card => card.UserId == userId).ToListAsync();

    public async Task<VirtualCard?> GetVirtualCardByIdAsync(int virtualCardId) =>
        await _context.VirtualCards.FindAsync(virtualCardId);

    public async Task UpdateVirtualCardAsync(VirtualCard virtualCard)
    {
        _context.VirtualCards.Update(virtualCard);
        await _context.SaveChangesAsync();
    }
}