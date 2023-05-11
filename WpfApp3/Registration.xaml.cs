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
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button_ok(object sender, RoutedEventArgs e)
        {
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
                                    new SuccessWindow("Новый аккаунт успешно создан!");
                                    this.Close();
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
        }

        private void button_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
