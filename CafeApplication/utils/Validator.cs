using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApplication.utils
{
    public class Validator
    {
        private StringBuilder Errors { get; set; }
        public Validator()
        {
            Errors = new StringBuilder();
        }
        public string StringErrors { get { return Errors.ToString(); } }
        public bool IsValid { get { return Errors.Length <= 0; } }

        public void ClearError()
        {
            Errors.Clear();
        }
        public Validator Min(string str, int min, string message)
        {
            if (str == null || str?.Length < min)
                Errors.AppendLine(message);

            return this;
        }
        public Validator Required(string str, string message)
        {
            if (str == null || str?.Length == 0)
                Errors.AppendLine(message);

            return this;
        }
        public Validator IsEmail(string str)
        {
            if (str == null || str?.Length == 0 || !str.Contains('@'))
                Errors.AppendLine("Некорректный e-mail");

            return this;
        }
        public Validator Validate(bool valid, string message)
        {
            if (valid)
                Errors.AppendLine(message);

            return this;
        }
    }
}
