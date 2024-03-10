using CafeApplication.pages;
using CafeApplication.pages.food;
using CafeApplication.pages.table;
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

namespace CafeApplication.windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new EmployeePage());
        }

        private void Button_Click_Employee(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new EmployeePage());
        }

        private void Button_Click_Orders(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrdersPage());
        }

        private void Button_Click_Changes(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ChangesPage());
        }        
        private void Button_Click_Foods(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FoodsPage());
        }
        private void Button_Click_FoodCategories(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FoodCategoriesPage());
        }
        private void Button_Click_Tables(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TablesPage());
        }
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }
        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                EmployeeButton.Visibility = Visibility.Collapsed;
                OrdersButton.Visibility = Visibility.Collapsed;
                ChangesButton.Visibility = Visibility.Collapsed;
                FoodsButton.Visibility = Visibility.Collapsed;
                TablesButton.Visibility = Visibility.Collapsed;
                FoodCategoriesButton.Visibility = Visibility.Collapsed;
                BackButton.Visibility = Visibility.Visible;
            }
            else
            {
                EmployeeButton.Visibility = Visibility.Visible;
                OrdersButton.Visibility = Visibility.Visible;
                ChangesButton.Visibility = Visibility.Visible;
                FoodsButton.Visibility= Visibility.Visible;
                TablesButton.Visibility= Visibility.Visible;
                FoodCategoriesButton.Visibility= Visibility.Visible;
                BackButton.Visibility = Visibility.Collapsed;
            }
        }
      
    }
}
