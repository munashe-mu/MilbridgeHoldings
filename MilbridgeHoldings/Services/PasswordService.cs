namespace MilbridgeHoldings.Services
{
    using ModelLibrary.Services;
    using System;
    using System.Security.Cryptography;

    [Flags]
    public enum PasswordOptions
    {
        /// <summary>
        /// Default flag
        /// </summary>
        None = 0,

        /// <summary>
        /// Enforce the password to have special characters
        /// </summary>
        HasSymbols = 1,

        /// <summary>
        /// Enforce the password to have NO repeated characters
        /// </summary>
        NoRepeating = 1 << 1,

        /// <summary>
        ///  Enforce the password to have NO consecutive characters
        /// </summary>
        NoConsecutive = 1 << 2,

        /// <summary>
        /// Enforce the password to have digits
        /// </summary>
        HasDigits = 1 << 3,

        /// <summary>
        /// Enforce the password to have capital letters
        /// </summary>
        HasCapitals = 1 << 4,

        /// <summary>
        /// Enforce the password to have lowercase letters
        /// </summary>
        HasLower = 1 << 5
    }

    /// <summary>
    /// Service for generating passwords
    /// </summary>
    public sealed class PasswordService
    {
        private const int defaultMaximum = 15;
        private const int defaultMinimum = 8;
        private readonly string _exclusionSet;
        private readonly bool _noConsecutive;
        private readonly bool _hasDigits;
        private readonly bool _noRepeating;
        private readonly bool _hasSymbols;
        private readonly bool _hasCapitals;

        internal static object GeneratePassword(object p)
        {
            throw new NotImplementedException();
        }

        private readonly bool _hasLower;
        private const string lowerCaseCharacters = "abcdefghijklmnopqrstuvwxyz";
        private const string upperCaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string specialCharacters = "@#$%&?=+";
        private const string digitCharacters = "0123456789";
#pragma warning disable SYSLIB0023 // Type or member is obsolete
        private readonly RNGCryptoServiceProvider _rng;
#pragma warning restore SYSLIB0023 // Type or member is obsolete
        private int _maxSize;
        private int _minSize;

        private PasswordService(PasswordOptions options)
        {
            _exclusionSet = " `~^*()_-{}[];:'\"<>,./|\\";
            Minimum = defaultMinimum;
            Maximum = defaultMaximum;
            _noConsecutive = options.HasFlag(PasswordOptions.NoConsecutive);
            _noRepeating = options.HasFlag(PasswordOptions.NoRepeating);
            _hasSymbols = options.HasFlag(PasswordOptions.HasSymbols);
            _hasDigits = options.HasFlag(PasswordOptions.HasDigits);
            _hasCapitals = options.HasFlag(PasswordOptions.HasCapitals);
            _hasLower = options.HasFlag(PasswordOptions.HasLower);
#pragma warning disable SYSLIB0023 // Type or member is obsolete
            _rng = new RNGCryptoServiceProvider();
#pragma warning restore SYSLIB0023 // Type or member is obsolete
        }

        private int Maximum
        {
            get { return _maxSize; }
            set
            {
                _maxSize = value;
                if (_minSize >= _maxSize)
                {
                    _maxSize = defaultMaximum;
                }
            }
        }

        private int Minimum
        {
            get { return _minSize; }
            set
            {
                _minSize = value;
                if (defaultMinimum > _minSize)
                {
                    _minSize = defaultMinimum;
                }
            }
        }

        private string Generate()
        {
            // Pick random length between minimum and maximum   
            int pwdLength = GetCryptographicRandomNumber(Minimum, Maximum);

            var pwdBuffer = new char[pwdLength];

            if (_hasCapitals) //then enforce at least one capital
            {
                var capital = GetRandomCharacter(upperCaseCharacters);
                var index = GetCryptographicRandomNumber(0, pwdLength - 1);
                while (pwdBuffer[index] != default(char))
                {
                    index = GetCryptographicRandomNumber(0, pwdLength - 1);
                }
                pwdBuffer[index] = capital;
            }

            if (_hasDigits) //then enforce at least one digit
            {
                var digit = GetRandomCharacter(digitCharacters);
                var index = GetCryptographicRandomNumber(0, pwdLength - 1);
                while (pwdBuffer[index] != default(char))
                {
                    index = GetCryptographicRandomNumber(0, pwdLength - 1);
                }
                pwdBuffer[index] = digit;
            }

            if (_hasSymbols) //then enforce at least one symbol
            {
                var symbol = GetRandomCharacter(specialCharacters);
                var index = GetCryptographicRandomNumber(0, pwdLength - 1);
                while (pwdBuffer[index] != default(char))
                {
                    index = GetCryptographicRandomNumber(0, pwdLength - 1);
                }
                pwdBuffer[index] = symbol;
            }

            if (_hasLower) //then enforce at least one lowercase
            {
                var lower = GetRandomCharacter(lowerCaseCharacters);
                var index = GetCryptographicRandomNumber(0, pwdLength - 1);
                while (pwdBuffer[index] != default(char))
                {
                    index = GetCryptographicRandomNumber(0, pwdLength - 1);
                }
                pwdBuffer[index] = lower;
            }

            // Generate random characters
            // Initial dummy character flag
            var lastCharacter = '\n';

            var characterOptions = string.Join("", lowerCaseCharacters,
                                               upperCaseCharacters,
                                               digitCharacters,
                                               specialCharacters);

            for (var i = 0; i < pwdLength; i++)
            {
                if (pwdBuffer[i] != default(char))
                    continue;

                var nextCharacter = GetRandomCharacter(characterOptions);
                if (_noConsecutive)
                {
                    while (lastCharacter == nextCharacter)
                    {
                        nextCharacter = GetRandomCharacter(characterOptions);
                    }
                }

                if (_noRepeating)
                {
                    var temp = pwdBuffer.ToString();
                    var duplicateIndex = temp!.IndexOf(nextCharacter);
                    while (duplicateIndex != -1)
                    {
                        nextCharacter = GetRandomCharacter(characterOptions);
                        duplicateIndex = temp.IndexOf(nextCharacter);
                    }
                }

                pwdBuffer[i] = nextCharacter;
                lastCharacter = nextCharacter;
            }

            return new string(pwdBuffer);
        }

        private int GetCryptographicRandomNumber(int lBound, int uBound)
        {
            // Assumes lBound >= 0 && lBound < uBound
            // returns an int >= lBound and < uBound
            uint uRandomNumber;
            var randomNumber = new byte[4];
            if (lBound == uBound - 1)
            {
                // test for degenerate case where only lBound can be returned
                return lBound;
            }

            var excludeRndBase = uint.MaxValue - uint.MaxValue % (uint)(uBound - lBound);

            do
            {
                _rng.GetBytes(randomNumber);
                uRandomNumber = BitConverter.ToUInt32(randomNumber, 0);
            } while (uRandomNumber >= excludeRndBase);

            return (int)(uRandomNumber % (uBound - lBound)) + lBound;
        }

        private char GetRandomCharacter(string characterArray)
        {
            var charArray = characterArray.ToCharArray();
            var upperBound = charArray.GetUpperBound(0);

            var randomCharPosition = GetCryptographicRandomNumber(charArray.GetLowerBound(0), upperBound);

            var randomChar = charArray[randomCharPosition];
            if (string.IsNullOrEmpty(_exclusionSet))
                return randomChar;

            while (_exclusionSet.IndexOf(randomChar) != -1)
            {
                randomCharPosition = GetCryptographicRandomNumber(charArray.GetLowerBound(0), upperBound);
                randomChar = charArray[randomCharPosition];
            }

            return randomChar;
        }

        /// <summary>
        /// Generates a random password with the specified options
        /// </summary>
        /// <param name="options">Flags to indicate the rules for generating the password</param>
        /// <returns>A random string matching the specified options</returns>
        public static string GeneratePassword(PasswordOptions options)
        {
            var service = new PasswordService(options);
            return service.Generate();
        }

        /// <summary>
        /// Generates a random password matching the specified password policy
        /// </summary>
        /// <param name="policy">The <see cref="PasswordPolicy"/> to guide the password creation</param>
        /// <returns>A random password string</returns>
        public static string GeneratePassword(PasswordPolicy policy)
        {
            var options = PasswordOptions.HasLower | PasswordOptions.NoConsecutive | PasswordOptions.NoRepeating;
            if (policy.MinimumDigits > 0) options |= PasswordOptions.HasDigits;
            if (policy.MinimumSpecialChars > 0) options |= PasswordOptions.HasSymbols;
            if (policy.MinimumUpperCase > 0) options |= PasswordOptions.HasCapitals;

            var service = new PasswordService(options)
            {
                Minimum = policy.MinimumLength,
                Maximum = policy.MaximumLength
            };
            var generated = service.Generate();
            var matchesPolicy = policy.ValidateAsync(generated).Result;

            while (!matchesPolicy.Succeeded)
            {
                generated = service.Generate();
                matchesPolicy = policy.ValidateAsync(generated).Result;
            }

            return generated;
        }
    }
}
