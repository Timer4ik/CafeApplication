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
    /// <summary>
    /// Логика взаимодействия для FoodCategoriesPage.xaml
    /// </summary>
    public partial class FoodCategoriesPage : Page
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
                FoodCategoriesDataGrid.ItemsSource = CafeEntities.GetContext().FoodCategories.ToList();
            }
            catch (Exception ex)
            {
                ShowPopup.InnerException(ex);
            }
        }
        public FoodCategoriesPage()
        {
            InitializeComponent();
            FetchData();
        }

        private void AddFoodCategory(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditOrAddFoodCategoryPage(null));
        }    
        private void DeleteFoodCategory(object sender, RoutedEventArgs e)
        {
        }
        private void EditFoodCategory(object sender, RoutedEventArgs e)
        {
            FoodCategory foodCategory = (sender as Button).DataContext as FoodCategory;

            NavigationService.Navigate(new EditOrAddFoodCategoryPage(foodCategory));
        }
    }
}
