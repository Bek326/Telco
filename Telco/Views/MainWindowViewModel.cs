using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Telco.Commands;
using Telco.Data;
using Telco.Models;
using Telco.UI;

namespace Telco.Views;

public class MainWindowViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

        private AbonentRepository _abonentRepository;
        private ObservableCollection<Abonent> _abonents;

        public ObservableCollection<Abonent> Abonents
        {
            get { return _abonents; }
            set
            {
                _abonents = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Abonents)));
            }
        }

        public ICommand RefreshCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ExportCsvCommand { get; }
        public ICommand ShowStreetsCommand { get; }

        public MainWindowViewModel(string connectionString)
        {
            _abonentRepository = new AbonentRepository(connectionString);
            Abonents = new ObservableCollection<Abonent>(_abonentRepository.GetAllAbonents());

            RefreshCommand = new RelayCommand(RefreshAbonents);
            SearchCommand = new RelayCommand(ShowSearchWindow);
            ExportCsvCommand = new RelayCommand(ExportCsv);
            ShowStreetsCommand = new RelayCommand(ShowStreetsWindow);
        }

        private void RefreshAbonents()
        {
            Abonents = new ObservableCollection<Abonent>(_abonentRepository.GetAllAbonents());
        }

        private void ShowSearchWindow()
        {
            var searchWindow = new SearchWindow();
            searchWindow.ShowDialog();
        }

        private void ExportCsv()
        {
            // Путь к файлу CSV
            string filePath = "exported_data.csv";

            // Открываем файл для записи
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Заголовки столбцов
                writer.WriteLine("FullName,Street,HouseNumber,HomePhoneNumber,WorkPhoneNumber,MobilePhoneNumber");

                // Записываем данные каждого абонента
                foreach (var abonent in Abonents)
                {
                    // Формируем CSV строку для абонента
                    string csvLine = $"{abonent.FullName},{abonent.Address.Street},{abonent.Address.HouseNumber}," +
                                     $"{abonent.PhoneNumbers.FirstOrDefault(pn => pn.NumberType == "Home")?.Number ?? ""}," +
                                     $"{abonent.PhoneNumbers.FirstOrDefault(pn => pn.NumberType == "Work")?.Number ?? ""}," +
                                     $"{abonent.PhoneNumbers.FirstOrDefault(pn => pn.NumberType == "Mobile")?.Number ?? ""}";

                    // Записываем строку в файл
                    writer.WriteLine(csvLine);
                }
            }

            // Уведомляем пользователя об успешном экспорте
            Console.WriteLine("Данные успешно экспортированы в файл: " + filePath);
        }

        private void ShowStreetsWindow()
        {
            var streetsWindow = new StreetsWindow();
            streetsWindow.ShowDialog();
        }

        public void SearchAbonentsByPhoneNumber(string phoneNumber)
        {
            var filteredAbonents = _abonents.Where(a =>
                a.HomePhoneNumber == phoneNumber ||
                a.WorkPhoneNumber == phoneNumber ||
                a.MobilePhoneNumber == phoneNumber).ToList();

            if (filteredAbonents.Any())
                Abonents = new ObservableCollection<Abonent>(filteredAbonents);
            else
                Console.WriteLine("Номер телефона не найден!");
        }
}