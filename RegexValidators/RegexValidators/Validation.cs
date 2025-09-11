using System.Text.RegularExpressions;

namespace RegexValidators
{
    public class Validation
    {
        private static readonly Regex EmailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled);

        private static readonly Regex PhoneRegex = new Regex(@"^(0|\+84)[1-9]\d{8,9}$", RegexOptions.Compiled);

        public static bool IsNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsValidEmail(string email)
        {
            return IsNotEmpty(email) && EmailRegex.IsMatch(email);
        }

        public static bool IsValidPhone(string phone)
        {
            return IsNotEmpty(phone) && PhoneRegex.IsMatch(phone);
        }

        public static bool IsValidBirthday(DateTime? birthday)
        {
            return birthday.HasValue && birthday.Value <= DateTime.Now;
        }

        public static bool IsValidId(string id)
        {
            return IsNotEmpty(id) && Regex.IsMatch(id, @"^\d+$"); 
        }

        public static bool IsValidName(string name)
        {
            return IsNotEmpty(name) && Regex.IsMatch(name, @"^[a-zA-Z\s]+$");
        }

        public static bool IsValidEClass(string eClass)
        {
            return IsNotEmpty(eClass);
        }
    }
}
