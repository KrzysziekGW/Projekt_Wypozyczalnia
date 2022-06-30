﻿using System;
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
using System.Windows.Threading;

namespace Wypozyczalnia_v4.Views
{
    /// <summary>
    /// Interaction logic for StronaGłównaView.xaml
    /// </summary>
    public partial class StronaGłównaView : UserControl
    {
        string connectionString = @"Data Source=DESKTOP-QR4BK4H;Initial Catalog=Wypozyczalnia;Integrated Security=True";
        public StronaGłównaView()
        {
            InitializeComponent();
            startclock();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmdZestaw = new SqlCommand("Select * from Zestaw", connection);
            connection.Open();

            DataTable dtZestaw = new DataTable();
            dtZestaw.Load(cmdZestaw.ExecuteReader());
            connection.Close();

            Lista.DataContext = dtZestaw;

        }
        private void startclock()
        {
        DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick +=tickevent;
        timer.Start();
            }

        private void tickevent(object sender, EventArgs e) 
        {
            //throw new NotImplementedException(); 
            Zegar.Text = DateTime.Now.ToString();
        }
    }
}
