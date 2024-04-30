using System.Windows;
using Telco.Data;
using Telco.Views;

namespace Telco.UI;

public partial class SearchWindow : Window
{
    public SearchWindow()
    {
        InitializeComponent();
        DataContext = new SearchViewModel(new PhoneNumberRepository("Server=beksultan;Database=PhoneCompanyDB;User Id=AbonentTelelphone;Password=sa;Trusted_Connection=True;"));
    }
}