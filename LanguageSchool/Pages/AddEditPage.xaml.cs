using LanguageSchool.Components;
using Microsoft.Win32;
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
            if(service.ID == 0)
            {
                App.db.Service.Add(service);
            }
            else
            {
                App.db.SaveChanges  ();
            }
        }
    }
    }

