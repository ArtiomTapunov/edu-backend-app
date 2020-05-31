using System.Text.RegularExpressions;
using System.Linq;

namespace ECA.Services.Helpers
{
    public static class PasswordStrength
    {
        public enum PasswordScore
        {
            Blank = 0,
            VeryWeak = 1,
            Weak = 2,
            Medium = 3,
            Strong = 4,
            VeryStrong = 5
        }

        public static PasswordScore CheckStrength(string password)
        {
            int score = 1;

            if (password.Length < 1)
                return PasswordScore.Blank;
            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 12)
                score++;
            if (password.Any(c => char.IsDigit(c)))
                score++;
            if (Regex.IsMatch(password, @"[a-z]", RegexOptions.ECMAScript) &&
                Regex.IsMatch(password, @"[A-Z]", RegexOptions.ECMAScript))
                score++;
            if (Regex.IsMatch(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript))
                score++;

            return (PasswordScore)score;
        }
    }
}
