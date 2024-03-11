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

namespace CafeApplication.pages.food
{
    /// <summary>
    /// Логика взаимодействия для FoodsPage.xaml
    /// </summary>
    public partial class FoodsPage : Page
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
                FoodsDataGrid.ItemsSource = CafeEntities.GetContext().Foods.ToList();
            }
            catch (Exception ex)
            {
                ShowPopup.InnerException(ex);
            }
        }
        public FoodsPage()
        {
            InitializeComponent();
            FetchData();
        }
        private void AddFood(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditOrAddFoodPage(null));
        }

        private void EditFood(object sender, RoutedEventArgs e)
        {
            Food Food = (sender as Button).DataContext as Food;

            NavigationService.Navigate(new EditOrAddFoodPage(Food));
        }

        private void DeleteFood(object sender, RoutedEventArgs e)
        {
            List<Food> foods = FoodsDataGrid.SelectedItems.Cast<Food>().ToList();

            if (foods.Count <= 0) return;
            if (!ShowPopup.AreYouSure($"Вы точно хотите удалить {foods.Count} элементов")) return;

            CafeEntities.GetContext().Foods.RemoveRange(foods);
            CafeEntities.GetContext().SaveChanges();
            FetchData();
        }
    }
}
