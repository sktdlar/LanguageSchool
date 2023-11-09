using LanguageSchool.Components;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

namespace LanguageSchool.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        public string ImageSource = "\\Source\\school_logo.png";
        public byte[] ImageBinary;
        Service service { get; set; }

        public AddEditPage(Service _service)
        {
            InitializeComponent();
            this.DataContext = _service;
            service = _service;
            Refresh();
            if(service.ID == 0)
            {
                ServicePhotoWrap.Visibility = Visibility.Collapsed;
                AddNewImages.Visibility = Visibility.Collapsed;
                ScrollWrap.Visibility = Visibility.Collapsed;
            }
            else
            {
                ServicePhotoWrap.Visibility = Visibility.Visible;
                AddNewImages.Visibility = Visibility.Visible;
                ScrollWrap.Visibility = Visibility.Visible;
            }
            
        }
        
        public void Refresh()
        {
            ServicePhotoWrap.Children.Clear();
            App.servicePage = this;
            foreach (ServicePhoto sservice in service.ServicePhoto)
            {
                ServicePhotoWrap.Children.Add(new ServiceImageUC(sservice));
            }
        }
        private void AddImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "ПАЭНГЭ|*.png|ДЖЕПЕГ|*.jpg|ДЖПЕГ|*.jpeg"
                };
                if(openFileDialog.ShowDialog() != null)
                {
                    service.MainImage = File.ReadAllBytes(openFileDialog.FileName);
                    service.MainImagePath = openFileDialog.FileName;
                    BitmapImage BiImg = new BitmapImage();
                    MemoryStream ba = new MemoryStream(service.MainImage);
                    BiImg.BeginInit();
                    BiImg.StreamSource = ba;
                    BiImg.EndInit();
                    ServiceImage.Source = BiImg as ImageSource;
                    
                }
                
            }
            catch
            {
                
            }
            
                //try
                //{
                //    OpenFileDialog ofdPicture = new OpenFileDialog();
                //    ofdPicture.Filter =
                //        "Image files|*.bmp;*.jpg;*.gif;*.png;*.tif|All files|*.*";
                //    ofdPicture.FilterIndex = 1;

                //    if (ofdPicture.ShowDialog() == true)
                //    ServiceImage.Source =
                //            new BitmapImage(new Uri(ofdPicture.FileName));
                //    ImageSource = ofdPicture.FileName;
                //    ImageBinary = File.ReadAllBytes(ImageSource);
                //ServiceImage.Source = new BitmapImage(new Uri(ImageSource));
                //}
                //catch
                //{
                //    MessageBox.Show("что-то не так(((");
                //}



            }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if(App.db.Service.Any(x => x.Title == service.Title))
            {
                sb = sb.AppendLine("Услуга уже существует!");
            }
            else if(service.DurationInSeconds / 360 > 4)
            {
                sb = sb.AppendLine("Услуга не может длиться более 4-х часов");

            }
            if(sb != null)
            {
                try
                {
                    if (service.ID == 0)
                    {
                        //Service NewService = new Service() 
                        //{
                        //    Title = service.Title,
                        //    MainImagePath = service.MainImagePath,
                        //    MainImage = service.MainImage,
                        //    Cost = Decimal.Parse(ServiceCostTb.Text),
                        //    Description = DescriptionTb.Text,
                        //    DurationInSeconds = int.Parse(ServiceDurationTb.Text)

                        //};
                        App.db.Service.Add(service);
                        App.db.SaveChanges();
                        MessageBox.Show("Сохранено");
                        NavigationService.GoBack();
                    }
                    else
                {
                    App.db.SaveChanges();
                    MessageBox.Show("Изменения внесены и сохранены");
                    NavigationService.GoBack();
                }

                }
                catch 
                {
                    MessageBox.Show("Не все поля заполнены!!!");
                    /*NavigationService.GoBack();*/
                }
                /*catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }*/

            }
            else
            {
                MessageBox.Show(sb.ToString());
            }
            
        }

        private void ServiceNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ServiceCostTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text[0]))){
                e.Handled = true;
            }
        }

        private void AddNewImages_Click(object sender, RoutedEventArgs e)
        {
            if(service.ID == 0)
            {
                MessageBox.Show("Сначала сохраните услугу!");
            }
            else
            {
                try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = "ПАЭНГЭ|*.png|ДЖЕПЕГ|*.jpg|ДЖПЕГ|*.jpeg"
                };
                if (openFileDialog.ShowDialog() != null)
                {
                    var ServiceImage = File.ReadAllBytes(openFileDialog.FileName);
                    ServicePhoto servicePhoto = new ServicePhoto() {
                        ServiceID = service.ID,
                        PhotoPath = openFileDialog.FileName,
                        Photo = ServiceImage
                    };
                    App.db.ServicePhoto.Add(servicePhoto);
                    App.db.SaveChanges();
                    Refresh();


                }

            }
            catch
            {
                MessageBox.Show("Ошибка");
                Refresh();
            }
            }
            
        }
        
    }
}

