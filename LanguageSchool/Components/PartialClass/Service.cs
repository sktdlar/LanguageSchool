using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LanguageSchool.Components
{
    
    public partial class Service
    {
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
    }
}
