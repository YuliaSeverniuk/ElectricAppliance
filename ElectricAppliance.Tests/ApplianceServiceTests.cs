using ElectricalAppliances;

namespace ElectricAppliance.Tests
{
    [TestClass]
    public class ApplianceServiceTests
    {
        [TestMethod]
        public void ReadFromFile_ValidLine_ParsesVacuumCleaner()
        {
            string path = Path.GetTempFileName();
            File.WriteAllText(path, "Порохотяг;Turbo3000;Samsung;4500;1200;Чорний");
            var service = new ApplianceService();

            var appliances = service.ReadFromFile(path);

            Assert.HasCount(1, appliances);
            var vacuum = (VacuumCleaner)appliances[0];
            Assert.AreEqual("Turbo3000", vacuum.Name);
            Assert.AreEqual("Samsung", vacuum.Company);
            Assert.AreEqual(4500m, vacuum.Price);
            Assert.AreEqual(1200, vacuum.Power);
            Assert.AreEqual("Чорний", vacuum.Color);

            File.Delete(path);
        }

        [TestMethod]
        public void ReadFromFile_UnknownType_ThrowsArgumentException()
        {
            string path = Path.GetTempFileName();
            File.WriteAllText(path, "Невідомий;Turbo3000;Samsung;4500");
            var service = new ApplianceService();

            Assert.ThrowsExactly<ArgumentException>(() => service.ReadFromFile(path));

            File.Delete(path);
        }

        [TestMethod]
        public void XmlRoundTrip_PreservesApplianceData()
        {
            string path = Path.GetTempFileName();
            var service = new ApplianceService();
            ElectricalAppliance[] appliances =
            {
                new WashingMachine("Aqua", "LG", 15000m, 12, 8.0),
            };

            service.SerializeToXml(path, appliances);
            var result = service.DeserializeFromXml(path);

            Assert.HasCount(1, result);
            var machine = (WashingMachine)result[0];
            Assert.AreEqual("Aqua", machine.Name);
            Assert.AreEqual(12, machine.ProgramCount);
            Assert.AreEqual(8.0, machine.Volume);

            File.Delete(path);
        }

        [TestMethod]
        public void JsonRoundTrip_PreservesApplianceData()
        {
            string path = Path.GetTempFileName();
            var service = new ApplianceService();
            ElectricalAppliance[] appliances =
            {
                new FoodProcessor("Chef", "Bosch", 8500m, 1000, 10),
            };

            service.SerializeToJson(path, appliances);
            var result = service.DeserializeFromJson(path);

            Assert.HasCount(1, result);
            var processor = (FoodProcessor)result[0];
            Assert.AreEqual("Chef", processor.Name);
            Assert.AreEqual(1000, processor.Power);
            Assert.AreEqual(10, processor.FunctionCount);

            File.Delete(path);
        }
    }
}
