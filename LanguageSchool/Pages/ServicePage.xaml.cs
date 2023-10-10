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
            else { AddBtn.Visibility = Visibility.Visible;}
            var services = App.db.Service.ToList();
            foreach (var service in services)
            {
                ServiceWrapPanel.Children.Add(
                    new ServiceUserControl(service.MainImage, service.Title, service.Cost, service.CostTime, service.DiscountString, service.CostVisibility));
            }
            
        }
    }
}
