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

namespace CafeApplication.pages.table
{
    /// <summary>
    /// Логика взаимодействия для TablesPage.xaml
    /// </summary>
    public partial class TablesPage : Page
    {
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
                TablesDataGrid.ItemsSource = CafeEntities.GetContext().Tables.ToList();
            }
            catch (Exception ex)
            {
                ShowPopup.InnerException(ex);
            }
        }
        public TablesPage()
        {
            InitializeComponent();
            FetchData();
        }
        private void AddTable(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditOrAddTablePage(null));
        }

        private void EditTable(object sender, RoutedEventArgs e)
        {
            Table table= (sender as Button).DataContext as Table;

            NavigationService.Navigate(new EditOrAddTablePage(table));
        }

        private void DeleteTable(object sender, RoutedEventArgs e)
        {
            List<Table> tables= TablesDataGrid.SelectedItems.Cast<Table>().ToList();

            if (tables.Count <= 0) return;
            if (!ShowPopup.AreYouSure($"Вы точно хотите удалить {tables.Count} элементов")) return;

            CafeEntities.GetContext().Tables.RemoveRange(tables);
            CafeEntities.GetContext().SaveChanges();
            FetchData();
        }
 
    }
}
