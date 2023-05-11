using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        // подключение созданого класса - DataBase.cs
        DataBase dataBase = new DataBase();
        public Registration()
        {
            InitializeComponent();
        }

        private void button_ok(object sender, RoutedEventArgs e)
        {
            // Максимальная длина строк, как в базе данных
            inputlogin.MaxLength = 50;
            inputpassword.MaxLength = 50;

            // Переменные для базы данных
            var loginUser = inputlogin.Text; 
            var passwordUser = inputpassword.Password;

            // Создание запроса, проверка Логина и Пароля с базой данных
            string querystring = $"insert into register(login_user, password_user) values('{loginUser}', '{passwordUser}')";

            // 
            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            // Открытие подключения БД
            dataBase.OpenConnection();


            if (inputlogin.Text.Length > 0)
            {
                if (inputpassword.Password.Length > 0)
                {
                    if (inputpassword2.Password.Length > 0)
                    {
                        string[] dataLogin = inputlogin.Text.Split('@'); // делим строку на две части
                        if (dataLogin.Length == 2) // проверяем если у нас две части
                        {
                            string[] data2Login = dataLogin[1].Split('.'); // делим вторую часть ещё на две части
                            if (data2Login.Length == 2)
                            {
                                if (inputpassword.Password == inputpassword2.Password) // проверка на совпадение паролей
                                {
                                    if (command.ExecuteNonQuery() == 1)
                                    {
                                        new SuccessWindow("Новый аккаунт успешно создан!");
                                        this.Close();
                                    }
                                }
                                else new Error("Пароли не совподают").Show();
                            }
                            else new Error("Укажите логин в форме х@x.x").Show();
                        }
                        else new Error("Укажите логин в форме х@x.x").Show();
                    }
                    else new Error("Ошибка: Проверьте поле ввода повторного пароля").Show();
                }
                else new Error("Ошибка: Проверьте поле ввода пароля").Show();
            }
            else new Error("Ошибка: Проверьте поле ввода логина").Show();
            if (checkUser())

            {
                return;
            }
            // Закрытие подключение к БД
            dataBase.CloseConnection();
        }

        // Проверка на существующего пользователя
        private bool checkUser()
        {
            // 
            var loginUser = inputlogin.Text;
            var passwordUser = inputpassword.Password;

            // создание адаптера базы данных
            SqlDataAdapter adapter = new SqlDataAdapter();

            // создание таблицы для сверки с
            DataTable table = new DataTable();

            // Создание запроса на проверку Логина и Пароля
            string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user = '{passwordUser}'";

            // Связь с БД
            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            // для получения данных с бд
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0) // проверка данны строки БД с полученными данными
            {
                new SuccessWindow("Логин уже занят!").Show();
                return true;
            }
            else
                return false;
        }

        private void button_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
