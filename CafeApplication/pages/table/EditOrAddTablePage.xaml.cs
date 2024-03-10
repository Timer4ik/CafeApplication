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
    /// Логика взаимодействия для EditOrAddTablePage.xaml
    /// </summary>
    public partial class EditOrAddTablePage : Page
    {
        public Table Table { get; set; }
        public EditOrAddTablePage(Table _Table)
        {
            InitializeComponent();

            Table = (_Table ?? new Table());

            DataContext = this;
        }
        private bool CheckIsValidAndShowMessage()
        {
            Validator validator = new Validator();

            validator
                .Validate(Table.Num == null, "Укажите номер столика")
                .Validate(Table.SeatAmount < 1, "Количество мест не может быть меньше 1");

            if (validator.IsValid) return true;

            ShowPopup.Error(validator.StringErrors, "Ошибка валидации");

            return false;
        }

        private void SaveNewTable()
        {
            CafeEntities.GetContext().Tables.Add(Table);
            CafeEntities.GetContext().SaveChanges();

            NavigationService.GoBack();

            ShowPopup.Success("Столик успешно создан");
        }
        private void SaveEditedTable()
        {
            CafeEntities.GetContext().SaveChanges();

            NavigationService.GoBack();

            ShowPopup.Success("Столик успешно изменён");
        }
        private void SaveOrAddTable(object sender, RoutedEventArgs e)
        {
            bool isValid = CheckIsValidAndShowMessage();
            if (!isValid) return;

            try
            {
                bool isEditPage = Table.TableId > 0;

                if (isEditPage) SaveEditedTable();

                else SaveNewTable();
            }
            catch (Exception error)
            {
                ShowPopup.InnerException(error);
            }
        }
    }
}
