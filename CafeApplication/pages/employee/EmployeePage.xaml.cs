using CafeApplication.utils;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CafeApplication.pages
{
    public partial class EmployeePage : Page
    {
        // При возвращении на страницу переизвлекаются изменённые сущности модели
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                CafeEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                FetchData();
            }
            catch (Exception error)
            {
                ShowPopup.InnerException(error);
            }
        }
        // Извлечение данных
        private void FetchData()
        {
            try
            {
                EmployeeDataGrid.ItemsSource = CafeEntities.GetContext().Users.ToList();
            }
            catch (Exception ex)
            {
                ShowPopup.InnerException(ex);
            }
        }
        public EmployeePage()
        {
            InitializeComponent();
            FetchData();
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditOrAddEmployeePage(null));
        }
        private void DeleteEmployee(object sender, RoutedEventArgs e)
        {
            List<User> users = EmployeeDataGrid.SelectedItems.Cast<User>().ToList();

            if (users.Count <= 0) return;
            if (!ShowPopup.AreYouSure($"Вы точно хотите удалить {users.Count} элементов")) return;

            CafeEntities.GetContext().Users.RemoveRange(users);
            CafeEntities.GetContext().SaveChanges();
            FetchData();
        }

        private void EditEmployee(object sender, RoutedEventArgs e)
        {
            User user = (sender as Button).DataContext as User;

            NavigationService.Navigate(new EditOrAddEmployeePage(user));
        }
       
    }
}
