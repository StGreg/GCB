using GCB.Common;
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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace GCB.ViewModels
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    /// 


    public class ItemsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PlannedNotOffered { get; set; }
        public DataTemplate PlannedOffered { get; set; }
        public DataTemplate OngoingYours { get; set; }
        public DataTemplate OngoingOthers { get; set; }
        public DataTemplate ClosedYours { get; set; }
        public DataTemplate ClosedOthers { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var listItem = item as InvestmentResponse.List;
            if (InvestmentLists.selector == "3" && listItem.assignedBranches.Count == 0)
            {
                return ClosedOthers;
            }
            else if (InvestmentLists.selector == "3")
            {
                return ClosedYours;
            }
            return base.SelectTemplateCore(item, container);
        }
    }

    public sealed partial class InvestmentLists : Page
    {

        public static string selector;

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public InvestmentLists()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            string id = (string)e.NavigationParameter;

            InvestmentListData investmentListData = (InvestmentListData)App.Current.Resources["investmentListData"];
            await investmentListData.GetInvestemntListData(((App)(App.Current)).sessionId, ((App)(App.Current)).deviceId, id);

            if (investmentListData != null)
            {
                List<InvestmentResponse.List> sortedInvestmentList = investmentListData.InvDatas[0].data.list.OrderBy(o => o.date_from).ToList();

                this.DefaultViewModel["Items"] = sortedInvestmentList;

                if (id == "1")
                {
                    pageTitle.Text = "Planowane inwestycje";
                    selector = "1";
                }
                if (id == "2")
                {
                    pageTitle.Text = "Trwające inwestycje";
                    selector = "2";
                }
                if (id == "3")
                {
                    pageTitle.Text = "Zakończone inwestycje";
                    selector = "3";
                }
            }
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        private void itemListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                //var item = itemGridView.SelectedItem;
                string id = ((InvestmentResponse.List)e.ClickedItem).id;
                this.Frame.Navigate(typeof(InvDetailsPage), id);
            }
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

    }
}
