using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ElectricalAppliances
{
    [XmlInclude(typeof(VacuumCleaner))]
    [XmlInclude(typeof(WashingMachine))]
    [XmlInclude(typeof(FoodProcessor))]
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "Type")]
    [JsonDerivedType(typeof(VacuumCleaner), "VacuumCleaner")]
    [JsonDerivedType(typeof(WashingMachine), "WashingMachine")]
    [JsonDerivedType(typeof(FoodProcessor), "FoodProcessor")]
    public abstract class ElectricalAppliance
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }

        protected ElectricalAppliance()
        {
            Name = string.Empty;
            Company = string.Empty;
        }

        protected ElectricalAppliance(
            string name,
            string company,
            decimal price)
        {
            Name = name;
            Company = company;
            Price = price;
        }

        public abstract string GetInfo();
    }
}