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
        string connectionString = @"Data Source=DESKTOP-QR4BK4H;Initial Catalog=Wypozyczalnia;Integrated Security=True";
        public ZwrotZestawuView()
        {
            InitializeComponent();
        }

        private void ButtonZwrotZestawu_Click(object sender, RoutedEventArgs e)
        {
            using (WypozyczalniaContext context = new WypozyczalniaContext(connectionString))
            {

               /* var idZestawu = context.Zestaw.Select(i => i.Id == Int32.Parse(BoxZwrot.Text));
                
                var idNart = 







                var nartyStatus = context.Narty.Single(i => i.Id == idZestawu);
                nartyStatus.StatusID = 1;

                var butyStatus = context.Buty.Single(i => i.Id == Int32.Parse(BoxButy.Text));
                butyStatus.StatusID = 1;

                var kaskiStatus = context.Kaski.Single(i => i.Id == Int32.Parse(BoxKask.Text));
                kaskiStatus.StatusID = 1;

                var kijkiStatus = context.Kijki.Single(i => i.Id == Int32.Parse(BoxKij.Text));
                kijkiStatus.StatusID = 1;
                context.Update(nartyStatus);
                context.Update(butyStatus);
                context.Update(kaskiStatus);
                context.Update(kijkiStatus);
                context.SaveChanges();*/
            }
        }
    }
}
