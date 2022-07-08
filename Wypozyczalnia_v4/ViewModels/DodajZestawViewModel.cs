using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using Wypozyczalnia_v4.DbCon;
using System.Windows.Controls;

namespace Wypozyczalnia_v4.ViewModels
{
    public partial class DodajZestawViewModel : UserControl
    {
        string connectionString = @"Data Source=DESKTOP-QR4BK4H;Initial Catalog=Wypozyczalnia;Integrated Security=True";

        public DodajZestawViewModel()
        {
            InitializeComponent();

            //Wczytanie tabel Narty,buty,kaski,kijki i Zestawów
            TabelSprzęt();
            TabelZestaw();
        }


        private void ButtonDodajZestaw_Click(object sender, RoutedEventArgs e)
        {

            if (BoxNarty.Text != "" && BoxButy.Text != "" && BoxKask.Text != "" && BoxKij.Text != "")
            {
                //Dodawanie zestawu
                using (WypozyczalniaContext context = new WypozyczalniaContext(connectionString))
                {
                    ZestawC z = new ZestawC();

                    z.NartyID = Int32.Parse(BoxNarty.Text);
                    z.ButyID = Int32.Parse(BoxButy.Text);
                    z.KaskiID = Int32.Parse(BoxKask.Text);
                    z.KijkiID = Int32.Parse(BoxKij.Text);

                    context.Zestaw.Add(z);
                    context.SaveChanges();

                    int newId = z.Id;

                    context.Database.ExecuteSqlCommand("EXEC DodajCeneZestawu " + newId);
                    context.SaveChanges();

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

                //Ponowne wczytanie tabel
                TabelSprzęt();
                TabelZestaw();

                MessageBox.Show("Dodano zestaw!");

            }
            else
            {
                MessageBox.Show("Wypełnij wszystkie pola!");
            }

        }


        private void ButtonUsunZestaw_Click(object sender, RoutedEventArgs e)
        {
            if (BoxUsun.Text != "")
            {
                //Usuwanie zestawu po id
                using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
                {
                    db.Database.ExecuteSqlCommand("EXEC ZmieńStatus " + BoxUsun.Text + ",1");
                    db.Remove(new ZestawC { Id = Int32.Parse(BoxUsun.Text) });
                    db.SaveChanges();
                }

                //Ponowne wczytanie tabel
                TabelSprzęt();
                TabelZestaw();

                MessageBox.Show("Usunięto zestaw!");
            }
            else
            {
                MessageBox.Show("Wypełnij puste pole!");
            }


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

        public void TabelZestaw()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select Id,NartyID,ButyID,KaskiID,KijkiID,CenaZestawu AS Cena from Zestaw", connection);
            connection.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connection.Close();

            DataGridZestaw.DataContext = dt;
        }
        public void TabelSprzęt()
        {
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
    }
}
