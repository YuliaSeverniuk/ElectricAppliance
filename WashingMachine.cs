namespace ElectricalAppliances
{
    public class WashingMachine : ElectricalAppliance
    {
        public int ProgramCount { get; set; }
        public double Volume { get; set; }

        public WashingMachine()
        {
        }

        public WashingMachine(
            string name,
            string company,
            decimal price,
            int programCount,
            double volume)
            : base(name, company, price)
        {
            ProgramCount = programCount;
            Volume = volume;
        }

        public override string GetInfo()
        {
            return $"Пральна машина | Назва: {Name} | Фірма: {Company} | " +
                   $"Ціна: {Price} | Програм: {ProgramCount} | Об'єм: {Volume}";
        }
    }
}