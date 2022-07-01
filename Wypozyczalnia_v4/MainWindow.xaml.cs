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
using Wypozyczalnia_v4.ViewModels;

namespace Wypozyczalnia_v4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StronaGłównaView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new StronaGłównaViewModel();
        }

        private void DodajKlientaView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new DodajKlientaViewModel();
        }

        private void DodajZestawView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new DodajZestawViewModel();
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ZwrotZestawuView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ZwrotZestawuViewModel();
        }

        private void WypożyczView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new WypożyczViewModel();
        }

        private void ButtonZaloguj_Click(object sender, RoutedEventArgs e)
        {
            string login = "Pracownik";

            if ((BoxLogin.Text == login) && (BoxHaslo.Password == "Admin"))
            {
                MessageBox.Show("Poprawny loginni haslo!");
                DataContext = new StronaGłównaViewModel();
                PabelBoczny.Visibility = Visibility.Visible;
            }
            else if (BoxLogin.Text != "Pracownik" || BoxHaslo.Password != "Admin")
            {
                MessageBox.Show("Zły login lub hasło!");
            }
        }
    }
}
