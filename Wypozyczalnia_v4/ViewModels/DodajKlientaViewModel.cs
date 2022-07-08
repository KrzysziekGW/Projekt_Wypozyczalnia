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
    public partial class DodajKlientaViewModel : UserControl

    {
        string connectionString = @"Data Source=DESKTOP-QR4BK4H;Initial Catalog=Wypozyczalnia;Integrated Security=True";


        public DodajKlientaViewModel()
        {
            InitializeComponent();
            TabelKlienci();

        }
        private void ButtonDodajKlienta_Click(object sender, RoutedEventArgs e)
        {
            
            if (BoxImie.Text != "" && BoxNazwisko.Text != "" && BoxPesel.Text != "" && BoxEmail.Text != "" && BoxTelefon.Text != "")
            {
                if (BoxPesel.Text.Length != 11 || BoxTelefon.Text.Length != 9)
                {
                    MessageBox.Show("Podaj odpowiednia długośc Emaila lub Numeru telefonu!");
                }
                else

                {
                    using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
                    {
                        db.Add(new Klient { Imie = BoxImie.Text, Nazwisko = BoxNazwisko.Text, PESEL = BoxPesel.Text, Email = BoxEmail.Text, Telefon = BoxTelefon.Text });
                        db.SaveChanges();
                    }

                    TabelKlienci();
                    MessageBox.Show("Pomyślnie dodano klienta!");
                }

            }

            else
            {
                MessageBox.Show("Wypełnij puste pola!");
            }


        }


        private void ButtonUsunKlienta_Click(object sender, RoutedEventArgs e)
        {
            if (BoxID.Text != "")
            {
                using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
                {
                    string klientID = BoxID.Text;
                    int id = Int32.Parse(klientID);
                    db.Remove(new Klient { Id = id });
                    db.SaveChanges();

                }

                TabelKlienci();
                MessageBox.Show("Usunięto klienta!");
            }
            else
            {
                MessageBox.Show("Wypełnij puste pole!");
            }

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

        private void BoxEmail_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {


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

