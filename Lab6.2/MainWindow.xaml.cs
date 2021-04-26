using System.Windows;
using System.Windows.Controls;
using WPF.MDI;

namespace Lab6._2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string dbconnection;

        public MainWindow(string connection)
        {
            InitializeComponent();
            dbconnection = connection;
        }

        private void submenuClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindowDB.Close();
        }

        private void submenuCascade_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.MdiLayout = MdiLayout.Cascade;
        }

        private void submenuHorizontal_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.MdiLayout = MdiLayout.TileHorizontal;
        }

        private void submenuVertical_Click(object sender, RoutedEventArgs e)
        {
            MainMdiContainer.MdiLayout = MdiLayout.TileVertical;
        }

        private void submenuConference_Click(object sender, RoutedEventArgs e)
        {
            string s = (sender as MenuItem).Header.ToString();
            string nameTable = "";
            bool flag = true;
            foreach (var c in MainMdiContainer.Children)
            {
                if (c.Title == s)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                switch (s)
                {
                    case "Конференция":
                        {
                            nameTable = "Conference";
                            break;
                        }
                    case "Руководители секции":
                        {
                            nameTable = "SectionLeaders";
                            break;
                        }
                    case "Секция":
                        {
                            nameTable = "Section";
                            break;
                        }
                    case "Участник":
                        {
                            nameTable = "Member";
                            break;
                        }
                    case "Доклад":
                        {
                            nameTable = "Lecture";
                            break;
                        }
                }
                MainMdiContainer.Children.Add(new MdiChild()
                {
                    Title = (sender as MenuItem).Header.ToString(),
                    Height = 300,
                    Width = 750,
                    Content = new Table(nameTable, dbconnection)
                });
            }
        }
    }
}