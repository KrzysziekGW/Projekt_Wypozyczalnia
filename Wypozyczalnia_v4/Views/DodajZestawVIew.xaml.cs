using System.Windows;
using System.Windows.Controls;
using Wypozyczalnia_v4.DbCon;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Wypozyczalnia_v4.Views
{
    /// <summary>
    /// Logika interakcji dla klasy Page1.xaml
    /// </summary>
    public partial class DodajZestawVIew : UserControl
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Wypozyczalnia;Integrated Security=True";

        public DodajZestawVIew()
        {
            InitializeComponent();

            //Wczytanie tabel Narty,buty,kaski,kijki
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmdNarty = new SqlCommand("Select * from Narty", connection);
            SqlCommand cmdKijki = new SqlCommand("Select * from Kijki", connection);
            SqlCommand cmdKaski = new SqlCommand("Select * from Kaski", connection);
            SqlCommand cmdButy = new SqlCommand("Select * from Buty", connection);
            connection.Open();
            DataTable dtNarty = new DataTable();
            DataTable dtKijki = new DataTable();
            DataTable dtKaski = new DataTable();
            DataTable dtButy = new DataTable();
            dtNarty.Load(cmdNarty.ExecuteReader());
            dtKijki.Load(cmdKijki.ExecuteReader());
            dtKaski.Load(cmdKaski.ExecuteReader());
            dtButy.Load(cmdButy.ExecuteReader());
            connection.Close();

            DataGridNarty.DataContext = dtNarty;
            DataGridKijki.DataContext = dtKijki;
            DataGridKaski.DataContext = dtKaski;
            DataGridButy.DataContext = dtButy;
        }

        private void ButtonDodajZestaw_Click(object sender, RoutedEventArgs e)
        {
            //Dodawanie zestawu
            using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
            {
                db.Add(new ZestawC { NartyID = Int32.Parse(BoxNarty.Text), ButyID = Int32.Parse(BoxButy.Text), KaskiID = Int32.Parse(BoxKask.Text), KijkiID = Int32.Parse(BoxKij.Text) });
                db.SaveChanges();

            }

            //Zmiana statusu dla przedmiotów z utworzonego zestawu wyżej
            using (WypozyczalniaContext context = new WypozyczalniaContext(connectionString))
            {                
                var nartyStatus = context.Narty.Single(i => i.Id == Int32.Parse(BoxNarty.Text));                            
                nartyStatus.StatusID = 2;

                var butyStatus = context.Buty.Single(i => i.Id == Int32.Parse(BoxButy.Text));
                butyStatus.StatusID = 2;

                var kaskiStatus = context.Kaski.Single(i => i.Id == Int32.Parse(BoxKask.Text));
                kaskiStatus.StatusID = 2;

                var kijkiStatus = context.Kijki.Single(i => i.Id == Int32.Parse(BoxKij.Text));
                kijkiStatus.StatusID = 2;
                context.Update(nartyStatus);
                context.Update(butyStatus);
                context.Update(kaskiStatus);
                context.Update(kijkiStatus);
                context.SaveChanges();
            }

            MessageBox.Show("Dodano zestaw!");
           
        }


        private void ButtonUsunZestaw_Click(object sender, RoutedEventArgs e)
        {
            //Usuwanie zestawu po id
            using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
            {
                db.Remove(new ZestawC { Id = Int32.Parse(BoxUsun.Text) });
                db.SaveChanges();
            }

            //Ponowne wczytanie tabeli
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmdZestaw = new SqlCommand("Select * from Zestaw", connection);
            connection.Open();

            DataTable dtZestaw = new DataTable();
            dtZestaw.Load(cmdZestaw.ExecuteReader());
            connection.Close();

            DataGridZestaw.DataContext = dtZestaw;

            MessageBox.Show("Usunięto zestaw!");

        }

        private void BoxNarty_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BoxButy_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BoxKask_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BoxKij_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BoxUsun_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
