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


            bool[] checks = new bool[] { false, false, false, false, false };
            string[] errors = new string[] { "Ошибка: Проверьте поле ввода логина", "Ошибка: Проверьте поле ввода пароля", "Ошибка: Проверьте поле ввода повторного пароля", "Укажите логин в форме х@x.x", "Укажите логин в форме х@x.x", "Пароли не совподают" };
            if (inputpassword.Password.Length > 0) { checks[0] = true; }                         // логин введен

            if (inputpassword2.Password.Length > 0) { checks[1] = true; }                       // пароль введен

            if (inputlogin.Text.Split('@').Length == 2) { checks[2] = true; }                  // в логине есть @

            if (inputlogin.Text.Split('@')[1].Split('.').Length == 2) { checks[3] = true; }          // в логине есть точка

            if (inputpassword.Password == inputpassword2.Password) { checks[4] = true; }     // пароли совпадают

            if (checks.Contains(false)) { new Error(errors[Array.IndexOf(checks, false)]).Show(); }
            else { new SuccessWindow("Новый аккаунт успешно создан!"); this.Close(); }

            //checks.Contains(false) ? new Error(errors[checks.IndexOf(false)]).Show() : new SuccessWindow("Новый аккаунт успешно создан!"); this.Close();


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
                new Error("Логин уже занят!").Show();
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
