using Labb3_NET22.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;


namespace Labb3_NET22.Views
{
    /// <summary>
    /// Interaction logic for EditView.xaml
    /// </summary>
    public partial class EditView : UserControl
    {
        private FileManager.FileManager _fileManager;

        public Quiz? SelectedQuiz { get; set; }
        public dynamic? SelectedItem { get; set; }
        public string[] CategoriesArray { get; set; } = { "Addition", "Subtraction", "General" };
        public int[] CorrectAnswerArray { get; set; } = { 1, 2, 3 };
        public string? ImageName { get; set; }

        public EditView()
        {
            _fileManager = new FileManager.FileManager();
            _ = InitializeQuizzes();

            InitializeComponent();
            DisableItems();
            this.DataContext = this;
        }

        private async Task InitializeQuizzes()
        {
            var quizzes = await _fileManager.LoadQuizzesAsync();
            SelectQuizComboBox.ItemsSource = quizzes;
            SelectQuizComboBox.DisplayMemberPath = "Title";
        }

        private void SelectQuizComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedQuiz = (Quiz)SelectQuizComboBox.SelectedItem;

            if (SelectedQuiz != null)
            {
                LvQuiz.ItemsSource = SelectedQuiz.Questions.Select(x => new { x.Id, x.Statement, Answer1 = x.Answers[0], Answer2 = x.Answers[1], Answer3 = x.Answers[2], x.CorrectAnswer, x.Category, x.ImagePath }).ToList();
            }
        }

        private void MouseDoubleClick_OnSelectedQuestion(object sender, MouseButtonEventArgs e)
        {
            EnableItems();
            DisplaySelectedQuestionProperties();
        }

        private void DisplaySelectedQuestionProperties()
        {
            SelectedItem = LvQuiz?.SelectedItems[0];

            SelectedQuestionIdTextBox.Text = SelectedItem?.Id.ToString();
            SelectedQuestionStatementTextBox.Text = SelectedItem?.Statement;
            SelectedQuestionAnswer1TextBox.Text = SelectedItem?.Answer1;
            SelectedQuestionAnswer2TextBox.Text = SelectedItem?.Answer2;
            SelectedQuestionAnswer3TextBox.Text = SelectedItem?.Answer3;
            SelectedQuestionCorrectAnswerComboBox.Text = SelectedItem?.CorrectAnswer.ToString();

            SelectedQuestionCategoryComboBox.ItemsSource = CategoriesArray;
            SelectedQuestionCategoryComboBox.SelectedItem = SelectedItem?.Category.ToString();

            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string appPath = appDirectory.Substring(0, appDirectory.IndexOf("\\bin")) + @"\Images\";

            SelectedQuestionImage.Source = new BitmapImage(new Uri(appPath + SelectedItem?.ImagePath));
        }

        private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var category = new Categories();

            switch (SelectedQuestionCategoryComboBox.Text)
            {
                case "Addition":
                    category = Categories.Addition;
                    break;
                case "Subtraction":
                    category = Categories.Subtraction;
                    break;
                case "General":
                    category = Categories.General;
                    break;
            }

            if (SelectedItem != null)   
            {
                var questions = new List<Question>()
                {
                    new Question()
                    {
                        Id = int.Parse(SelectedQuestionIdTextBox.Text),
                        Statement = SelectedQuestionStatementTextBox.Text,
                        Answers = new[] { SelectedQuestionAnswer1TextBox.Text, SelectedQuestionAnswer2TextBox.Text, SelectedQuestionAnswer3TextBox.Text },
                        CorrectAnswer = Convert.ToInt32(SelectedQuestionCorrectAnswerComboBox.Text),

                        //ImagePath = SelectedQuestionImage.Source.ToString(),
                        ImagePath = ImageName ?? SelectedItem!.ImagePath,
                        Category = category
                    }
                };

                var quiz = new QuizModel
                {
                    Id = SelectedQuiz!.Id,
                    Questions = questions,
                    Title = SelectedQuiz.Title,
                };

                await _fileManager.SaveToFileAsync(quiz);
            } 

            MessageBox.Show("Saved!");
            await UpdateQuestionProperties();
            
        }

        private async void SelectedQuestionChooseImageButton_OnClick(object sender, RoutedEventArgs e)
        {
            await ChoosePicture();
        }

        private async Task ChoosePicture()
        {
            ImageName = await _fileManager.UploadImageAsync();

            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string appPath = appDirectory.Substring(0, appDirectory.IndexOf("\\bin")) + @"\Images\";

            SelectedQuestionImage.Source = new BitmapImage(new Uri( appPath + ImageName));
        }

        private async Task UpdateQuestionProperties()
        {
            SelectedQuiz = (Quiz)SelectQuizComboBox.SelectedItem;
            var quiz = await _fileManager.GetSelectedQuizAsync(SelectedQuiz);

            LvQuiz.ItemsSource = quiz.Questions.Select(x => new { x.Id, x.Statement, Answer1 = x.Answers[0], Answer2 = x.Answers[1], Answer3 = x.Answers[2], x.CorrectAnswer, x.Category, x.ImagePath }).ToList();
        }

        private void DisableItems()
        {
            SaveButton.IsEnabled = false;
            SelectedQuestionIdTextBox.IsEnabled = false;
            SelectedQuestionStatementTextBox.IsEnabled = false;
            SelectedQuestionAnswer1TextBox.IsEnabled = false;
            SelectedQuestionAnswer2TextBox.IsEnabled = false;
            SelectedQuestionAnswer3TextBox.IsEnabled = false;
            SelectedQuestionCorrectAnswerComboBox.IsEnabled = false;
            SelectedQuestionCategoryComboBox.IsEnabled = false;
            SelectedQuestionChooseImageButton.IsEnabled = false;
        }

        private void EnableItems()
        {
            SaveButton.IsEnabled = true;

            SelectedQuestionStatementTextBox.IsEnabled = true;
            SelectedQuestionAnswer1TextBox.IsEnabled = true;
            SelectedQuestionAnswer2TextBox.IsEnabled = true;
            SelectedQuestionAnswer3TextBox.IsEnabled = true;
            SelectedQuestionCorrectAnswerComboBox.IsEnabled = true;
            SelectedQuestionCategoryComboBox.IsEnabled = true;
            SelectedQuestionChooseImageButton.IsEnabled = true;

            SelectedQuestionCategoryComboBox.ItemsSource = CategoriesArray;
            SelectedQuestionCategoryComboBox.SelectedItem = SelectedItem?.Category.ToString();
        }

    }

}
