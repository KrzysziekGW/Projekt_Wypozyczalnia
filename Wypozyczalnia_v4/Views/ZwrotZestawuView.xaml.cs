using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Wypozyczalnia_v4.DbCon;

namespace Wypozyczalnia_v4.Views
{
    /// <summary>
    /// Interaction logic for ZwrotZestawuView.xaml
    /// </summary>
    public partial class ZwrotZestawuView : UserControl
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Wypozyczalnia;Integrated Security=True";
        public ZwrotZestawuView()
        {
            InitializeComponent();
            TabelWypożyczone();
        }

        private void ButtonZwrotZestawu_Click(object sender, RoutedEventArgs e)
        {
            using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
            {
                db.Database.ExecuteSqlCommand("EXEC ZwrotZestawu " + Int32.Parse(BoxKlientID.Text) + "," + Int32.Parse(BoxZestawID.Text) + ",'" + DateTime.Now + "'");
                db.SaveChanges();
                TabelWypożyczone();
            }
        }
        private void ButtonObliczKwotęDoZapłaty_Click(object sender, RoutedEventArgs e)
        {
            using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
            {
                db.Database.ExecuteSqlCommand("EXEC DodajKwoteDoZapłaty " + Int32.Parse(BoxKlientID.Text) + "," + Int32.Parse(BoxZestawID.Text));
                db.SaveChanges();
                TabelWypożyczone();
            }
        }
        public void TabelWypożyczone()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select * from Wypożyczone", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            WypożyczoneDataGrid.DataContext = dt;
        }

        
    }
}
