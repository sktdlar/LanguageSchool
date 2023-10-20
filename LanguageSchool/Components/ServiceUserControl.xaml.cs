using System;
using System.Collections.Generic;
using System.IO;
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

namespace LanguageSchool.Components
{
    /// <summary>
    /// Логика взаимодействия для ServiceUserControl.xaml
    /// </summary>
    public partial class ServiceUserControl : UserControl
    {
        Service service;
        public ServiceUserControl(Service _service)
        {
            InitializeComponent();
            service = _service;
            if(App.AdmModeBool)
            {
                EditBtn.Visibility = Visibility.Visible;
                DeleteBtn.Visibility = Visibility.Visible;
            }
            else
            {
                EditBtn.Visibility = Visibility.Collapsed;
                DeleteBtn.Visibility = Visibility.Collapsed;
            }
            //byte[] image, string title, decimal cost, string CostTime,string DiscountString, Visibility CostVisibility
            JustCost.Text = $"{service.Cost:0} "; 
            ServiceImg.Source = GetImageSource(service.MainImage);
            TitleTb.Text = service.Title;
            CostTb.Text = service.CostTime;
            DiscountTB.Text = service.DiscountString;
            JustCost.Visibility = service.CostVisibility;
            MainBorder.Background = service.DiscountBrush;

        }
        private static ImageSource GetImageSource(byte[] image)
        {

            BitmapImage biImg = new BitmapImage(); 
            try
            {
            MemoryStream ms = new MemoryStream(image);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();
            }
            catch
            {
                MessageBox.Show("error");
            }
            
                return biImg as ImageSource;
            
            
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (service.ClientService != null)
            {
                MessageBox.Show("Удаление запрещено");
            }
            else
            {
                MessageBox.Show("все удалилось");
            }
        }
    }
}
