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
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void button_done(object sender, RoutedEventArgs e)
        {
            if (inputlogin.Text.Length > 0)
            {
                if (inputpassword.Password.Length > 0)
                {
                    //поиск в БД
                    new SuccessWindow("Вход выполнен!").Show();
                }
                else { new Error("Ошибка: Проверьте поле ввода пароля").Show(); }
            }
            else { new Error("Ошибка: Проверьте поле ввода логина").Show(); }
        }

        private void button_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_register(object sender, RoutedEventArgs e)
        {
            new Registration().ShowDialog();
        }
    }
}
