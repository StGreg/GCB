using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            if ((email.Text == "") || (pass.Password == ""))
            {
                var logerror = new MessageDialog("Pola E-mail oraz Hasło nie mogą być puste!");
                var errorMsg = logerror.ShowAsync();
            }
            else
            {
                LoginData loginData = (LoginData)App.Current.Resources["loginData"];
                await loginData.GetLoginData(email.Text, pass.Password, ((App)(App.Current)).deviceId);

                if (loginData.LoginDatas[0].status == "OK")
                {
                    ((App)(App.Current)).sessionId = loginData.LoginDatas[0].data.sessionId;
                    ((App)(App.Current)).userName = loginData.LoginDatas[0].data.user.name;
                    ((App)(App.Current)).userEmail = loginData.LoginDatas[0].data.user.email;
                    this.Frame.Navigate(typeof(Main));
                    //this.Frame.Navigate(typeof(test2));
                }
                if (loginData.LoginDatas[0].status != "OK")
                {
                    var logerror = new MessageDialog("Niewłaściwy e-mail lub hasło!\nSpróbuj ponownie.");
                    var errorMsg = logerror.ShowAsync();
                    email.Text = "";
                    pass.Password = "";
                    //loginData.LoginDatas.Clear();
                }
            }


        }
    }
}
