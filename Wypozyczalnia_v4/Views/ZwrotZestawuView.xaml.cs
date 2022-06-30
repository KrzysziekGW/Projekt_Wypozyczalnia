using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using Wypozyczalnia_v4.DbCon;

namespace Wypozyczalnia_v4.Views
{
    /// <summary>
    /// Interaction logic for ZwrotZestawuView.xaml
    /// </summary>
    public partial class ZwrotZestawuView : UserControl
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Wypozyczalnia;Integrated Security=True";
        public ZwrotZestawuView()
        {
            InitializeComponent();
        }

        private void ButtonZwrotZestawu_Click(object sender, RoutedEventArgs e)
        {
            using (WypozyczalniaContext context = new WypozyczalniaContext(connectionString))
            {
                context.Database.ExecuteSqlCommand("EXEC ZmieńStatus " + BoxZwrot.Text + ",1");
                context.SaveChanges();
            }
        }
    }
}
