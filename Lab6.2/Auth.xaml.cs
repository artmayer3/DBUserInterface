using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace Lab6._2
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void OpenMainWindow(string connectionString)
        {
            MainWindow mainWindow = new MainWindow(connectionString);
            mainWindow.Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            labelError.Content = string.Empty;
            var connectionString = $"Server=127.0.0.1;Port=5432;User Id={loginBox.Text};Password={passwordBox.Password};Database=db_Conference;";
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
            }
            catch (NpgsqlException)
            {
                labelError.Content = "Неверное имя пользователя или пароль";
                return;
            }
            OpenMainWindow(connectionString);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}