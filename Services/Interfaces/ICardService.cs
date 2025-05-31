using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Services.Interfaces;

public interface ICardService
{
    Task<VirtualCard?> CreateVirtualCardAsync(CreateVirtualCardDto dto, int userId);
    Task<DebitCard?> CreateDebitCardAsync(CreateDebitCardDto dto, int userId);
    Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync();
    Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync(int userId);
    Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync();
    Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync(int userId);
    Task<bool> TransferFromAccountToVirtualCardAsync(VirtualCardTransferMoneyDto dto);
    Task<bool> TransferFromVirtualCardToAccountAsync(VirtualCardTransferMoneyDto dto);
}
