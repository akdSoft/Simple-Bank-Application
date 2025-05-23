﻿using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Data;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;

namespace Simple_Bank_Application.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly AppDbContext _context;

    public BankAccountRepository(AppDbContext context) => _context = context;


    public async Task<IEnumerable<BankAccountDto>> GetAllBankAccountsAsync()
    {
        return await _context.BankAccounts
            .Include(acc => acc.User)
            .Select(acc => new BankAccountDto
            {
                Id = acc.Id,
                Balance = acc.Balance,
                UserId = acc.UserId,
                UserName = acc.User.Name,
                UserSurname = acc.User.Surname
            }).ToListAsync();

    }

    public async Task<BankAccountDto?> GetBankAccountByIdAsync(int id)
    {
        return await _context.BankAccounts
            .Include(acc => acc.User)
            .Where(acc => acc.Id == id)
            .Select(acc => new BankAccountDto
            {
                Id = acc.Id,
                Balance = acc.Balance,
                UserId = acc.UserId,
                UserName = acc.User.Name,
                UserSurname = acc.User.Surname
            }).FirstOrDefaultAsync();
    }

    public async Task<BankAccountDto?> CreateBankAccountAsync(int userId)
    {
        var bankAccount = new BankAccount
        {
            UserId = userId,
            Balance = 0
        };

        _context.BankAccounts.Add(bankAccount);
        await _context.SaveChangesAsync();

        var createdAccount = await _context.BankAccounts
            .Include(acc => acc.User)
            .Where(acc => acc.Id == bankAccount.Id)//??
            .Select(acc => new BankAccountDto
            {
                Id = acc.Id,
                Balance = acc.Balance,
                UserId = acc.UserId,
                UserName = acc.User.Name,
                UserSurname = acc.User.Surname
            }).FirstOrDefaultAsync();

        return createdAccount;
    }

    public async Task<bool> DeleteBankAccountAsync(int id)
    {
        var deleted = await _context.BankAccounts.FindAsync(id);
        if (deleted == null) return false;

        

        _context.BankAccounts.Remove(deleted);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<BankAccountDto?> IncreaseOrDecreaseBalanceAsync(int accountId, decimal amount)
    {
        var account = await _context.BankAccounts.FindAsync(accountId);
        account.Balance += amount;
        await _context.SaveChangesAsync();

        var accountDto = await _context.BankAccounts
            .Include(acc => acc.User)
            .Where(acc => acc.Id == accountId)
            .Select(acc => new BankAccountDto
            {
                Id = acc.Id,
                Balance = acc.Balance,
                UserId = acc.UserId,
                UserName = acc.User.Name,
                UserSurname = acc.User.Surname
            }).FirstOrDefaultAsync();

        return accountDto;
    }

    public async Task<IEnumerable<BankAccountDto?>> GetBankAccountsByUserId(int userId)
    {
        return await _context.BankAccounts
            .Include(acc => acc.User)
            .Where(acc => acc.UserId == userId)
            .Select(acc => new BankAccountDto
            {
                Id = acc.Id,
                Balance = acc.Balance,
                UserId = acc.UserId,
                UserName = acc.User.Name,
                UserSurname = acc.User.Surname
            }).ToListAsync();
    }
}
