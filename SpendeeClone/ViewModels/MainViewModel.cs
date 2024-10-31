using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SpendeeClone.Models;
using SpendeeClone.Services;
using System.Collections.ObjectModel;

namespace SpendeeClone.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private ObservableCollection<Transaction> transactions;

        [ObservableProperty]
        private decimal balance;

        public MainViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            LoadTransactions();
        }

        [RelayCommand]
        private async Task LoadTransactions()
        {
            var items = await _databaseService.GetTransactionsAsync();
            Transactions = new ObservableCollection<Transaction>(items);
            CalculateBalance();
        }

        [RelayCommand]
        private async Task AddTransaction(Transaction transaction)
        {
            await _databaseService.SaveTransactionAsync(transaction);
            await LoadTransactions();
        }

        private void CalculateBalance()
        {
            Balance = Transactions.Sum(t => t.Type == TransactionType.Income ? t.Amount : -t.Amount);
        }
    }
}