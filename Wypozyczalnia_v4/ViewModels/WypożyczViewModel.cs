using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wypozyczalnia_v4.DbCon;
using System.Windows.Controls;
namespace Wypozyczalnia_v4.ViewModels
{
    public partial class WypożyczViewModel : UserControl
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Wypozyczalnia;Integrated Security=True";

        public WypożyczViewModel()
        {
            InitializeComponent();
            TabelWypożyczone();
            TabelNajnowszyKlient();
            TabelNajnowszyZestaw();
        }


        private void ButtonWypożycz_Click(object sender, RoutedEventArgs e)
        {
            if (BoxKlientID.Text != "" && BoxKlientID.Text != "")
            {
                using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
                {
                    db.Database.ExecuteSqlCommand("EXEC Wypożycz " + Int32.Parse(BoxKlientID.Text) + "," + Int32.Parse(BoxZestawID.Text) + ",'" + DateTime.Now + "',null,2");
                    db.SaveChanges();
                    TabelWypożyczone();

                }
            }
            else
            {
                MessageBox.Show("Wypełnij wszystie pola!");
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
        public void TabelNajnowszyKlient()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select TOP 1 * from Klienci Order By ID Desc", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            TopKlientIdDataGrid.DataContext = dt;
        }
        public void TabelNajnowszyZestaw()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select TOP 1 * from Zestaw Order By ID Desc", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            TopZestawIdDataGrid.DataContext = dt;
        }
    }
}
