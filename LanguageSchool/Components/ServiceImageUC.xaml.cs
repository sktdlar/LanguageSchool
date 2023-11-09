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
    /// Логика взаимодействия для ServiceImageUC.xaml
    /// </summary>
    public partial class ServiceImageUC : UserControl
    {
        ServicePhoto service1;
        public ServiceImageUC(ServicePhoto service)
        {
            InitializeComponent();
            service1 = service;
            MemoryStream ms = new MemoryStream(service.Photo);
            BitmapImage BiImg = new BitmapImage();
            BiImg.BeginInit();
            BiImg.StreamSource = ms;
            BiImg.EndInit();
            ServiceImg.Source = BiImg as ImageSource;
        }

        private void SelectAsMain_Click(object sender, RoutedEventArgs e)
        {
            var ServiceId = service1.ServiceID;
            var service = App.db.Service.Find(ServiceId);
            var imagePath = service.MainImagePath;
            var image = service.MainImage;
            service.MainImage = service1.Photo;
            service.MainImagePath = service1.PhotoPath;
            //App.db.ServicePhoto.Find(service.ID).PhotoPath = imagePath;
            //App.db.ServicePhoto.Find(service.ID).Photo = image;
            App.servicePage.Refresh();
            App.db.SaveChanges();
            //App.servicePage.ServiceImage = (Image)Service.ServicePhoto;/**/
            MemoryStream ms = new MemoryStream(service.MainImage);
            BitmapImage BiImg = new BitmapImage();
            BiImg.BeginInit();
            BiImg.StreamSource = ms;
            BiImg.EndInit();
            ServiceImg.Source = BiImg as ImageSource;
            App.servicePage.ServiceImage.Source = BiImg;
            App.servicePage.Refresh();

        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var ServiceId = service1.ServiceID;
            if(App.db.Service.Find(ServiceId).MainImage == service1.Photo)
            {
                App.db.Service.Find(ServiceId).MainImage = null;
                App.db.Service.Find(ServiceId).MainImagePath = null;
                App.db.ServicePhoto.Remove(service1);
                App.db.SaveChanges();
                MessageBox.Show("Фото удалено со страницы услуги. Главное фото так же было удалено");
                App.servicePage.Refresh();
                

            }
            else
            {
                App.db.ServicePhoto.Remove(service1);
                App.db.SaveChanges();
                MessageBox.Show("Фото удалено со страницы услуги.");
                App.servicePage.Refresh();

            }


        }
    }
}
