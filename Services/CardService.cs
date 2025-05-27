using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _repo;

    public CardService(ICardRepository repo) => _repo = repo;
    public async Task<DebitCard> CreateDebitCardAsync(CreateDebitCardDto dto, int userId) =>
        await _repo.CreateDebitCardAsync(dto, userId);

    public async Task<VirtualCard> CreateVirtualCardAsync(CreateVirtualCardDto dto, int userId) =>
        await _repo.CreateVirtualCardAsync(dto, userId);

    public async Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync() =>
        await _repo.GetAllDebitCardsAsync();

    public async Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync(int userId) =>
        await _repo.GetAllDebitCardsAsync(userId);

    public async Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync() =>
        await _repo.GetAllVirtualCardsAsync();

    public async Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync(int userId) =>
        await _repo.GetAllVirtualCardsAsync(userId);

    public async Task<bool> TransferFromAccountToVirtualCardAsync(VirtualCardTransferMoneyDto dto) =>
        await _repo.TransferFromAccountToVirtualCardAsync(dto);

    public async Task<bool> TransferFromVirtualCardToAccountAsync(VirtualCardTransferMoneyDto dto) =>
        await _repo.TransferFromVirtualCardToAccountAsync(dto);
}