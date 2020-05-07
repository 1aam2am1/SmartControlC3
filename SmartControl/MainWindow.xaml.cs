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
using SmartControl.Settings;
using SmartControl.Api.Data;

namespace SmartControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ConnectSettings settings = new ConnectSettings();
        readonly Client client = new Client();

        private readonly Login login = new Login();

        public MainWindow()
        {
            InitializeComponent();

            Credentials credentials = new Credentials();

            MyUserSettings.Instance.Restore(settings);
            MyUserSettings.Instance.Restore(credentials);
            login.SetCredentials(credentials);

            login.OnLoginChange += OnLoginChange;
            login.OnSettings += OnSettingsChange;

            DataContext = login;
        }

        void OnLoginChange(Credentials credentials)
        {
            LoadingScreen loading = new LoadingScreen();

            DataContext = loading;

            client.Connect(settings, credentials, async (n) =>
            {
                if (n)
                {
                    MyUserSettings.Instance.Save(credentials);

                    var work = new WorkView();
                    await work.SetClient(client);

                    Dispatcher.Invoke(() => { DataContext = work; });

                    client.StartDataSynchronization();
                }
                else
                {
                    DataContext = login;
                }

            });
        }

        void OnSettingsChange()
        {
            SettingsView settings = new SettingsView();
            settings.SetConnectSettings(this.settings);
            settings.OnSettingsClose += OnSettingsClose;

            DataContext = settings;
        }

        void OnSettingsClose()
        {
            MyUserSettings.Instance.Save(this.settings);
            DataContext = login;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MyUserSettings.Instance.Save();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}
