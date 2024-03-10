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
    public partial class OrdersPage : Page
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
                OrderDataGrid.ItemsSource = CafeEntities.GetContext().Orders.ToList();
            }
            catch (Exception ex)
            {
                ShowPopup.InnerException(ex);
            }
        }
        public OrdersPage()
        {
            InitializeComponent();
            FetchData();
        }
  
        private void AddOrder(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditOrAddOrderPage(null));
        }

        private void EditOrder(object sender, RoutedEventArgs e)
        {
            Order order= (sender as Button).DataContext as Order;

            NavigationService.Navigate(new EditOrAddOrderPage(order));
        }
    }
}
