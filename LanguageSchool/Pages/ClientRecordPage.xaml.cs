using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LanguageSchool.Components;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LanguageSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientRecordPage.xaml
    /// </summary>
    public partial class ClientRecordPage : Page
    {
        Service service;
        public ClientRecordPage(Service _service)
        {
            InitializeComponent();
            service = _service;
            this.DataContext = service;
            ClientCb.ItemsSource = App.db.Client.ToList();
            ClientCb.DisplayMemberPath = "FullName";
            DateDp.DisplayDateStart = DateTime.Today;
        }


        

        private void StartTime_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (StartTime.Text.Length == 5 && DateTime.TryParse(StartTime.Text, out DateTime resultTime))
            {
                EndTime.Text = resultTime.AddSeconds(service.DurationInSeconds).ToShortTimeString();
            }
        }

        private void RecordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ClientCb.SelectedItem != null &&
                DateDp.SelectedDate != null && StartTime.Text != "")
            {
                var startDate = $"{DateDp.SelectedDate.Value.ToString("dd.MM.yyyy")} {StartTime.Text}";
                if (DateTime.TryParse(startDate, out DateTime dateTimeStart))
                {
                    if (dateTimeStart > DateTime.Now)
                    {
                        var selecTClient = ClientCb.SelectedItem as Client;
                        App.db.ClientService.Add(new ClientService()
                        {
                            ClientID = selecTClient.ID,
                            ServiceID = service.ID,
                            StartTime = dateTimeStart,
                        });
                        App.db.SaveChanges();
                        MessageBox.Show("Запись добавлена");
                        Navigation.BackPage();
                    }
                    else
                    {
                        MessageBox.Show("Время прошло");
                    }
                }
                else
                {
                    MessageBox.Show("Время введено неверно");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
    }
}
