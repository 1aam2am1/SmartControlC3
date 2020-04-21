using SmartControl.Api;
using System;
using System.Collections.Generic;
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
using System.Configuration;

namespace SmartControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataManager dataManager;

        public MainWindow()
        {
            InitializeComponent();

            dataManager = new DataManager();
            MyUserSettings.Instance.Restore(dataManager);

            Login login = new Login();
            login.SetCredentials(dataManager);

            login.OnLoginChange += OnLoginChange;
            login.OnSettings += OnSettingsChange;

            DataContext = login;
        }


        void OnLoginChange(Credentials credentials)
        {
            LoadingScreen loading = new LoadingScreen();
            dataManager.Credentials = credentials;


            DataContext = loading;
        }

        void OnSettingsChange()
        {
            SettingsView settings = new SettingsView();
            settings.setConnectSettings(dataManager);

            DataContext = settings;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MyUserSettings.Instance.Save(dataManager);
            MyUserSettings.Instance.Save();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
