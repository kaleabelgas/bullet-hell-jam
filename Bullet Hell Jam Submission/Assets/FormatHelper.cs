namespace MyUtils
{
    public static class FormatHelper
    {
        public static string FormatNumber(string _numberToFormat, bool _showMil = true, bool _showBil = true)
        {
            string _formattedNumber = string.Format("{0:N0}", double.Parse(_numberToFormat));

            if (_formattedNumber.Length > 11 && _showBil)
            {
                _formattedNumber = $"{_formattedNumber.Substring(0, 3)}B";
            }
            else if (_numberToFormat.Length > 6 && _showMil)
            {
                _formattedNumber = $"{_formattedNumber.Substring(0, 3)}M";
            }

            return _formattedNumber;
        }

        public static string FormatNumber(int _numberToFormat, bool _showMil = true, bool _showBil = true)
        {
            string _formattedNumber = string.Format("{0:N0}", _numberToFormat);

            if (_formattedNumber.Length > 11 && _showBil)
            {
                _formattedNumber = $"{_formattedNumber.Substring(0, 3)}B";
            }
            else if (_formattedNumber.Length > 7 && _showMil)
            {
                _formattedNumber = $"{_formattedNumber.Substring(0, 3)}M";
            }

            return _formattedNumber;
        }
    }

}
