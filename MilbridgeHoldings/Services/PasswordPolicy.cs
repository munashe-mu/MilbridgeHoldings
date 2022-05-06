namespace ModelLibrary.Services
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    [Serializable]
    public class PasswordPolicy 
    {
        private int _minLength;
        private int _maxLength;
        private int _minSpecialChars;
        private int _minUpperCase;
        private int _minDigits;

        public int MinimumLength
        {
            get
            {
                return _minLength;
            }
            set
            {
                if (value <= 3) throw new ArgumentException("Minimum length has to be at least 4 characters");
                _minLength = value;
            }
        }

        public int MaximumLength
        {
            get { return _maxLength; }
            set
            {
                if (value < _minLength) throw new ArgumentException("Maximum length has to be at least equal to minimum length");
                _maxLength = value;
            }
        }

        public int MinimumSpecialChars
        {
            get { return _minSpecialChars; }
            set
            {
                if (value >= _minLength) throw new ArgumentException("Minimum special characters must be less than the minimum length");
                _minSpecialChars = value;
            }
        }

        public int MinimumUpperCase
        {
            get { return _minUpperCase; }
            set
            {
                if (value >= _minLength) throw new ArgumentException("Minimum uppercase characters must be less than the minimum length");
                _minUpperCase = value;
            }
        }

        public int MinimumDigits
        {
            get { return _minDigits; }
            set
            {
                if (value >= _minLength) throw new ArgumentException("Minimum digits must be less than the minimum length");
                _minDigits = value;
            }
        }

        public Task<IdentityResult> ValidateAsync(string item)
        {
            try
            {
                var validationErrors = new Stack<string>();
                if (item.Length < MinimumLength) validationErrors.Push(string.Format("Password must be at least {0} characters long", MinimumLength));
                if (item.Length > MaximumLength) validationErrors.Push(string.Format("Password must not be longer than {0} characters", MaximumLength));

                if (MinimumDigits > 0)
                {
                    var digits = item.ToCharArray().Count(Char.IsDigit);
                    if (digits < MinimumDigits) validationErrors.Push(string.Format("Password must contain at least {0} digits", MinimumDigits));
                }

                if (MinimumUpperCase > 0)
                {
                    var regUpperCase = new Regex("[A-Z]");
                    var upperCase = regUpperCase.Matches(item).Count;
                    if (upperCase < MinimumUpperCase) validationErrors.Push(string.Format("Password must contain at least {0} uppercase (A-Z) characters", MinimumUpperCase));
                }
                if (MinimumSpecialChars > 0)
                {
                    var specialchars = item.ToCharArray().Count(c => !Char.IsLetterOrDigit(c));
                    if (specialchars < MinimumSpecialChars) validationErrors.Push(string.Format("Password must contain at least {0} special characters", MinimumSpecialChars));
                }

                if (validationErrors.Count > 0)
                    return Task.FromResult(new IdentityResult());

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception)
            {
                return Task.FromResult(new IdentityResult());
            }
        }
    }
}
