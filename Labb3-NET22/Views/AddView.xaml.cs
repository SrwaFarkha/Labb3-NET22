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
using Labb3_NET22.FileManager;

namespace Labb3_NET22.Views
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddView : UserControl
    {
        private FileManager.FileManager _fileManager;
        public string? ImageName { get; set; }
        public QuizModel? NewQuiz { get; set; } = new QuizModel();

        public int[] CorrectAnswerArray { get; set; } = { 1, 2, 3 };
        public string[] CategoriesArray { get; set; } = { "Addition", "Subtraction", "General" };
        public dynamic? SelectedItem { get; set; }
        public int QuestionIdCounter { get; set; } = 1;
        public int QuizIdCounter { get; set; }

        public AddView()
        {
            _fileManager = new FileManager.FileManager();
            InitializeComponent();
            InitializeSettings();
        }

        private void AddQuestionToListButton_OnClick(object sender, RoutedEventArgs e)
        {
            var category = GetQuestionCategory();

            string imagePath = QuestionImage.Source.ToString();
            string fileName = imagePath.Substring(imagePath.LastIndexOf('/') + 1);

            var questions = new List<Question>()
            {
                new Question()
                {
                    Id = QuestionIdCounter,
                    Statement = QuestionStatementTextBoxInput.Text,
                    Answers = new[] { QuestionAnswer1TextBoxInput.Text, QuestionAnswer2TextBoxInput.Text, QuestionAnswer3TextBoxInput.Text },
                    CorrectAnswer = Convert.ToInt32(QuestionCorrectAnswerComboBox.Text),
                    ImagePath = fileName,
                    Category = category
                }
            };

            NewQuiz?.Questions.Add(questions.First());
            LvQuiz.ItemsSource = NewQuiz?.Questions.Select(x => new { x.Id, x.Statement, Answer1 = x.Answers[0], Answer2 = x.Answers[1], Answer3 = x.Answers[2], x.CorrectAnswer, x.Category, x.ImagePath }).ToList();
            QuestionIdCounter++;
            MessageBox.Show("Question added to list!");
            ClearQuestionProperties();

        }


        private async void QuestionChooseImageButton_OnClick(object sender, RoutedEventArgs e)
        {
            await ChoosePicture();
        }

        private async Task ChoosePicture()
        {

            ImageName = await _fileManager.UploadImageAsync();

            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string appPath = appDirectory.Substring(0, appDirectory.IndexOf("\\bin")) + @"\Images\";

            QuestionImage.Source = new BitmapImage(new Uri(appPath + ImageName));

            if (IsInputFieldNotNull())
            {
                AddQuestionToListButton.IsEnabled = true;
            }
        }

        private bool IsInputFieldNotNull()
        {
            if (QuestionStatementTextBoxInput.Text != "" &&
                QuestionAnswer1TextBoxInput.Text != "" &&
                QuestionAnswer2TextBoxInput.Text != "" &&
                QuestionAnswer3TextBoxInput.Text != "" &&
                QuestionCorrectAnswerComboBox.SelectedItem != null &&
                QuestionCategoryComboBox.SelectedItem != null &&
                QuestionImage.Source != null)
            {
                return true;
            }

            return false;

        }


        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var id = int.Parse(QuestionIdTextBox.Text);

            var question = NewQuiz?.Questions.Find(x => x.Id == id);
            if (question != null)
            {
                NewQuiz?.Questions.Remove(question);
                MessageBox.Show("Successfully removed question from list!");
                RemoveButton.IsEnabled = false;
                UpdateQuestionList();
                ClearQuestionProperties();
            }

            if (NewQuiz?.Questions.Count == 0)
            {
                SaveButton.IsEnabled = false;
            }
        }

        private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            NewQuiz!.Id = QuizIdCounter;
            NewQuiz.Title = QuizTitleInput.Text;
            if (NewQuiz != null)
            {
                await _fileManager.SaveQuizToFileAsync(NewQuiz);
                MessageBox.Show("Quiz successfully created!");

                QuizIdCounter++;
                QuizIdInput.Text = QuizIdCounter.ToString();
                LvQuiz.ItemsSource = null;
                NewQuiz = new QuizModel();
                ClearQuestionProperties();
            }
        }

        private void UpdateQuestionList()
        {
            LvQuiz.ItemsSource = NewQuiz?.Questions.Select(x => new { x.Id, x.Statement, Answer1 = x.Answers[0], Answer2 = x.Answers[1], Answer3 = x.Answers[2], x.CorrectAnswer, x.Category, x.ImagePath }).ToList();
        }

        private void MouseClickOnQuestion_OnHandler(object sender, MouseButtonEventArgs e)
        {
            DisplaySelectedQuestionProperties();
            RemoveButton.IsEnabled = true;
        }

        private void DisplaySelectedQuestionProperties()
        {
            SelectedItem = LvQuiz?.SelectedItems[0];

            QuestionIdTextBox.Text = SelectedItem?.Id.ToString();
            QuestionStatementTextBoxInput.Text = SelectedItem?.Statement;
            QuestionAnswer1TextBoxInput.Text = SelectedItem?.Answer1;
            QuestionAnswer2TextBoxInput.Text = SelectedItem?.Answer2;
            QuestionAnswer3TextBoxInput.Text = SelectedItem?.Answer3;
            QuestionCorrectAnswerComboBox.Text = SelectedItem?.CorrectAnswer.ToString();

            QuestionCategoryComboBox.ItemsSource = CategoriesArray;
            QuestionCategoryComboBox.SelectedItem = SelectedItem?.Category.ToString();

            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string appPath = appDirectory.Substring(0, appDirectory.IndexOf("\\bin")) + @"\Images\";

            QuestionImage.Source = new BitmapImage(new Uri(appPath + SelectedItem?.ImagePath));
        }

        private void ClearQuestionPropertiesButton_OnClick(object sender, RoutedEventArgs e)
        {
            ClearQuestionProperties();
        }

        private void ClearQuestionProperties()
        {
            if (NewQuiz?.Questions.Count == 0)
            {
                QuestionIdCounter = 1;
            }

            QuestionIdTextBox.Text = QuestionIdCounter.ToString();

            QuestionStatementTextBoxInput.Text = "";
            QuestionAnswer1TextBoxInput.Text = "";
            QuestionAnswer2TextBoxInput.Text = "";
            QuestionAnswer3TextBoxInput.Text = "";
            QuestionCorrectAnswerComboBox.SelectedItem = null;
            QuestionCategoryComboBox.SelectedItem = null;
            QuestionImage.Source = null;

            if (QuizTitleInput.Text != "" && NewQuiz?.Questions.Count != 0)
            {
                SaveButton.IsEnabled = true;
            }

            AddQuestionToListButton.IsEnabled = false;
        }

        private async void InitializeSettings()
        {
            var quizzes = await _fileManager.LoadQuizzesAsync();
            QuizIdCounter = quizzes.Count + 1;
            QuizIdInput.Text = QuizIdCounter.ToString();

            QuizIdInput.IsEnabled = false;
            SaveButton.IsEnabled = false;
            RemoveButton.IsEnabled = false;
            QuestionIdTextBox.Text = QuestionIdCounter.ToString();
            QuestionIdTextBox.IsEnabled = false;
            AddQuestionToListButton.IsEnabled = false;
        }

        private Categories GetQuestionCategory()
        {
            return QuestionCategoryComboBox.Text switch
            {
                "Addition" => Categories.Addition,
                "Subtraction" => Categories.Subtraction,
                _ => Categories.General
            };
        }

        private void QuizTitleInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (QuizTitleInput.Text != "" && NewQuiz?.Questions.Count != 0)
            {
                SaveButton.IsEnabled = true;
            }
            else if (QuizTitleInput.Text == "")
            {
                SaveButton.IsEnabled = false;
            }
        }
    }
}
