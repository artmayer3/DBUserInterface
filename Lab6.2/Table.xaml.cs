using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Lab6._2
{
    /// <summary>
    /// Логика взаимодействия для Table.xaml
    /// </summary>
    public partial class Table : UserControl
    {
        private string tableName;
        private string dbConnectionString;
        private NpgsqlConnection dbConn;
        private NpgsqlCommand sqlCmd;
        private NpgsqlDataAdapter adapter;
        private DataTable dTable;
        private string sqlQuery;

        public Table(string name, string dbConnection)
        {
            InitializeComponent();
            tableName = name;
            dbConnectionString = dbConnection;
            dbConn = new NpgsqlConnection();
            sqlCmd = new NpgsqlCommand();
            try
            {
                dbConn = new NpgsqlConnection(dbConnectionString);
                dbConn.Open();
                sqlCmd.Connection = dbConn;
                CreateTable();
                dbConn.Close();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void CreateTable()
        {
            dTable = new DataTable();

            if (dbConn.State != ConnectionState.Open)
            {
                MessageBox.Show("Нет соединения с БД");
                return;
            }

            try
            {
                switch (tableName)
                {
                    case "Conference":
                        sqlQuery = "SELECT * FROM SELECT_ALL_FROM_CONFERENCE()";
                        break;

                    case "SectionLeaders":
                        sqlQuery = "SELECT * FROM SELECT_ALL_FROM_SECTION_LEADER()";
                        break;

                    case "Section":
                        sqlQuery = "SELECT * FROM SELECT_ALL_FROM_SECTION()";
                        break;

                    case "Member":
                        sqlQuery = "SELECT * FROM SELECT_ALL_FROM_MEMBER()";
                        break;

                    case "Lecture":
                        sqlQuery = "SELECT * FROM SELECT_ALL_FROM_LECTURE()";
                        break;
                }
                dataGrid.DataContext = dTable.DefaultView;
                adapter = new NpgsqlDataAdapter(sqlQuery, dbConn);
                adapter.Fill(dTable);
                List<string> list = new List<string>();
                foreach (var c in dTable.Columns)
                {
                    if (!c.ToString().Contains("ID"))
                        list.Add(c.ToString());
                }
                comboBox1.ItemsSource = list;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void insTable_Click(object sender, RoutedEventArgs e)
        {
            //if (dbConn.State != ConnectionState.Open)
            //{
            //    MessageBox.Show("Нет соединения с БД");
            //    return;
            //}
            insertEdit(0, tableName);
        }

        private void editTable_Click(object sender, RoutedEventArgs e)
        {
            insertEdit(1, tableName);
        }

        private void delTable_Click(object sender, RoutedEventArgs e)
        {
            if (dTable.Rows.Count == 0 || dataGrid.SelectedIndex == -1)
            {
                MessageBox.Show($"Нет данных для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            dbConn.Open();
            try
            {
                sqlCmd = new NpgsqlCommand($"CALL DELETE_FROM_TABLE('{tableName}', {dTable.Rows[dataGrid.SelectedIndex][0]})", dbConn);
                sqlCmd.ExecuteNonQuery();
                dTable.Clear();
                adapter = new NpgsqlDataAdapter(sqlQuery, dbConn);
                adapter.Fill(dTable);
                dataGrid.DataContext = dTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dbConn.Close();
            }
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            dbConn.Open();
            dTable.Clear();
            adapter = new NpgsqlDataAdapter(sqlQuery, dbConn);
            adapter.Fill(dTable);
            dataGrid.DataContext = dTable;
            dbConn.Close();
        }

        private void insertEdit(int param, string name)
        {
            if (dTable.Rows.Count != 0 && dataGrid.SelectedIndex != -1)
            {
            }
            else
            {
                if (param == 1)
                {
                    MessageBox.Show($"Нет данных для редактирования", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            switch (name)
            {
                case "Conference":
                    {
                        InsEdit insEdit;
                        if (param == 0)
                        {
                            insEdit = new InsEdit("Добавление", name, dbConnectionString);
                        }
                        else
                        {
                            insEdit = new InsEdit("Изменение", name, dTable.Rows[dataGrid.SelectedIndex], dbConnectionString);
                        }
                        insEdit.Tag = param;
                        insEdit.ShowDialog();
                        break;
                    }
                case "SectionLeaders":
                    {
                        InsEdit insEdit;
                        if (param == 0)
                        {
                            insEdit = new InsEdit("Добавление", name, dbConnectionString);
                        }
                        else
                        {
                            insEdit = new InsEdit("Изменение", name, dTable.Rows[dataGrid.SelectedIndex], dbConnectionString);
                        }
                        insEdit.Tag = param;
                        insEdit.ShowDialog();
                        break;
                    }
                case "Section":
                    {
                        InsEdit insEdit;
                        if (param == 0)
                        {
                            insEdit = new InsEdit("Добавление", name, dbConnectionString);
                        }
                        else
                        {
                            insEdit = new InsEdit("Изменение", name, dTable.Rows[dataGrid.SelectedIndex], dbConnectionString);
                        }
                        insEdit.Tag = param;
                        insEdit.ShowDialog();
                        break;
                    }
                case "Member":
                    {
                        InsEdit insEdit;
                        if (param == 0)
                        {
                            insEdit = new InsEdit("Добавление", name, dbConnectionString);
                        }
                        else
                        {
                            insEdit = new InsEdit("Изменение", name, dTable.Rows[dataGrid.SelectedIndex], dbConnectionString);
                        }
                        insEdit.Tag = param;
                        insEdit.ShowDialog();
                        break;
                    }
                case "Lecture":
                    {
                        InsEdit insEdit;
                        if (param == 0)
                        {
                            insEdit = new InsEdit("Добавление", name, dbConnectionString);
                        }
                        else
                        {
                            insEdit = new InsEdit("Изменение", name, dTable.Rows[dataGrid.SelectedIndex], dbConnectionString);
                        }
                        insEdit.Tag = param;
                        insEdit.ShowDialog();
                        break;
                    }
            }
            dTable.Clear();
            adapter = new NpgsqlDataAdapter(sqlQuery, dbConn);
            adapter.Fill(dTable);
            dataGrid.DataContext = dTable;
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (tableName)
            {
                case "Conference":
                    {
                        if (e.PropertyName == "id")
                        {
                            e.Column.Visibility = Visibility.Collapsed;
                        }
                        break;
                    }
                case "SectionLeaders":
                    {
                        if (e.PropertyName == "id")
                        {
                            e.Column.Visibility = Visibility.Collapsed;
                        }
                        break;
                    }
                case "Section":
                    {
                        if (e.PropertyName == "id")
                        {
                            e.Column.Visibility = Visibility.Collapsed;
                        }
                        if (e.PropertyName == "id_conference")
                        {
                            e.Column.Visibility = Visibility.Collapsed;
                        }
                        if (e.PropertyName == "id_employee")
                        {
                            e.Column.Visibility = Visibility.Collapsed;
                        }
                        break;
                    }
                case "Member":
                    {
                        if (e.PropertyName == "id")
                        {
                            e.Column.Visibility = Visibility.Collapsed;
                        }
                        break;
                    }
                case "Lecture":
                    {
                        if (e.PropertyName == "id")
                        {
                            e.Column.Visibility = Visibility.Collapsed;
                        }
                        if (e.PropertyName == "id_section")
                        {
                            e.Column.Visibility = Visibility.Collapsed;
                        }
                        if (e.PropertyName == "id_member")
                        {
                            e.Column.Visibility = Visibility.Collapsed;
                        }
                        break;
                    }
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBox1.IsEnabled = true;
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            dbConn.Open();
            string s = "";
            switch (tableName)
            {
                case "Conference":
                    s = $"SELECT ID, Name AS [Название конференции], OpeningDate AS [Дата начала], ClosingDate AS [Дата окончания] FROM {tableName} WHERE [{comboBox1.SelectedItem}] like '%{textBox1.Text}%'";
                    break;

                case "SectionLeaders":
                    s = $"SELECT ID, Name AS [ФИО], Department AS [Кафедра], Position AS [Должность] FROM {tableName} WHERE [{comboBox1.SelectedItem}] like '%{textBox1.Text}%'";
                    break;

                case "Section":
                    s = $"SELECT Section.ID, Section.Name AS [Название секции], Section.OpeningDate AS [Дата начала работы], Section.ClosingDate AS [Дата завершения работы], ID_Conference, Conference.Name AS [Название конференции], ID_Employee, SectionLeaders.Name AS [ФИО сотрудника] FROM {tableName} INNER JOIN Conference ON ID_Conference = Conference.ID INNER JOIN SectionLeaders ON ID_Employee = SectionLeaders.ID WHERE [{comboBox1.SelectedItem}] like '%{textBox1.Text}%'";
                    break;

                case "Member":
                    s = $"SELECT ID, Name AS [ФИО], WorkPlace AS [Место работы], WorkPosition AS [Должность], Email AS [Email], PhoneNumber AS [Контактный телефон], Address AS [Адрес] FROM {tableName} WHERE [{comboBox1.SelectedItem}] like '%{textBox1.Text}%'";
                    break;

                case "Lecture":
                    s = $"SELECT Lecture.ID, Lecture.Name AS [Название доклада], PerformanceDate AS [Дата выступления], ID_Section, Section.Name AS [Наименование секции], ID_Member, Member.Name AS [ФИО участника] FROM {tableName} INNER JOIN Section ON ID_Section = Section.Id INNER JOIN Member ON ID_Member = Member.ID WHERE [{comboBox1.SelectedItem}] like '%{textBox1.Text}%'";
                    break;
            }
            adapter = new NpgsqlDataAdapter(s, dbConn);
            dTable.Clear();
            adapter.Fill(dTable);
            dataGrid.DataContext = dTable;
            dbConn.Close();

            //DataView dv = dTable.DefaultView;
            //dv.RowFilter = $"[{comboBox1.SelectedItem.ToString()}] like '%{textBox1.Text}%'";
            //dataGrid.DataContext = dv.ToTable();
        }
    }
}