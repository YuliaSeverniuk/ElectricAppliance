namespace ElectricalAppliances
{
    public class VacuumCleaner : ElectricalAppliance
    {
        public int Power { get; set; }
        public string Color { get; set; }

        public VacuumCleaner()
        {
            Color = string.Empty;
        }

        public VacuumCleaner(
            string name,
            string company,
            decimal price,
            int power,
            string color)
            : base(name, company, price)
        {
            Power = power;
            Color = color;
        }

        public override string GetInfo()
        {
            return $"Порохотяг | Назва: {Name} | Фірма: {Company} | " +
                   $"Ціна: {Price} | Потужність: {Power} Вт | Колір: {Color}";
        }
    }
}