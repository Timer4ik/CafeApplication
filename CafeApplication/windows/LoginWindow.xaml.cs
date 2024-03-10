using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CafeApplication.windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            User user = CafeEntities.GetContext().Users.FirstOrDefault(u => u.Email == EmailField.Text && u.Password == PasswordField.Password);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            AdminWindow adminWindow = new AdminWindow { Owner = Owner };

            adminWindow.Show();

            Close();
        }
    }
}
