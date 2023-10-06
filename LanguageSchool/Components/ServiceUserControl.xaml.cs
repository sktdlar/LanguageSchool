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
        public ServiceUserControl(byte[] image, string title, decimal cost, string CostTime, string discount, Visibility CostVisibility)
        {
            InitializeComponent();
            
                BitmapImage biImg = new BitmapImage();
                MemoryStream ms = new MemoryStream(image);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();
                ImageSource imgSrc = biImg as ImageSource;

            JustCost.Text = cost.ToString() + " ";
            ServiceImg.Source =  imgSrc;
            TitleTb.Text = title;
            CostTb.Text = CostTime;
            DiscountTB.Text = discount;
            JustCost.Visibility = CostVisibility;

        }
    }
}
