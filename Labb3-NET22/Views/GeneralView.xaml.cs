using Labb3_NET22.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

namespace Labb3_NET22.Views
{
    /// <summary>
    /// Interaction logic for GeneralView.xaml
    /// </summary>
    public partial class GeneralView : UserControl
    {
        private FileManager.FileManager _fileManager;
        public Quiz? SelectedQuiz { get; set; }
        public string? SelectedCategory { get; set; }

        public Question? CurrentQuestion { get; set; }
        public int CorrectAnswers { get; set; }
        public int CurrentCorrectAnswer { get; set; }
        public int CurrentWrongAnswer { get; set; }
        public int QuestionCounter { get; set; }
        public string[] Categories { get; set; } =  { "All", "Addition", "Subtraction", "General" };

        public ICommand PlayCommand => new RelayCommand(StartGame);
        public ICommand ButtonPlayAgainCommand => new RelayCommand(StartGame);
        public ICommand Answer1Command => new RelayCommand(() => CheckAnswer(1));
        public ICommand Answer2Command => new RelayCommand(() => CheckAnswer(2));
        public ICommand Answer3Command => new RelayCommand(() => CheckAnswer(3));

        public GeneralView()
        {
            _fileManager = new FileManager.FileManager();
            _ = InitializeQuizzes();
            InitializeComponent();

            StackPanelQuizCard.Visibility = Visibility.Hidden;
            ButtonPlayAgain.Visibility = Visibility.Hidden;
            this.DataContext = this;

            IsSelectBoxSelected();
        }

        private async Task InitializeQuizzes()
        {
            var quizzes = await _fileManager.LoadQuizzesAsync();
            SelectQuizComboBox.ItemsSource = quizzes;
            SelectQuizComboBox.DisplayMemberPath = "Title";
        }

        private void SelectQuizComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsSelectBoxSelected();

            SelectedQuiz = (Quiz)SelectQuizComboBox.SelectedItem;
        }

        private void SelectCategoryComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsSelectBoxSelected();

            SelectedCategory = SelectCategoryComboBox.SelectedItem.ToString() ?? "";
        }

        private void StartGame()
        {
            HideAndShowItems();
            ResetQuestions();
            GetNextQuestion();
        }

        private void GetNextQuestion()
        {
            CurrentQuestion = SelectedQuiz?.GetRandomQuestion(SelectedCategory);

            if (CurrentQuestion != null)
            {
                string questionCountString = SelectedCategory == "All" ?
                    $"Question {QuestionCounter} of {SelectedQuiz?.Questions.Count()}"
                    : $"Question {QuestionCounter} of {SelectedQuiz!.Questions.Count(x => x.Category.ToString() == SelectedCategory)}";


                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string appPath = appDirectory.Substring(0, appDirectory.IndexOf("\\bin", StringComparison.Ordinal)) + @"\Images\";

                ImageBox.Source = new BitmapImage(new Uri(appPath + CurrentQuestion?.ImagePath));

                StatementTextBlock.Text = CurrentQuestion!.Statement;
                AnswerInput1.Content = CurrentQuestion.Answers[0];
                AnswerInput2.Content = CurrentQuestion.Answers[1];
                AnswerInput3.Content = CurrentQuestion.Answers[2];
                QuestionCountLabel.Content = questionCountString;
                CurrentCorrectAnswerTextBlock.Text = $"{CurrentCorrectAnswer} - Correct";
                CurrentWrongAnswerTextBlock.Text = $"{CurrentWrongAnswer} - Incorrect";
                CurrentQuestion.IsAsked = true;
            }
            else
            {
                ShowResult();
            }
        }

        private void CheckAnswer(int indexOfAnswer)
        {
            if (CurrentQuestion == null)
                return;

            if (CurrentQuestion.CorrectAnswer == indexOfAnswer)
            {
                CurrentCorrectAnswer++;
                CorrectAnswers++;
            }
            else
            {
                CurrentWrongAnswer++;
            }

            QuestionCounter++;
            GetNextQuestion();
        }

        private void ResetQuestions()
        {
            CurrentCorrectAnswer = 0;
            CurrentWrongAnswer = 0;
            QuestionCounter = 1;
            CorrectAnswers = 0;

            if (SelectedQuiz?.Questions != null)
            {
                foreach (var question in SelectedQuiz.Questions)
                {
                    question.IsAsked = false;
                }
            }
        }

        private void ShowResult()
        {
            string correctAnswerString = SelectedCategory == "All" ?
                  $"Correct answers: {CorrectAnswers} of {SelectedQuiz?.Questions.Count()}"  
                : $"Correct answers: {CorrectAnswers} of {SelectedQuiz!.Questions.Count(x => x.Category.ToString() == SelectedCategory)}";
           
            HideAllItems();
            ResultLabel.Visibility = Visibility.Visible;
            CorrectAnswerLabel.Visibility = Visibility.Visible;
            ButtonPlayAgain.Visibility = Visibility.Visible;
            ResultLabel.Content = "End of quiz!";
            CorrectAnswerLabel.Content = correctAnswerString;
        }

        private void HideAllItems()
        {
            SelectQuizComboBox.Visibility = Visibility.Hidden;
            SelectCategoryComboBox.Visibility = Visibility.Hidden;
            StartButton.Visibility = Visibility.Hidden;
            StackPanelQuizCard.Visibility = Visibility.Hidden;
        }

        private void IsSelectBoxSelected()
        {
            if (SelectQuizComboBox.SelectedItem == null && SelectCategoryComboBox.SelectedItem == null)
            {
                StartButton.IsEnabled = false;
            } 
            else if (SelectQuizComboBox.SelectedItem != null && SelectCategoryComboBox.SelectedItem != null)
            {
                StartButton.IsEnabled = true;
            }
        }

        private void HideAndShowItems()
        {
            SelectQuizComboBox.Visibility = Visibility.Hidden;
            SelectCategoryComboBox.Visibility = Visibility.Hidden;
            StartButton.Visibility = Visibility.Hidden;
            ResultLabel.Visibility = Visibility.Hidden;
            ButtonPlayAgain.Visibility = Visibility.Hidden;
            CorrectAnswerLabel.Visibility = Visibility.Hidden;
            StackPanelQuizCard.Visibility = Visibility.Visible;
        }
    }
}
