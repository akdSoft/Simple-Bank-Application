using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepo;
    private readonly IUserRepository _userRepo;
    private readonly IBankAccountRepository _bankAccountRepo;

    public CardService(ICardRepository cardRepo, IUserRepository userRepo, IBankAccountRepository bankAccountRepo)
    {
        _cardRepo = cardRepo;
        _userRepo = userRepo;
        _bankAccountRepo = bankAccountRepo;
    }
    public async Task<DebitCard?> CreateDebitCardAsync(CreateDebitCardDto dto, int userId)
    {
        var (cardNumber, cvv) = GenerateCardNumberAndCvv();

        //Banka kartınu oluşturuyoruz
        var user = await _userRepo.GetUserWithPasswordByIdAsync(userId);

        if (user == null)
            return null;

        var debitCard = new DebitCard
        {
            UserId = user.Id,
            Type = "Debit Card",
            CardNumber = cardNumber,
            ExpirationDate = DateTime.Now.AddYears(10),
            CVV = cvv,
            CardholderNameAndSurname = user.Name + " " + user.Surname,
            OnlineShopping = dto.OnlineShopping,
            Password = dto.Password,
            LinkedAccountId = dto.LinkedAccountId,

        };

        return await _cardRepo.CreateDebitCardAsync(debitCard);
    }

    public async Task<VirtualCard?> CreateVirtualCardAsync(CreateVirtualCardDto dto, int userId)
    {
        var (cardNumber, cvv) = GenerateCardNumberAndCvv();

        //Sanal kartı oluşturuyoruz
        var user = await _userRepo.GetUserWithPasswordByIdAsync(userId);

        if (user == null)
            return null;

        var virtualCard = new VirtualCard
        {
            UserId = user.Id,
            Type = "Virtual Card",
            CardNumber = cardNumber,
            ExpirationDate = DateTime.Now.AddYears(10),
            CVV = cvv,
            CardholderNameAndSurname = user.Name + " " + user.Surname,
            OnlineShopping = dto.OnlineShopping,
            AvailableLimit = 0
        };

        return await _cardRepo.CreateVirtualCardAsync(virtualCard);
    }

    public async Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync() =>
        await _cardRepo.GetAllDebitCardsAsync();

    public async Task<IEnumerable<DebitCard>> GetAllDebitCardsAsync(int userId) =>
        await _cardRepo.GetAllDebitCardsAsync(userId);

    public async Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync() =>
        await _cardRepo.GetAllVirtualCardsAsync();

    public async Task<IEnumerable<VirtualCard>> GetAllVirtualCardsAsync(int userId) =>
        await _cardRepo.GetAllVirtualCardsAsync(userId);

    //Çift değer döndürmek için Tuple data tipini kullandık
    static (string CardNumber, string Cvv) GenerateCardNumberAndCvv()
    {
        //16 haneli kart numarasını oluşturuyoruz
        string cardNumber = "";
        Random rnd = new Random();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                cardNumber += Convert.ToString(rnd.Next(0, 10));
            }
            cardNumber += " ";
        }

        //CVV kodunu oluşturuyoruz
        string cvv = Convert.ToString(rnd.Next(100, 1000));

        return (cardNumber, cvv);
    }
}