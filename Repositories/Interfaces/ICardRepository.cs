using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Data;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Repositories.Interfaces;

public interface ICardRepository
{
    Task<DebitCard> CreateDebitCardAsync(DebitCard card);
    Task<VirtualCard> CreateVirtualCardAsync(VirtualCard card);
    Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync();
    Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync(int userId);
    Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync();
    Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync(int userId);
    Task<VirtualCard?> GetVirtualCardByIdAsync(int virtualCardId);
    Task UpdateVirtualCardAsync(VirtualCard virtualCard);
}
