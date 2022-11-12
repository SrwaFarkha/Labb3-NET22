using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Labb3_NET22.DataModels;
using Labb3_NET22.Extensions;
using Microsoft.Win32;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Labb3_NET22.FileManager
{
    public class FileManager
    {
        private readonly DataService.DataService _dataService;
        private readonly string _directoryPath;
        private readonly string _pathToFile;

        public FileManager()
        {
            _dataService = new DataService.DataService();
            _directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "SrwaLabb3Quizzes");
            _pathToFile = Path.Combine(_directoryPath, "SroaQuizzes.json");
        }

        public async Task<List<Quiz>> LoadQuizzesAsync()
        {
            if (!File.Exists(_pathToFile))
            {
                return await CreateFileAsync();
            }

            var quizzesObject = await ReadFileAsync();

            var quizzes = new List<Quiz>();
            if (quizzesObject != null)
            {
                foreach (var quiz in quizzesObject)
                {
                    quizzes.Add(new Quiz(quiz.Id, quiz.Questions, quiz.Title));
                }
            }

            return quizzes;
        }


        public async Task SaveToFileAsync(QuizModel quizToSave)
        {
            var quizzesObject = await ReadFileAsync();

            var quiz = quizzesObject!.First(x => x.Id == quizToSave.Id);
            quiz.Title = quizToSave.Title;

            await quiz.Questions.Replace(x => x.Id == quizToSave.Questions.Select(x => x.Id).First(),
                quizToSave.Questions.First());
            await quizzesObject!.Replace(x => x.Id == quiz.Id, quiz);

            await using var stream = File.Open(_pathToFile, FileMode.Create);
            await JsonSerializer.SerializeAsync(stream, quizzesObject);
            await stream.DisposeAsync();
        }

        public async Task SaveQuizToFileAsync(QuizModel quizToSave)
        {
            var quizzesObject = await ReadFileAsync();

            quizzesObject?.Add(quizToSave);

            await using var stream = File.Open(_pathToFile, FileMode.Create);
            await JsonSerializer.SerializeAsync(stream, quizzesObject);
            await stream.DisposeAsync();
        }

        public async Task<List<Quiz>> CreateFileAsync()
        {
            Directory.CreateDirectory(_directoryPath);

            var defaultData = await LoadDefaultQuizzesAsync();
            var jsonObject = JsonConvert.SerializeObject(defaultData, Newtonsoft.Json.Formatting.Indented);

            await using var createFile = File.Open(_pathToFile, FileMode.Create);
            createFile.Close();
            using var writeToFile = File.WriteAllTextAsync(_pathToFile, jsonObject);

            return defaultData;
        }

        public async Task<List<Quiz>> LoadDefaultQuizzesAsync()
        {
            var quizzes = await _dataService.GetDefaultQuizzes();
            return quizzes;
        }

        public async Task<QuizModel> GetSelectedQuizAsync(Quiz selectedQuiz)
        {
            var quizzesObject = await ReadFileAsync();

            var quiz = quizzesObject?.Where(x => x.Id == selectedQuiz.Id).First();

            return quiz ?? new QuizModel();
        }

        public async Task<List<QuizModel>> ReadFileAsync()
        {
            await using var fileStream = new FileStream(_pathToFile, FileMode.Open, FileAccess.Read);
            var quizzesObject = await JsonSerializer.DeserializeAsync<List<QuizModel>>(fileStream);
            if (quizzesObject != null)
            {
                return quizzesObject;
            }
            return new List<QuizModel>();
        }

        public async Task<string> UploadImageAsync()
        {

            var openFileDialog = new OpenFileDialog() { Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*" };
            var fileName = "";

            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.SafeFileName;

                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string appPath = appDirectory.Substring(0, appDirectory.IndexOf("\\bin")) + @"\Images\";

                if (!File.Exists(appPath +fileName))
                {
                    File.Copy(openFileDialog.FileName, appPath + fileName);
                }
                if (File.Exists(appPath + fileName))
                {
                    return await Task.FromResult(fileName);
                }

            }
            return await Task.FromResult(fileName);
        }
    }
}

