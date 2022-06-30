using System.Windows;
using System.Windows.Controls;
using Wypozyczalnia_v4.DbCon;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Text.RegularExpressions;

namespace Wypozyczalnia_v4.Views
{
    /// <summary>
    /// Interaction logic for DodajKlientaVIew.xaml
    /// </summary>
    public partial class DodajKlientaVIew : UserControl
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Wypozyczalnia;Integrated Security=True";

        

        public DodajKlientaVIew()
        {
            InitializeComponent();

            TabelKlienci();

        }
        private void ButtonDodajKlienta_Click(object sender, RoutedEventArgs e)
        {

            using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
            {
                db.Add(new Klient { Imie = BoxImie.Text, Nazwisko = BoxNazwisko.Text, PESEL = BoxPesel.Text, Email = BoxEmail.Text, Telefon = BoxTelefon.Text });
                db.SaveChanges();

            }

            TabelKlienci();
        }

        
        private void ButtonUsunKlienta_Click(object sender, RoutedEventArgs e)
        {

            using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
            {
                string klientID = BoxID.Text;
                int id = Int32.Parse(klientID);
                db.Remove(new Klient { Id = id });
                db.SaveChanges();

            }

            TabelKlienci();
        }



        private void BoxImie_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BoxNazwisko_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void BoxTelefon_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BoxPesel_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BoxID_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void TabelKlienci()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select * from Klienci", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            KlienciDataGrid.DataContext = dt;
        }

    }

}
