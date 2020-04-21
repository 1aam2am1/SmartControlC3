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

        public Login()
        {
            InitializeComponent();
        }

        void LoginButton(object sender, RoutedEventArgs e)
        {
            Credentials credentials = new Credentials();
            credentials.UserName = UserName1.Text;
            credentials.Password = PasswordBox1.Password;

            OnLoginChange?.Invoke(credentials);
        }
    }
}
