using System.Windows;
using Telco.Views;

namespace Telco.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel("Server=beksultan;Database=PhoneCompanyDB;User Id=AbonentTelelphone;Password=sa;Trusted_Connection=True;");
    }
}