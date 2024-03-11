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
    /// Логика взаимодействия для EditOrAddFoodCategoryPage.xaml
    /// </summary>
    public partial class EditOrAddFoodCategoryPage : Page
    {
        public FoodCategory FoodCategory { get; set; }

        public List<FoodCategory> FoodCategoryCategories { get; set; }
        private ImageLoader FoodCategoryPhotoLoader { get; set; } = new ImageLoader();
        public EditOrAddFoodCategoryPage(FoodCategory _FoodCategory)
        {
            InitializeComponent();

            FoodCategoryCategories = CafeEntities.GetContext().FoodCategories.ToList();
            FoodCategory = (_FoodCategory ?? new FoodCategory());

            DataContext = this;
        }
        private bool CheckIsValidAndShowMessage()
        {
            Validator validator = new Validator();

            validator
                .Validate(FoodCategory.Name == null, "Название категории продукта обязательное к заполнению");

            if (validator.IsValid) return true;

            ShowPopup.Error(validator.StringErrors, "Ошибка валидации");

            return false;
        }

        private void SaveNewFoodCategory()
        {
            if (FoodCategoryPhotoLoader.PhotoName != null)
                FoodCategory.Photo = FoodCategoryPhotoLoader.SaveAndReturnFileName();

            CafeEntities.GetContext().FoodCategories.Add(FoodCategory);
            CafeEntities.GetContext().SaveChanges();

            NavigationService.GoBack();

            ShowPopup.Success("Продукт успешно создан");
        }
        private void SaveEditedFoodCategory()
        {
            if (FoodCategoryPhotoLoader.PhotoName != null)
                FoodCategory.Photo = FoodCategoryPhotoLoader.SaveAndReturnFileName();

            CafeEntities.GetContext().SaveChanges();

            NavigationService.GoBack();

            ShowPopup.Success("Продукт успешно изменён");
        }
        private void SaveOrAddFoodCategory(object sender, RoutedEventArgs e)
        {
            bool isValid = CheckIsValidAndShowMessage();
            if (!isValid) return;

            try
            {
                bool isEditPage = FoodCategory.FoodCategoryId > 0;

                if (isEditPage) SaveEditedFoodCategory();

                else SaveNewFoodCategory();
            }
            catch (Exception error)
            {
                ShowPopup.InnerException(error);
            }
        }
        private void SelectPhoto(object sender, RoutedEventArgs e)
        {
            FoodCategoryPhoto.Source = FoodCategoryPhotoLoader.ShowDialog(5) ?? FoodCategoryPhoto.Source;
        }
    }
}
