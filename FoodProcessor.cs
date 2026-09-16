namespace ElectricalAppliances
{
    public class FoodProcessor : ElectricalAppliance
    {
        public int Power { get; set; }
        public int FunctionCount { get; set; }

        public FoodProcessor()
        {
        }

        public FoodProcessor(
            string name,
            string company,
            decimal price,
            int power,
            int functionCount)
            : base(name, company, price)
        {
            Power = power;
            FunctionCount = functionCount;
        }

        public override string GetInfo()
        {
            return $"Комбайн | Назва: {Name} | Фірма: {Company} | " +
                   $"Ціна: {Price} | Потужність: {Power} Вт | " +
                   $"Функцій: {FunctionCount}";
        }
    }
}