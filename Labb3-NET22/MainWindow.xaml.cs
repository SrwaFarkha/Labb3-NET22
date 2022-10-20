using Labb3_NET22.ViewModels;
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

namespace Labb3_NET22
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

        private void GeneralView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new GeneralViewModel();
            WelcomeText.Visibility = Visibility.Hidden;
        }

        private void EditView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new EditViewModel();
            WelcomeText.Visibility = Visibility.Hidden;
        }

        private void AddView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AddViewModel();
            WelcomeText.Visibility = Visibility.Hidden;
        }
    }
}
