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
using Wypozyczalnia_v4.DbCon;

namespace Wypozyczalnia_v4.ViewModels
{
    public partial class ZwrotZestawuViewModel : UserControl
    {
        string connectionString = @"Data Source=DESKTOP-QR4BK4H;Initial Catalog=Wypozyczalnia;Integrated Security=True";
        public ZwrotZestawuViewModel()
        {
            InitializeComponent();
            TabelWypożyczone();
        }

        private void ButtonZwrotZestawu_Click(object sender, RoutedEventArgs e)
        {
            if (BoxKlientID.Text != "" && BoxKlientID.Text != "")
            {
                using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
                {
                    db.Database.ExecuteSqlCommand("EXEC ZwrotZestawu " + Int32.Parse(BoxKlientID.Text) + "," + Int32.Parse(BoxZestawID.Text) + ",'" + DateTime.Now + "'");
                    db.SaveChanges();
                    TabelWypożyczone();
                }
            }
            else
            {
                MessageBox.Show("Wypełnij wszystie pola!");
            }


        }
        private void ButtonObliczKwotęDoZapłaty_Click(object sender, RoutedEventArgs e)
        {
            if (BoxKlientID.Text != "" && BoxKlientID.Text != "")
            {
                using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
                {
                    db.Database.ExecuteSqlCommand("EXEC DodajKwoteDoZapłaty " + Int32.Parse(BoxKlientID.Text) + "," + Int32.Parse(BoxZestawID.Text));
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
    }
}
