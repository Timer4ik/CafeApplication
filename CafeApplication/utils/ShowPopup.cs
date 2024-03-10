using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CafeApplication.utils
{
    public static class ShowPopup
    {
        public static void Success(string message = "Операция выполнена успешноо", string caption = "Успех")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public static void Warning(string message = "Внимание", string caption = "Внимание")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public static void Error(string message = "Ошибка", string caption = "Ошибка")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void AreYouSure(string message = "Вы уверены?", string caption = "Внимание")
        {
            MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
        public static void InnerException(Exception ex)
        {
            MessageBox.Show(ex.InnerException.ToString(), "Ошибка");
        }
    }
}
