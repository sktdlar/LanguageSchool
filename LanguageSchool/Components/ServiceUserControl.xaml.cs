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
        public Service Service { get; set; }
        public ServiceUserControl(Service _service)
        {
            InitializeComponent();
            if (!App.AdmModeBool)
            {
                RecordBtn.Visibility = Visibility.Collapsed;
            }
            else
                RecordBtn.Visibility = Visibility.Visible;
            Service = _service;
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
            JustCost.Text = $"{Service.Cost:0} "; 
            ServiceImg.Source = GetImageSource(Service.MainImage);
            TitleTb.Text = Service.Title;
            CostTb.Text = Service.CostTime;
            DiscountTB.Text = Service.DiscountString;
            JustCost.Visibility = Service.CostVisibility;
            MainBorder.Background = Service.DiscountBrush;
            

        }
          ImageSource GetImageSource(byte[] image)
        {

            BitmapImage biImg = new BitmapImage(); 
            try
            {
                if (Service.MainImage != null) 
                {
                    MemoryStream ms = new MemoryStream(image);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();
                }
                else
                {
                    MemoryStream ms = new MemoryStream(File.ReadAllBytes("\\Sourse\\school_logo.png"));
                    biImg.BeginInit();
                    biImg.StreamSource = ms;
                    biImg.EndInit();
                }
            
            }
            catch
            {
                MessageBox.Show("error");
            }
            
                return biImg as ImageSource;
            
            
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (App.db.ClientService.Any(x => x.ServiceID == Service.ID))
            {
                MessageBox.Show("Удаление запрещено");
            }
            else
            {
                MessageBox.Show("все удалилось");
                
                App.db.Service.Remove(Service);
                App.db.SaveChanges();
                
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent( new Pages.AddEditPage(Service), "ИЗМЕНЕНИЕ УСЛУГИ"));
        }

        private void Record_Click(object sender, RoutedEventArgs e)
        {
            
            Navigation.NextPage(new PageComponent(new Pages.ClientRecordPage(Service), "Запись на услугу"));
        }
    }
}
