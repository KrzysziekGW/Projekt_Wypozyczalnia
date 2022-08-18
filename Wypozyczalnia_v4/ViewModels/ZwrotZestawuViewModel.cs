using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wypozyczalnia_v4.DbCon;

namespace Wypozyczalnia_v4.ViewModels
{
    public partial class ZwrotZestawuViewModel : UserControl
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Wypozyczalnia;Integrated Security=True";
        public ZwrotZestawuViewModel()
        {
            InitializeComponent();
            TabelWypożyczone();
        }

        private void ButtonZwrotZestawu_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand check_Klient = new SqlCommand("SELECT COUNT(*) FROM Klienci where  (ID = @ID)", connection);
            check_Klient.Parameters.AddWithValue("@ID", BoxKlientID.Text);
            int KlientExist = (int)check_Klient.ExecuteScalar();

            SqlCommand check_Zestaw = new SqlCommand("SELECT COUNT(*) FROM Zestaw WHERE (ID = @ID)", connection);
            check_Zestaw.Parameters.AddWithValue("@ID", BoxZestawID.Text);
            int ZestawExist = (int)check_Zestaw.ExecuteScalar();

            SqlCommand check_ZestawX = new SqlCommand("SELECT COUNT(*) FROM Zestaw inner join Wypożyczone on Wypożyczone.ZestawID = Zestaw.ID where (Zestaw.ID = @ID) and Wypożyczone.DataOddania is null", connection);
            check_ZestawX.Parameters.AddWithValue("@ID", BoxZestawID.Text);
            int ZestawXExist = (int)check_ZestawX.ExecuteScalar();


            using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))


                if (ZestawExist > 0 && KlientExist > 0 && ZestawXExist > 0 )
                {

                    db.Database.ExecuteSqlRaw("EXEC ZwrotZestawu " + Int32.Parse(BoxKlientID.Text) + "," + Int32.Parse(BoxZestawID.Text) + ",'" + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "'");
                    db.Database.ExecuteSqlRaw("EXEC ZmieńStatus " + Int32.Parse(BoxZestawID.Text) + " , 1");
                    db.SaveChanges();
                    TabelWypożyczone();
                }
                else
                {
                    MessageBox.Show("Podaj odpowednie ID!");
                }


        }
        private void ButtonObliczKwotęDoZapłaty_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand check_Klient = new SqlCommand("SELECT COUNT(*) FROM Klienci where  (ID = @ID)", connection);
            check_Klient.Parameters.AddWithValue("@ID", BoxKlientID.Text);
            int KlientExist = (int)check_Klient.ExecuteScalar();

            SqlCommand check_Zestaw = new SqlCommand("SELECT COUNT(*) FROM Zestaw WHERE (ID = @ID)", connection);
            check_Zestaw.Parameters.AddWithValue("@ID", BoxZestawID.Text);
            int ZestawExist = (int)check_Zestaw.ExecuteScalar();

            using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
                if (ZestawExist > 0 && KlientExist > 0)
                {


                    db.Database.ExecuteSqlRaw("EXEC DodajKwoteDoZapłaty " + Int32.Parse(BoxKlientID.Text) + "," + Int32.Parse(BoxZestawID.Text));
                    db.SaveChanges();
                    TabelWypożyczone();

                }
                else
                {
                    MessageBox.Show("Podaj odpowiednie ID");
                }

        }
        private void ButtonWyszukajKlienta_Click(object sender, RoutedEventArgs e)
        {


            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand check_Imie = new SqlCommand("SELECT COUNT(*) FROM Klienci where  (Imie = @Imie)", connection);
            check_Imie.Parameters.AddWithValue("@Imie", BoxImieKlient.Text);
            int ImieExist = (int)check_Imie.ExecuteScalar();

            SqlCommand check_Nazwisko = new SqlCommand("SELECT COUNT(*) FROM Klienci WHERE (Nazwisko = @Nazwisko)", connection);
            check_Nazwisko.Parameters.AddWithValue("@Nazwisko", BoxNazwiskoKlient.Text);
            int NazwiskoExist = (int)check_Nazwisko.ExecuteScalar();

            SqlCommand check_Numer = new SqlCommand("SELECT COUNT(*) FROM Klienci WHERE (Telefon = @Telefon)", connection);
            check_Numer.Parameters.AddWithValue("@Telefon", BoxTelefonKlient.Text);
            int NumerExist = (int)check_Numer.ExecuteScalar();


            if (ImieExist > 0 && NazwiskoExist > 0 && NumerExist > 0)
            {
                TabelWyszukajKlienta();
                MessageBox.Show("Znaleziono klienta!");
            }
            else
            {
                MessageBox.Show("Nie znaleziono klienta!");
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
        public void TabelWyszukajKlienta()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select * from WyszukajKlienta('" + BoxImieKlient.Text + "','" + BoxNazwiskoKlient.Text + "','" + Int32.Parse(BoxTelefonKlient.Text) + "')", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            WyszukajKlientaDataGrid.DataContext = dt;
        }

        private void BoxNazwiskoKlienta_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void BoxImieKlienta_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void BoxTelefonKlienta_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
