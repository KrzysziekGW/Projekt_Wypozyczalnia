using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Wypozyczalnia_v4.ViewModels
{
    public partial class StronaGłównaViewModel : UserControl
    {
        string connectionString = @"Data Source=DESKTOP-QR4BK4H;Initial Catalog=Wypozyczalnia;Integrated Security=True";
        public StronaGłównaViewModel()
        {
            InitializeComponent();
            startclock();
            TabelDzisiejszeWypozyczenia();
            TabelDzisiejszeZwroty();
            
        }
        private void startclock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            //throw new NotImplementedException(); 
            Zegar.Text = DateTime.Now.ToString();
        }

        public void TabelDzisiejszeWypozyczenia()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmdZestaw = new SqlCommand("Select * from dbo.v_DzisiejszeWypożyczenia", connection);
            connection.Open();

            DataTable dtZestaw = new DataTable();
            dtZestaw.Load(cmdZestaw.ExecuteReader());
            connection.Close();

            DziesiejszeWypożyczeniaDataGrid.DataContext = dtZestaw;
        }
        public void TabelDzisiejszeZwroty()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmdZestaw = new SqlCommand("Select * from dbo.v_DzisiejszeZwroty", connection);
            connection.Open();

            DataTable dtZestaw = new DataTable();
            dtZestaw.Load(cmdZestaw.ExecuteReader());
            connection.Close();

            DziesiejszeZwrotyDataGrid.DataContext = dtZestaw;
        }
    }
}
