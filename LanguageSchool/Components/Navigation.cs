using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LanguageSchool.Components
{
     static class Navigation
    {
        private static List<PageComponent> Components = new List<PageComponent>();
        public static MainWindow mainWindow;
        public static void NextPage(PageComponent pageComponent)
        {
            Components.Add(pageComponent);
            Update(Components[Components.Count - 1]);
        }
        public static void BackPage()
        {
            if(Components.Count() > 1)
            {
                Components.RemoveAt(Components.Count - 1);
                Update(Components[Components.Count - 1]);
            }
            
        }
        private static void Update(PageComponent pageComponent)
        {
            mainWindow.MainTb.Text = pageComponent.Title;
            mainWindow.GoBackBtn.Visibility = Components.Count() >1 ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            mainWindow.MainFrame.Navigate(pageComponent.Page);
            
        }
        public static void ClearComponents()
        {
            Components.Clear();
        }

    }

     class PageComponent 
    {
        public Page Page { get; set; }
        public string Title {get; set; }
        public PageComponent(Page page, string title)
        {
            Page = page;
            Title = title;
        }
    }
}
