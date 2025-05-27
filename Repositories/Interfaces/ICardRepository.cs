using Simple_Bank_Application.Data;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Repositories.Interfaces;

public interface ICardRepository
{
    Task<DebitCard> CreateDebitCardAsync(CreateDebitCardDto dto, int userId);
    Task<VirtualCard> CreateVirtualCardAsync(CreateVirtualCardDto dto, int userId);
    Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync();
    Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync(int userId);
    Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync();
    Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync(int userId);
    Task<bool> TransferFromVirtualCardToAccountAsync(VirtualCardTransferMoneyDto dto);
    Task<bool> TransferFromAccountToVirtualCardAsync(VirtualCardTransferMoneyDto dto);
}
