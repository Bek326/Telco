using System.Collections.ObjectModel;
using System.ComponentModel;
using Telco.Data;
using Telco.Models;

namespace Telco.Views;

public class StreetsViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private StreetRepository streetRepository;
    private ObservableCollection<Street> streets;

    public ObservableCollection<Street> Streets
    {
        get { return streets; }
        set
        {
            streets = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Streets)));
        }
    }

    public StreetsViewModel(string connectionString)
    {
        streetRepository = new StreetRepository(connectionString);
        Streets = new ObservableCollection<Street>(streetRepository.GetAllStreets());
    }

    public void RefreshStreets()
    {
        Streets = new ObservableCollection<Street>(streetRepository.GetAllStreets());
    }
}