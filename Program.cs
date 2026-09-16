using System.Reflection;
using log4net;
using log4net.Config;

namespace ElectricalAppliances
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        static void Main()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            try
            {
                Run();
            }
            catch (Exception ex)
            {
                Log.Error("Помилка під час виконання програми.", ex);

                Console.WriteLine();
                Console.WriteLine($"Сталася помилка: {ex.Message}");
                Console.WriteLine("Детальну інформацію записано у файл logs/errors.log.");
            }
        }

        static void Run()
        {
            const string inputFile = "input.txt";
            const string file1 = "File1.txt";
            const string file2 = "File2.txt";
            const string xmlFile = "appliances.xml";
            const string jsonFile = "appliances.json";

            ApplianceService service = new ApplianceService();
            UserUI userUI = new UserUI();

            ElectricalAppliance[] appliances =
                service.ReadFromFile(inputFile);

            service.WriteFile1(
                file1,
                appliances);

            string company = userUI.ReadCompanyName();

            service.WriteFile2(
                file2,
                appliances,
                company);

            service.SerializeToXml(xmlFile, appliances);
            service.SerializeToJson(jsonFile, appliances);

            ElectricalAppliance[] fromXml =
                service.DeserializeFromXml(xmlFile);
            ElectricalAppliance[] fromJson =
                service.DeserializeFromJson(jsonFile);

            Console.WriteLine();
            Console.WriteLine("Дані успішно оброблено.");
            Console.WriteLine($"Створено файл: {file1}");
            Console.WriteLine($"Створено файл: {file2}");
            Console.WriteLine($"Створено файл: {xmlFile}");
            Console.WriteLine($"Створено файл: {jsonFile}");

            Console.WriteLine();
            Console.WriteLine("Прилади, десеріалізовані з XML:");
            foreach (var appliance in fromXml)
            {
                Console.WriteLine(appliance.GetInfo());
            }

            Console.WriteLine();
            Console.WriteLine("Прилади, десеріалізовані з JSON:");
            foreach (var appliance in fromJson)
            {
                Console.WriteLine(appliance.GetInfo());
            }
        }
    }
}