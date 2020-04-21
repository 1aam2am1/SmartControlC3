using SmartControl.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartControl
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public event Action<Credentials> OnLoginChange;
        public event Action OnSettings;

        ILoginSettings ILoginSettings;

        public Login()
        {
            InitializeComponent();
        }

        public void SetCredentials(ILoginSettings s)
        {
            ILoginSettings = s;

            UserName1.Text = ILoginSettings.Credentials.UserName;
            PasswordBox1.Password = ILoginSettings.Credentials.Password;
        }

        void LoginButton(object sender, RoutedEventArgs e)
        {
            Credentials credentials = new Credentials();
            credentials.UserName = UserName1.Text;
            credentials.Password = PasswordBox1.Password;

            OnLoginChange?.Invoke(credentials);
        }

        void SettingsButton(object sender, RoutedEventArgs e)
        {
            OnSettings?.Invoke();
        }
    }
}
