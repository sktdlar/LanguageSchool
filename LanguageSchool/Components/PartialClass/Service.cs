using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LanguageSchool.Components
{
    
    public partial class Service
    {
        public decimal ServiceCost
        {
            get
            {
                if(Discount is null)
                {
                    return Cost;
                }
                else
                {
                    return Cost - Cost * (decimal)Discount / 100;
                }
            }
        }
        public string CostTime
        {
            get
            {
                if(Discount is null)
                {
                    return $"{Cost:0} рублей за {DurationInSeconds / 60} минут";
                }
                else
                {
                    return $"{(double)Cost - (double)Cost / 100 * Discount:0} рублей за {DurationInSeconds / 60} минут";
                }
            }
            
        }
        public string DiscountString
        {
            get
            {
                if (Discount is null)
                {
                    return "";
                }
                else { return $"Скидка {Discount}%"; }
            }
        }
        public Visibility CostVisibility { get
            {
                if(Discount is null) { return Visibility.Collapsed; }
                else { return Visibility.Visible; }
            } }
        public Brush DiscountBrush
        {
            get
            {
                if (Discount is null)
                {
                    return new SolidColorBrush(Colors.White);
                }
                else 
                {
                    return new SolidColorBrush(Colors.LightGreen);
                }
            }
        } 
    }
}
