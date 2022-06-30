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
    /// Interaction logic for WypożyczView.xaml
    /// </summary>
    public partial class WypożyczView : UserControl
    {
        string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Wypozyczalnia;Integrated Security=True";

        public WypożyczView()
        {
            InitializeComponent();
        }

        private void ButtonWypożycz_Click(object sender, RoutedEventArgs e)
        {
            /*using (WypozyczalniaContext db = new WypozyczalniaContext(connectionString))
            {
                db.Database.ExecuteSqlCommand("EXEC Wypożycz "+ Int32.Parse(BoxKlientID.Text) + "," + Int32.Parse(BoxZestawID.Text)+ "," + DataTime.Now + "null,2");
                db.SaveChanges();
            
            }*/
        }
    }
}
