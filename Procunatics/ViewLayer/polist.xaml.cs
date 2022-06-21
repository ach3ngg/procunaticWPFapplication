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
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using MaterialDesignThemes.Wpf;

namespace Procunatics.ViewLayer
{
    /// <summary>
    /// Interaction logic for polist.xaml
    /// </summary>
    public partial class polist : UserControl
    {

        private DBAccess.connection conn = new DBAccess.connection();
        private MySqlCommand command;

        public polist()
        {
            InitializeComponent();

            try
            {
                conn.OpenConnection();

                command = new MySqlCommand("Select poID, vendorID, employeeID, status, date FROM procunatics.purchaseorder", conn.get_connection());
                object a = command.ExecuteScalar();

                if (a == null)
                {
                    MessageBox.Show("Kenot show data..who you?");
                }
                else
                {
                    MySqlDataAdapter adp = new MySqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    adp.Fill(ds, "purchaseOrderList");
                    purchaseOrderDataGrid.DataContext = ds;
                }
            }

            catch (MySqlException x)
            {
                MessageBox.Show("" + x);
            }
        }
    }
}
