using LanguageSchool.Components;
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

namespace LanguageSchool
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //var path = @"C:\Users\Данил\Desktop\DemoExam\Сессия 1\";
            //foreach (var item in App.db.Service.ToList())
            //{
            //    var fullPath = path + item.MainImagePath.Trim();
            //    var imageByte = File.ReadAllBytes(fullPath);
            //    item.MainImage = imageByte;
            ////}
            //App.db.SaveChanges();
            Navigation.mainWindow = this;
            Navigation.NextPage(new PageComponent( new Pages.ServicePage(), "ServicePage"));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(App.AdmModeBool is false)
            {
                if(PassForAdm.Password == "0")
                {
                    App.AdmModeBool = true;
                    MessageBox.Show("Режим админа включен");
                    
                    AdmModeCheck.Visibility = Visibility.Visible;
                    PassForAdm.Password = "";
                    Navigation.ClearComponents();
                    Navigation.NextPage(new PageComponent(new Pages.ServicePage(), "ServicePage"));
                    
                }
                else
                {
                    MessageBox.Show("Неправильный пароль");
                    PassForAdm.Password = "";
                }
            }
            else
            {
                App.AdmModeBool = false;
                MessageBox.Show("Режим админа выключен");
                AdmModeCheck.Visibility = Visibility.Collapsed;
                PassForAdm.Password = "";
                Navigation.ClearComponents() ;
                Navigation.NextPage(new PageComponent(new Pages.ServicePage(), "ServicePage"));
                
            }
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
        }
    }
}
