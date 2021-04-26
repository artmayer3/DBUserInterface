using System;
using System.Data;
using Npgsql;
using System.Windows;

namespace Lab6._2
{
    /// <summary>
    /// Логика взаимодействия для InsEdit.xaml
    /// </summary>
    public partial class InsEdit : Window
    {
        private NpgsqlConnection dbConn;
        private NpgsqlCommand sqlCmd;
        private NpgsqlDataAdapter adapter;
        private DataSet tmp;
        private DataRow currentDataRow;
        private string tableName;

        public InsEdit(string title, string name, string dbConnection)
        {
            InitializeComponent();
            Title = title;
            tableName = name;
            dbConn = new NpgsqlConnection(dbConnection);
            tmp = new DataSet();
            dbConn.Open();
            switch (name)
            {
                case "Conference":
                    {
                        label1.Content = "Название:";
                        label2.Content = "Дата начала:";
                        label3.Content = "Дата окончания:";
                        label1.Visibility = Visibility.Visible;
                        label2.Visibility = Visibility.Visible;
                        label3.Visibility = Visibility.Visible;
                        textBox1.Visibility = Visibility.Visible;
                        datePicker1.Visibility = Visibility.Visible;
                        datePicker2.Visibility = Visibility.Visible;
                        break;
                    }
                case "SectionLeaders":
                    {
                        label1.Content = "ФИО:";
                        label2.Content = "Кафедра:";
                        label3.Content = "Должность:";
                        label1.Visibility = Visibility.Visible;
                        label2.Visibility = Visibility.Visible;
                        label3.Visibility = Visibility.Visible;
                        textBox1.Visibility = Visibility.Visible;
                        textBox2.Visibility = Visibility.Visible;
                        textBox3.Visibility = Visibility.Visible;
                        break;
                    }
                case "Section":
                    {
                        Height = 420;
                        button1.Margin = new Thickness(70, 350, 0, 0);
                        button2.Margin = new Thickness(209, 350, 0, 0);
                        label1.Content = "Название:";
                        label2.Content = "Дата начала работы:";
                        label3.Content = "Дата завершения:";
                        label4.Content = "Конференция:";
                        label5.Content = "Руководители секции:";
                        label1.Visibility = Visibility.Visible;
                        label2.Visibility = Visibility.Visible;
                        label3.Visibility = Visibility.Visible;
                        label4.Visibility = Visibility.Visible;
                        label5.Visibility = Visibility.Visible;
                        textBox1.Visibility = Visibility.Visible;
                        datePicker1.Visibility = Visibility.Visible;
                        datePicker2.Visibility = Visibility.Visible;
                        comboBox1.Margin = new Thickness(176, 232, 0, 0);
                        comboBox2.Margin = new Thickness(176, 295, 0, 0);
                        comboBox1.Visibility = Visibility.Visible;
                        comboBox2.Visibility = Visibility.Visible;

                        tmp.Tables.Add(new DataTable("Conference"));
                        adapter = new NpgsqlDataAdapter($"SELECT ID, Name FROM Conference", dbConn);
                        adapter.Fill(tmp.Tables["Conference"]);

                        tmp.Tables.Add(new DataTable("SectionLeaders"));
                        adapter = new NpgsqlDataAdapter($"SELECT ID, Name FROM SECTION_LEADER", dbConn);
                        adapter.Fill(tmp.Tables["SectionLeaders"]);

                        comboBox1.ItemsSource = tmp.Tables["Conference"].DefaultView;
                        comboBox1.DisplayMemberPath = "name";
                        comboBox1.SelectedValuePath = "id";
                        comboBox2.ItemsSource = tmp.Tables["SectionLeaders"].DefaultView;
                        comboBox2.DisplayMemberPath = "name";
                        comboBox2.SelectedValuePath = "id";
                        break;
                    }
                case "Member":
                    {
                        Height = 500;
                        button1.Margin = new Thickness(70, 400, 0, 0);
                        button2.Margin = new Thickness(209, 400, 0, 0);
                        label1.Content = "ФИО:";
                        label2.Content = "Место работы:";
                        label3.Content = "Должность:";
                        label4.Content = "Email:";
                        label5.Content = "Контактный телефон:";
                        label6.Content = "Адрес:";
                        label1.Visibility = Visibility.Visible;
                        label2.Visibility = Visibility.Visible;
                        label3.Visibility = Visibility.Visible;
                        label4.Visibility = Visibility.Visible;
                        label5.Visibility = Visibility.Visible;
                        label6.Visibility = Visibility.Visible;
                        textBox1.Visibility = Visibility.Visible;
                        textBox2.Visibility = Visibility.Visible;
                        textBox3.Visibility = Visibility.Visible;
                        textBox4.Visibility = Visibility.Visible;
                        textBox5.Visibility = Visibility.Visible;
                        textBox6.Visibility = Visibility.Visible;
                        break;
                    }
                case "Lecture":
                    {
                        Height = 350;
                        button1.Margin = new Thickness(70, 280, 0, 0);
                        button2.Margin = new Thickness(209, 280, 0, 0);
                        label1.Content = "Название доклада:";
                        label2.Content = "Дата выступления:";
                        label3.Content = "Секция:";
                        label4.Content = "Участник:";
                        label1.Visibility = Visibility.Visible;
                        label2.Visibility = Visibility.Visible;
                        label3.Visibility = Visibility.Visible;
                        label4.Visibility = Visibility.Visible;
                        textBox1.Visibility = Visibility.Visible;
                        datePicker1.Visibility = Visibility.Visible;
                        comboBox1.Visibility = Visibility.Visible;
                        comboBox2.Visibility = Visibility.Visible;
                        comboBox1.Margin = new Thickness(176, 169, 0, 0);
                        comboBox2.Margin = new Thickness(176, 232, 0, 0);

                        tmp.Tables.Add(new DataTable("Section"));
                        adapter = new NpgsqlDataAdapter($"SELECT ID, Name FROM Section", dbConn);
                        adapter.Fill(tmp.Tables["Section"]);

                        tmp.Tables.Add(new DataTable("Member"));
                        adapter = new NpgsqlDataAdapter($"SELECT ID, Name FROM Member", dbConn);
                        adapter.Fill(tmp.Tables["Member"]);

                        comboBox1.ItemsSource = tmp.Tables["Section"].DefaultView;
                        comboBox1.DisplayMemberPath = "name";
                        comboBox1.SelectedValuePath = "id";
                        comboBox2.ItemsSource = tmp.Tables["Member"].DefaultView;
                        comboBox2.DisplayMemberPath = "name";
                        comboBox2.SelectedValuePath = "id";
                        break;
                    }
            }
            dbConn.Close();
        }

        public InsEdit(string title, string name, DataRow currentRow, string dbConnectionString)
        {
            InitializeComponent();
            Title = title;
            currentDataRow = currentRow;
            tableName = name;
            dbConn = new NpgsqlConnection(dbConnectionString);
            tmp = new DataSet();
            dbConn.Open();
            switch (name)
            {
                case "Conference":
                    {
                        label1.Content = "Название:";
                        label2.Content = "Дата начала:";
                        label3.Content = "Дата окончания:";
                        label1.Visibility = Visibility.Visible;
                        label2.Visibility = Visibility.Visible;
                        label3.Visibility = Visibility.Visible;
                        textBox1.Visibility = Visibility.Visible;
                        datePicker1.Visibility = Visibility.Visible;
                        datePicker2.Visibility = Visibility.Visible;
                        button1.Content = "Изменить";
                        textBox1.Text = currentRow["NAME"].ToString();
                        datePicker1.SelectedDate = DateTime.Parse(currentRow["OPENING_DATE"].ToString());
                        datePicker2.SelectedDate = DateTime.Parse(currentRow["CLOSING_DATE"].ToString());
                        break;
                    }
                case "SectionLeaders":
                    {
                        label1.Content = "ФИО:";
                        label2.Content = "Кафедра:";
                        label3.Content = "Должность:";
                        label1.Visibility = Visibility.Visible;
                        label2.Visibility = Visibility.Visible;
                        label3.Visibility = Visibility.Visible;
                        textBox1.Visibility = Visibility.Visible;
                        textBox2.Visibility = Visibility.Visible;
                        textBox3.Visibility = Visibility.Visible;
                        button1.Content = "Изменить";
                        textBox1.Text = currentRow["NAME"].ToString();
                        textBox2.Text = currentRow["DEPARTMENT"].ToString();
                        textBox3.Text = currentRow["JOB_POSITION"].ToString();
                        break;
                    }
                case "Section":
                    {
                        Height = 420;
                        button1.Margin = new Thickness(70, 350, 0, 0);
                        button2.Margin = new Thickness(209, 350, 0, 0);
                        label1.Content = "Название:";
                        label2.Content = "Дата начала работы:";
                        label3.Content = "Дата завершения:";
                        label4.Content = "Конференция:";
                        label5.Content = "Руководители секции:";
                        label1.Visibility = Visibility.Visible;
                        label2.Visibility = Visibility.Visible;
                        label3.Visibility = Visibility.Visible;
                        label4.Visibility = Visibility.Visible;
                        label5.Visibility = Visibility.Visible;
                        textBox1.Visibility = Visibility.Visible;
                        datePicker1.Visibility = Visibility.Visible;
                        datePicker2.Visibility = Visibility.Visible;
                        comboBox1.Margin = new Thickness(176, 232, 0, 0);
                        comboBox2.Margin = new Thickness(176, 295, 0, 0);
                        comboBox1.Visibility = Visibility.Visible;
                        comboBox2.Visibility = Visibility.Visible;

                        tmp.Tables.Add(new DataTable("Conference"));
                        adapter = new NpgsqlDataAdapter($"SELECT ID, Name FROM Conference", dbConn);
                        adapter.Fill(tmp.Tables["Conference"]);

                        tmp.Tables.Add(new DataTable("SectionLeaders"));
                        adapter = new NpgsqlDataAdapter($"SELECT ID, Name FROM SECTION_LEADER", dbConn);
                        adapter.Fill(tmp.Tables["SectionLeaders"]);

                        comboBox1.ItemsSource = tmp.Tables["Conference"].DefaultView;
                        comboBox1.DisplayMemberPath = "name";
                        comboBox1.SelectedValuePath = "id";
                        comboBox1.SelectedValue = currentRow["ID_CONFERENCE"];

                        comboBox2.ItemsSource = tmp.Tables["SectionLeaders"].DefaultView;
                        comboBox2.DisplayMemberPath = "name";
                        comboBox2.SelectedValuePath = "id";
                        comboBox2.SelectedValue = currentRow["ID_EMPLOYEE"];

                        button1.Content = "Изменить";
                        textBox1.Text = currentRow["NAME"].ToString();
                        datePicker1.SelectedDate = DateTime.Parse(currentRow["OPENING_DATE"].ToString());
                        datePicker2.SelectedDate = DateTime.Parse(currentRow["CLOSING_DATE"].ToString());
                        break;
                    }
                case "Member":
                    {
                        Height = 500;
                        button1.Margin = new Thickness(70, 400, 0, 0);
                        button2.Margin = new Thickness(209, 400, 0, 0);
                        label1.Content = "ФИО:";
                        label2.Content = "Место работы:";
                        label3.Content = "Должность:";
                        label4.Content = "Email:";
                        label5.Content = "Контактный телефон:";
                        label6.Content = "Адрес:";
                        label1.Visibility = Visibility.Visible;
                        label2.Visibility = Visibility.Visible;
                        label3.Visibility = Visibility.Visible;
                        label4.Visibility = Visibility.Visible;
                        label5.Visibility = Visibility.Visible;
                        label6.Visibility = Visibility.Visible;
                        textBox1.Visibility = Visibility.Visible;
                        textBox2.Visibility = Visibility.Visible;
                        textBox3.Visibility = Visibility.Visible;
                        textBox4.Visibility = Visibility.Visible;
                        textBox5.Visibility = Visibility.Visible;
                        textBox6.Visibility = Visibility.Visible;
                        button1.Content = "Изменить";
                        textBox1.Text = currentRow["NAME"].ToString();
                        textBox2.Text = currentRow["JOB_PLACE"].ToString();
                        textBox3.Text = currentRow["JOB_POSITION"].ToString();
                        textBox4.Text = currentRow["EMAIL"].ToString();
                        textBox5.Text = currentRow["PHONE_NUMBER"].ToString();
                        textBox6.Text = currentRow["ADDRESS"].ToString();
                        break;
                    }
                case "Lecture":
                    {
                        Height = 350;
                        button1.Margin = new Thickness(70, 280, 0, 0);
                        button2.Margin = new Thickness(209, 280, 0, 0);
                        label1.Content = "Название доклада:";
                        label2.Content = "Дата выступления:";
                        label3.Content = "Секция:";
                        label4.Content = "Участник:";
                        label1.Visibility = Visibility.Visible;
                        label2.Visibility = Visibility.Visible;
                        label3.Visibility = Visibility.Visible;
                        label4.Visibility = Visibility.Visible;
                        textBox1.Visibility = Visibility.Visible;
                        datePicker1.Visibility = Visibility.Visible;
                        comboBox1.Visibility = Visibility.Visible;
                        comboBox2.Visibility = Visibility.Visible;
                        comboBox1.Margin = new Thickness(176, 169, 0, 0);
                        comboBox2.Margin = new Thickness(176, 232, 0, 0);

                        tmp.Tables.Add(new DataTable("Section"));
                        adapter = new NpgsqlDataAdapter($"SELECT ID, Name FROM Section", dbConn);
                        adapter.Fill(tmp.Tables["Section"]);

                        tmp.Tables.Add(new DataTable("Member"));
                        adapter = new NpgsqlDataAdapter($"SELECT ID, Name FROM Member", dbConn);
                        adapter.Fill(tmp.Tables["Member"]);

                        comboBox1.ItemsSource = tmp.Tables["Section"].DefaultView;
                        comboBox1.DisplayMemberPath = "name";
                        comboBox1.SelectedValuePath = "id";
                        comboBox1.SelectedValue = currentRow["ID_SECTION"];

                        comboBox2.ItemsSource = tmp.Tables["Member"].DefaultView;
                        comboBox2.DisplayMemberPath = "name";
                        comboBox2.SelectedValuePath = "id";
                        comboBox2.SelectedValue = currentRow["ID_MEMBER"];

                        button1.Content = "Изменить";
                        textBox1.Text = currentRow["NAME"].ToString();
                        datePicker1.SelectedDate = DateTime.Parse(currentRow["PERFORMANCE_DATE"].ToString());
                        break;
                    }
            }
            dbConn.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (tableName)
                {
                    case "Conference":
                        {
                            if (textBox1.Text == "" || datePicker1.SelectedDate.ToString() == "" || datePicker2.SelectedDate.ToString() == "")
                            {
                                MessageBox.Show("Заполнены не все поля");
                            }
                            else
                            {
                                dbConn.Open();
                                if ((int)Tag == 0)
                                {
                                    sqlCmd = new NpgsqlCommand($"CALL INSERT_INTO_CONFERENCE('{textBox1.Text}', '{datePicker1.SelectedDate}', '{datePicker2.SelectedDate}')", dbConn);
                                }
                                else
                                {
                                    sqlCmd = new NpgsqlCommand($"CALL UPDATE_CONFERENCE({currentDataRow[0]}, '{textBox1.Text}', '{datePicker1.SelectedDate}', '{datePicker2.SelectedDate}')", dbConn);
                                }
                                sqlCmd.ExecuteNonQuery();
                                InsEditWindow.Close();
                                dbConn.Close();
                            }
                            break;
                        }
                    case "SectionLeaders":
                        {
                            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                            {
                                MessageBox.Show("Заполнены не все поля");
                            }
                            else
                            {
                                dbConn.Open();
                                if ((int)Tag == 0)
                                {
                                    sqlCmd = new NpgsqlCommand($"CALL INSERT_INTO_SECTION_LEADER('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}')", dbConn);
                                }
                                else
                                {
                                    sqlCmd = new NpgsqlCommand($"CALL UPDATE_SECTION_LEADER({currentDataRow[0]}, '{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}')", dbConn);
                                }
                                sqlCmd.ExecuteNonQuery();
                                InsEditWindow.Close();
                                dbConn.Close();
                            }
                            break;
                        }
                    case "Section":
                        {
                            if (textBox1.Text == "" || datePicker1.SelectedDate.ToString() == "" || datePicker2.SelectedDate.ToString() == "" || comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
                            {
                                MessageBox.Show("Заполнены не все поля");
                            }
                            else
                            {
                                dbConn.Open();
                                if ((int)Tag == 0)
                                {
                                    sqlCmd = new NpgsqlCommand($"CALL INSERT_INTO_SECTION('{textBox1.Text}', '{datePicker1.SelectedDate}', '{datePicker2.SelectedDate}', '{comboBox1.SelectedValue}', '{comboBox2.SelectedValue}')", dbConn);
                                }
                                else
                                {
                                    sqlCmd = new NpgsqlCommand($"CALL UPDATE_SECTION({currentDataRow[0]}, '{textBox1.Text}', '{datePicker1.SelectedDate}', '{datePicker2.SelectedDate}', '{comboBox1.SelectedValue}', '{comboBox2.SelectedValue}')", dbConn);
                                }
                                sqlCmd.ExecuteNonQuery();
                                InsEditWindow.Close();
                                dbConn.Close();
                            }
                            break;
                        }
                    case "Member":
                        {
                            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
                            {
                                MessageBox.Show("Заполнены не все поля");
                            }
                            else
                            {
                                dbConn.Open();
                                if ((int)Tag == 0)
                                {
                                    sqlCmd = new NpgsqlCommand($"CALL INSERT_INTO_MEMBER('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}', '{textBox5.Text}', '{textBox6.Text}')", dbConn);
                                }
                                else
                                {
                                    sqlCmd = new NpgsqlCommand($"CALL UPDATE_MEMBER({currentDataRow[0]}, '{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}', '{textBox5.Text}', '{textBox6.Text}')", dbConn);
                                }
                                sqlCmd.ExecuteNonQuery();
                                InsEditWindow.Close();
                                dbConn.Close();
                            }
                            break;
                        }
                    case "Lecture":
                        {
                            if (textBox1.Text == "" || datePicker1.SelectedDate.ToString() == "" || comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
                            {
                                MessageBox.Show("Заполнены не все поля");
                            }
                            else
                            {
                                dbConn.Open();
                                if ((int)Tag == 0)
                                {
                                    sqlCmd = new NpgsqlCommand($"CALL INSERT_INTO_LECTURE('{textBox1.Text}', '{datePicker1.SelectedDate}', '{comboBox1.SelectedValue}', '{comboBox2.SelectedValue}')", dbConn);
                                }
                                else
                                {
                                    sqlCmd = new NpgsqlCommand($"CALL UPDATE_LECTURE({currentDataRow[0]}, '{textBox1.Text}', '{datePicker1.SelectedDate}', '{comboBox1.SelectedValue}', '{comboBox2.SelectedValue}')", dbConn);
                                }
                                sqlCmd.ExecuteNonQuery();
                                InsEditWindow.Close();
                                dbConn.Close();
                            }
                            break;
                        }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            InsEditWindow.Close();
        }
    }
}