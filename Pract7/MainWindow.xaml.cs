using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Pract7.MainWindow;

namespace Pract7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User CurrentUser = new User();
        private string folder = "Users";
        public MainWindow()
        {
            InitializeComponent();
            panel1.DataContext = CurrentUser;
            panel3.DataContext = CurrentUser;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            RandomId();
        }
        private void RandomId()
        {
            Random random = new Random();
            bool idExists;
            string id;
            do
            {
                id = random.Next(1000, 100000).ToString();
                string filePath = System.IO.Path.Combine(folder, $"D_{id}.json");
                idExists = File.Exists(filePath);
            }while(idExists);
            CurrentUser.Id = id;
        }
        private void Registr(object sender, RoutedEventArgs e)
        {
            if (CurrentUser.Password != TextBoxPassword.Text)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            if (string.IsNullOrWhiteSpace(CurrentUser.Name) || string.IsNullOrWhiteSpace(CurrentUser.LastName) || string.IsNullOrWhiteSpace(CurrentUser.MiddleName) || string.IsNullOrWhiteSpace(CurrentUser.Specialisation) || string.IsNullOrWhiteSpace(CurrentUser.Password))
            {
                MessageBox.Show("Заполните все поля для регистрации!");
                return;
            }
            var user = new
            {
                Id = CurrentUser.Id,
                Name = CurrentUser.Name,
                LastName = CurrentUser.LastName,
                MiddleName = CurrentUser.MiddleName,
                Specialisation = CurrentUser.Specialisation,
                Password = CurrentUser.Password
            };
            string json = JsonSerializer.Serialize(user);
            string filePath = System.IO.Path.Combine(folder, $"D_{CurrentUser.Id}.json");
            IdTextBox.Text = CurrentUser.Id;
            //string info = $"\"Name\": \"{CurrentUser.Name}\",\r\n\"LastName\": \"{CurrentUser.LastName}\",\r\n\"MiddleName\": \"{CurrentUser.MiddleName}\",\r\n\"Specialisation\": \"{CurrentUser.Specialisation}\",\r\n\"Password\": \"{CurrentUser.Password}\"";
            File.WriteAllText(filePath, json);
            MessageBox.Show("Вы зарегистрированы!");
        }

        private void Input(object sender, RoutedEventArgs e)
        {
            string id = TextBoxId.Text;
            string password = PasswordTextBox.Text;
            if(string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все поля для входа!");
                return;
            }
            string filePath = System.IO.Path.Combine(folder, $"D_{CurrentUser.Id}.json");
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var user = JsonSerializer.Deserialize<User>(json);
                if (user.Password == password)
                {
                    CurrentUser.Id = user.Id;
                    CurrentUser.Name = user.Name;
                    CurrentUser.LastName = user.LastName;
                    CurrentUser.MiddleName = user.MiddleName;
                    CurrentUser.Specialisation = user.Specialisation;
                    CurrentUser.Password = user.Password;
                    MessageBox.Show("Вход выполнен успешно");
                }
                else
                {
                    MessageBox.Show("Неверный пароль");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Пользователь не зарегистрирован");
                return;
            }
        }
    }
}