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
    /// Логика взаимодействия для EditOrAddOrderPage.xaml
    /// </summary>
    public partial class EditOrAddOrderPage : Page
    {
        public Order Order { get; set; }
        public List<Table> Tables { get; set; } = CafeEntities.GetContext().Tables.ToList();
        public EditOrAddOrderPage(Order _order)
        {
            InitializeComponent();

            Change change = CafeEntities.GetContext().Changes.FirstOrDefault(i => i.ChangeDate == DateTime.Today);
            if (change == null)
            {
                CafeEntities.GetContext().Changes.Add(new Change { ChangeDate = DateTime.Today });
                CafeEntities.GetContext().SaveChanges();

                change = CafeEntities.GetContext().Changes.FirstOrDefault(i => i.ChangeDate == DateTime.Today);
            }

            Order = (_order ?? new Order()
            {
                ChangeId = change.ChangeId,
            });
            

            this.DataContext = this;
        }
        private bool CheckIsValidAndShowMessage()
        {
            Validator validator = new Validator();

            validator
                .Validate(Order.GuestsCount <= 0, "Количество гостей не может быть меньше 1")
                .Validate(Order.Table == null, "Поле номер столика обязательное к заполнению");

            if (validator.IsValid) return true;

            ShowPopup.Error(validator.StringErrors, "Ошибка валидации");

            return false;
        }

        private void SaveNewOrder()
        {
            CafeEntities.GetContext().Orders.Add(Order);
            CafeEntities.GetContext().SaveChanges();

            NavigationService.GoBack();

            ShowPopup.Success("Заказ успешно создан");
        }
        private void SaveEditedOrder()
        {
            CafeEntities.GetContext().SaveChanges();

            NavigationService.GoBack();

            ShowPopup.Success("Заказ успешно изменён");
        }
        private void SaveOrAddOrder(object sender, RoutedEventArgs e)
        {
            bool isValid = CheckIsValidAndShowMessage();
            if (!isValid) return;

            try
            {
                bool isEditPage = Order.OrderId > 0;

                if (isEditPage) SaveEditedOrder();

                else SaveNewOrder();
            }
            catch (Exception error)
            {
                ShowPopup.InnerException(error);
            }

        }
    }
}
