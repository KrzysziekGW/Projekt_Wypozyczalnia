using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlClient;
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
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand check_Klient = new SqlCommand("SELECT COUNT(*) FROM Klienci where  (ID = @ID)", connection);
            check_Klient.Parameters.AddWithValue("@ID", BoxKlientID.Text);
            int KlientExist = (int)check_Klient.ExecuteScalar();

            SqlCommand check_Zestaw = new SqlCommand("SELECT COUNT(*) FROM Zestaw WHERE (ID = @ID)", connection);
            check_Zestaw.Parameters.AddWithValue("@ID", BoxZestawID.Text);
            int ZestawExist = (int)check_Zestaw.ExecuteScalar();




            if (KlientExist > 0 && ZestawExist > 0)
            {
                using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
                {
                    db.Database.ExecuteSqlInterpolated($"EXEC Wypożycz { Int32.Parse(BoxKlientID.Text)},{Int32.Parse(BoxZestawID.Text)},{DateTime.Now.ToString("MM/dd/yyyy h:mm tt")},{null},{2}");
                    db.Database.ExecuteSqlRaw("EXEC ZmieńStatus " + Int32.Parse(BoxZestawID.Text) + " , 2");
                    db.SaveChanges();
                    TabelWypożyczone();

                }
            }
            else
            {
                MessageBox.Show("Podaj odpowiednie ID!");
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
