using CafeApplication;
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
using CafeApplication.utils;
using System.IO;

namespace CafeApplication.pages
{
    public partial class EditOrAddEmployeePage : Page
    {
        public User User { get; set; }
        public List<Role> Roles { get; set; }
        private ImageLoader UserPhotoLoader { get; set; } = new ImageLoader();
        public EditOrAddEmployeePage(User _user)
        {
            InitializeComponent();

            Roles = CafeEntities.GetContext().Roles.ToList();
            User = (_user ?? new User());

            this.DataContext = this;
        }
        private bool CheckIsValidAndShowMessage()
        {
            Validator validator = new Validator();

            validator
                .Min(User.FullName, 5, "Фио не может быть меньше 5 символов")
                .Validate(User.FullName == null || User.FullName.Split(' ').Length != 3, "Фио должно содержать фамиилию, имя, отчество")
                .Min(User.FullName, 5, "Фио не может быть меньше 5 символов")
                .Validate(User.Role == null, "Поле роль обязательно к заполнению")
                .IsEmail(User.Email)
                .Min(User.Password, 6, "Поле пароль должно содержать не менее 6 символов");

            if (validator.IsValid) return true;

            ShowPopup.Error(validator.StringErrors, "Ошибка валидации");

            return false;
        }

        private void SaveNewEmployee()
        {
            if (UserPhotoLoader.PhotoName != null)
                User.Photo = UserPhotoLoader.SaveAndReturnFileName();

            CafeEntities.GetContext().Users.Add(User);
            CafeEntities.GetContext().SaveChanges();

            NavigationService.GoBack();

            ShowPopup.Success("Пользователь успешно создан");
        }
        private void SaveEditedEmployee()
        {
            if (UserPhotoLoader.PhotoName != null)
                User.Photo = UserPhotoLoader.SaveAndReturnFileName();

            CafeEntities.GetContext().SaveChanges();

            NavigationService.GoBack();

            ShowPopup.Success("Пользователь успешно изменён");
        }
        private void SaveOrAddEmployee(object sender, RoutedEventArgs e)
        {
            bool isValid = CheckIsValidAndShowMessage();
            if (!isValid) return;

            try
            {
                bool isEditPage = User.UserId > 0;

                if (isEditPage) SaveEditedEmployee();

                else SaveNewEmployee();
            }
            catch (Exception error)
            {
                ShowPopup.InnerException(error);
            }

        }
        private void SelectPhoto(object sender, RoutedEventArgs e)
        {
            UserPhoto.Source = UserPhotoLoader.ShowDialog(5) ?? UserPhoto.Source;
        }
    }
}
