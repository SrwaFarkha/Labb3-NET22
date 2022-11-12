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
using Labb3_NET22.FileManager;
using Labb3_NET22.Views;

namespace Labb3_NET22
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FileManager.FileManager _fileManager;
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            _fileManager = new FileManager.FileManager();

            _ =InitializeQuizzes();
            InitializeComponent();
        }

        public async Task InitializeQuizzes()
        {
            await _fileManager.LoadQuizzesAsync();
        }

        private void GeneralView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new GeneralView();
            WelcomeText.Visibility = Visibility.Hidden;
        }

        private void EditView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new EditView();
            WelcomeText.Visibility = Visibility.Hidden;
        }

        private void AddView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AddView();
            WelcomeText.Visibility = Visibility.Hidden;
        }
    }
}
