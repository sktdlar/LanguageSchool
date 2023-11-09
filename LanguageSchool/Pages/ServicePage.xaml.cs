using LanguageSchool.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            if(!App.AdmModeBool){AddBtn.Visibility = Visibility.Hidden;}
            else 
            { 
                AddBtn.Visibility = Visibility.Visible;
                Refresh();
            }
            Refresh();
            
            

        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void DiscountFilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            IEnumerable<Service> services = App.db.Service;
            if(SortCb.SelectedIndex != 0)
            {
                if(SortCb.SelectedIndex == 1)
                {
                    services = services.OrderBy(x => x.ServiceCost);
                }
                else
                {
                    services = services.OrderByDescending(x => x.ServiceCost);
                }

            }
            if(DiscountFilterCb.SelectedIndex != 0)
            {
                if(DiscountFilterCb.SelectedIndex == 1)
                {
                    services = services.Where(x => x.Discount == null || x.Discount < 5);
                }
                else if (DiscountFilterCb.SelectedIndex == 2)
                {
                    services = services.Where(x => x.Discount >= 5 && x.Discount < 15);
                }
                else if (DiscountFilterCb.SelectedIndex == 3)
                {
                    services = services.Where(x => x.Discount >= 15 && x.Discount < 30);
                }
                else if (DiscountFilterCb.SelectedIndex == 4)
                {
                    services = services.Where(x => x.Discount >= 30 && x.Discount < 70);
                }
                else if (DiscountFilterCb.SelectedIndex == 5)
                {
                    services = services.Where(x => x.Discount >= 70 && x.Discount <= 100);
                }
                
            }
            if(SearchTb.Text != null)
            {
                services = services.Where(x => x.Title.Contains(SearchTb.Text));
            }
            ServiceWrapPanel.Children.Clear();
            foreach (var service in services)
            {
                ServiceWrapPanel.Children.Add(
                    new ServiceUserControl(service));
            }
            CountOfItems.Text = $"{services.Count()} из {App.db.Service.Count()}";
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent(new Pages.AddEditPage(new Service()), "ДОБАВЛЕНИЕ УСЛУГИ"));

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void AllRecords_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent(new Pages.UpcomingEntriesPage(), "Все записи")) ;
        }
    }
}
