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
using MySql.Data.MySqlClient;
using MaterialDesignThemes.Wpf;

namespace Procunatics.ViewLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string username, password, sql;
        private DBAccess.connection conn = new DBAccess.connection();
        private MySqlCommand command;


        public MainWindow()
        {
            InitializeComponent();
        }

        public bool IsDarkTheme {get; set;}
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }

            paletteHelper.SetTheme(theme);            
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void loginOnClick(object sender, RoutedEventArgs e)
        {
            username = txtUsername.Text;
            password = txtPassword.Password;

            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Aiyaa..kenot be empty one..how check?");
            }

            else
            {
                sql = "SELECT username, email FROM employee WHERE username = '" + username + "' AND password = '" + password + "'";

                if( conn.OpenConnection() == true)
                {
                    try
                    {
                        command = new MySqlCommand(sql, conn.get_connection());
                        object a = command.ExecuteScalar();

                        if(a == null)
                        {
                            MessageBox.Show("Whos dis? Wrong password/username lah. Again try!");
                        }
                        else
                        {
                            Dashboard dashboard = new Dashboard();
                            dashboard.Show();
                            this.Close();
                        }
                    }

                    catch (MySqlException x)
                    {
                        MessageBox.Show("" + x);
                    }

                }
            }

            txtUsername.Text = "";
            txtPassword.Password = "";
            conn.close_conn();
        }

    }
}
