namespace StackOverflow.NewFolder
{
    public static class StringExtension
    {
        public static string Max60(this string str) =>
            (str.Length > 60) ? str.Substring(0, 60) : str;
        
    }
}
