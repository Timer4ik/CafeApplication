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
    /// Логика взаимодействия для EditOrAddFoodPage.xaml
    /// </summary>
    public partial class EditOrAddFoodPage : Page
    {
        public Food Food { get; set; }

        public List<FoodCategory> FoodCategories { get; set; }
        private ImageLoader FoodPhotoLoader { get; set; } = new ImageLoader();
        public EditOrAddFoodPage(Food _Food)
        {
            InitializeComponent();

            FoodCategories = CafeEntities.GetContext().FoodCategories.ToList();
            Food = (_Food ?? new Food());

            DataContext = this;
        }
        private bool CheckIsValidAndShowMessage()
        {
            Validator validator = new Validator();

            validator
                .Validate(Food.Name == null, "Название продукта обязательное к заполнению")
                .Validate(Food.Price < 1, "Стоимость продукта не может быть меньше 1")
                .Validate(Food.FoodCategory == null, "Выберите категорию продукта");

            if (validator.IsValid) return true;

            ShowPopup.Error(validator.StringErrors, "Ошибка валидации");

            return false;
        }

        private void SaveNewFood()
        {
            if (FoodPhotoLoader.PhotoName != null)
                Food.Photo = FoodPhotoLoader.SaveAndReturnFileName();

            CafeEntities.GetContext().Foods.Add(Food);
            CafeEntities.GetContext().SaveChanges();

            NavigationService.GoBack();

            ShowPopup.Success("Продукт успешно создан");
        }
        private void SaveEditedFood()
        {
            if (FoodPhotoLoader.PhotoName != null)
                Food.Photo = FoodPhotoLoader.SaveAndReturnFileName();

            CafeEntities.GetContext().SaveChanges();

            NavigationService.GoBack();

            ShowPopup.Success("Продукт успешно изменён");
        }
        private void SaveOrAddFood(object sender, RoutedEventArgs e)
        {
            bool isValid = CheckIsValidAndShowMessage();
            if (!isValid) return;

            try
            {
                bool isEditPage = Food.FoodId > 0;

                if (isEditPage) SaveEditedFood();

                else SaveNewFood();
            }
            catch (Exception error)
            {
                ShowPopup.InnerException(error);
            }
        }
        private void SelectPhoto(object sender, RoutedEventArgs e)
        {
            FoodPhoto.Source = FoodPhotoLoader.ShowDialog(5) ?? FoodPhoto.Source;
        }
    }
}
