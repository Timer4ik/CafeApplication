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
    public partial class ChangesPage : Page
    {
        public System.DateTime SelectedDate { get; set; }
        public User SelectedUser { get; set; }
        public List<User> Users { get; set; }

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
                EmployeeDataGrid.ItemsSource = CafeEntities.GetContext().ChangeEmployees
                    .Where(item => item.Change.ChangeDate == SelectedDate)
                    .ToList()
                    .ConvertAll(i => i.User); ;

                Users = CafeEntities.GetContext().Users.ToList();
            }
            catch (Exception ex)
            {
                ShowPopup.InnerException(ex);
            }
        }
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            FetchData();
        }
        public ChangesPage()
        {
            InitializeComponent();

            DataContext = this;
        }
        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            Change change = CafeEntities.GetContext().Changes.FirstOrDefault(i => i.ChangeDate == SelectedDate);
            if (change == null)
            {
                CafeEntities.GetContext().Changes.Add(new Change { ChangeDate = SelectedDate});
                CafeEntities.GetContext().SaveChanges();
                
                change = CafeEntities.GetContext().Changes.FirstOrDefault(i => i.ChangeDate == SelectedDate);
            }

            ChangeEmployee user = CafeEntities.GetContext().ChangeEmployees.FirstOrDefault(i => i.UserId == SelectedUser.UserId && i.ChangeId == change.ChangeId);

            if (user != null) return;

            CafeEntities.GetContext().ChangeEmployees.Add(new ChangeEmployee()
            {
                ChangeId = change.ChangeId,
                User = SelectedUser
            });
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
