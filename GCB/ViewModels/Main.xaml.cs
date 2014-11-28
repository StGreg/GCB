using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GCB.ViewModels
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Main : Page
    {

        public Main()
        {

            this.InitializeComponent();

        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = ((TextBlock)e.AddedItems[0]).Text;
            if (name == "Planowane inwestycje")
            {
                string id = "1";
                this.contentFrame.Navigate(typeof(InvestmentLists), id);
            }
            if (name == "Trwające inwestycje")
            {
                string id = "2";
                this.contentFrame.Navigate(typeof(InvestmentLists), id);
            }
            if (name == "Zakończone inwestycje")
            {
                string id = "3";
                this.contentFrame.Navigate(typeof(InvestmentLists), id);
            }
        }



    }
}
