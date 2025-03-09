public static class NumberFormatter
{
    public static string FormatNumber(int number)
    {
        if (number >= 1_000_000) 
        {
            return (number / 1_000_000f).ToString("F1") + "M"; 
        }
        else if (number >= 1_0000)
        {
            return (number / 1_000f).ToString("F1") + "K";
        }
        else
        {
            return number.ToString(); 
        }
    }
}
