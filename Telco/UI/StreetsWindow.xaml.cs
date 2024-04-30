using System.Windows;
using Telco.Views;

namespace Telco.UI;

public partial class StreetsWindow : Window
{
    public StreetsWindow()
    {
        InitializeComponent();
        DataContext = new StreetsViewModel("Server=beksultan;Database=PhoneCompanyDB;User Id=AbonentTelelphone;Password=sa;Trusted_Connection=True;");
    }
}