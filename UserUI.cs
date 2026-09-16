namespace ElectricalAppliances
{
    class UserUI
    {
        public string ReadCompanyName()
        {
            Console.Write("Введіть назву фірми: ");
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
