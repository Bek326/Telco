using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Telco.Commands;
using Telco.Data;
using Telco.Models;

namespace Telco.Views;

public class SearchViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private PhoneNumberRepository phoneNumberRepository;
    private string searchNumber;
    private ObservableCollection<Abonent> searchResult;

    public string SearchNumber
    {
        get { return searchNumber; }
        set
        {
            searchNumber = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchNumber)));
        }
    }

    public ObservableCollection<Abonent> SearchResult
    {
        get { return searchResult; }
        set
        {
            searchResult = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchResult)));
        }
    }

    public ICommand SearchCommand { get; }

    public SearchViewModel(PhoneNumberRepository phoneNumberRepository)
    {
        this.phoneNumberRepository = phoneNumberRepository;
        SearchCommand = new RelayCommand(() => Search(true), () => CanSearch(true));
    }

    private bool CanSearch(object parameter)
    {
        // Проверка на корректность ввода номера телефона для активации кнопки поиска
        return !string.IsNullOrWhiteSpace(SearchNumber);
    }

    private void Search(object parameter)
    {
        try
        {
            SearchResult = new ObservableCollection<Abonent>(phoneNumberRepository.GetAbonentsByPhoneNumber(SearchNumber));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}