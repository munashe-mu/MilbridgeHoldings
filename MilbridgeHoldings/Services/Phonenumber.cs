namespace ModelLibrary.Services
{
    public class Phonenumber
    {
        public string FormatPhonenumber(string phonenumber)
        {
            if (string.IsNullOrEmpty(phonenumber))
            {
                return "phone number can not be null";
            }

            phonenumber = phonenumber.Trim().Replace("+", "");
            string formattedNumber;
            long trial;
            if (long.TryParse(phonenumber, out trial))
            {
                if ((phonenumber.StartsWith("077") || phonenumber.StartsWith("078") || phonenumber.StartsWith("073") || phonenumber.StartsWith("071")) && phonenumber.Length == 10)
                {
                    formattedNumber = "263" + phonenumber.Substring(1).Trim();
                    return formattedNumber;

                }
                if ((phonenumber.StartsWith("26377") || phonenumber.StartsWith("26378") || phonenumber.StartsWith("26373") || phonenumber.StartsWith("26371")) && phonenumber.Length == 12)
                {
                    formattedNumber = phonenumber.Trim();
                    return formattedNumber;
                }
                if ((phonenumber.StartsWith("0026377") || phonenumber.StartsWith("0026378") || phonenumber.StartsWith("0026373") || phonenumber.StartsWith("0026371")) && phonenumber.Length == 14)
                {
                    formattedNumber = phonenumber.Substring(2).Trim();
                    return formattedNumber;
                }
                if ((phonenumber.StartsWith("77") || phonenumber.StartsWith("78") || phonenumber.StartsWith("73") || phonenumber.StartsWith("71")) && phonenumber.Length == 9)
                {
                    formattedNumber = "263" + phonenumber.Trim();
                    return formattedNumber;
                }
                formattedNumber = "Wrong cellphone number format";
                return formattedNumber;
            }
            formattedNumber = "Wrong cellphone number format";
            return formattedNumber;
        }
    }
}
