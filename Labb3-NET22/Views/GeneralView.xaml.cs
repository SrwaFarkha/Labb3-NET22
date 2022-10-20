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
using Labb3_NET22.DataModels;

namespace Labb3_NET22.Views
{
    /// <summary>
    /// Interaction logic for GeneralView.xaml
    /// </summary>
    public partial class GeneralView : UserControl
    {
        private DataService.DataService _dataService = new DataService.DataService();

        public List<Quiz> Quizzes { get; set; }

        public GeneralView()
        {
            Quizzes = _dataService.GetAllQuizzes();
            InitializeComponent();
            var catagories = new string[] { "All", "Movies", "Books", "General" };
            SelectCategoriesCombobox.ItemsSource = catagories;

            this.DataContext = this;

        }

    }

}
