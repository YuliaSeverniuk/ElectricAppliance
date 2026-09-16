using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Xml.Serialization;

namespace ElectricalAppliances
{
    class ApplianceService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };

        public void SerializeToXml(
            string fileName,
            ElectricalAppliance[] appliances)
        {
            var serializer = new XmlSerializer(typeof(ElectricalAppliance[]));

            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                serializer.Serialize(writer, appliances);
            }
        }

        public ElectricalAppliance[] DeserializeFromXml(string fileName)
        {
            var serializer = new XmlSerializer(typeof(ElectricalAppliance[]));

            using (var reader = new StreamReader(fileName, Encoding.UTF8))
            {
                return (ElectricalAppliance[])serializer.Deserialize(reader)!;
            }
        }

        public void SerializeToJson(
            string fileName,
            ElectricalAppliance[] appliances)
        {
            string json = JsonSerializer.Serialize(appliances, JsonOptions);
            File.WriteAllText(fileName, json, Encoding.UTF8);
        }

        public ElectricalAppliance[] DeserializeFromJson(string fileName)
        {
            string json = File.ReadAllText(fileName, Encoding.UTF8);
            return JsonSerializer.Deserialize<ElectricalAppliance[]>(json, JsonOptions)!;
        }

        public ElectricalAppliance[] ReadFromFile(string fileName)
        {
            var appliances = new List<ElectricalAppliance>();

            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] data = line.Split(';');

                string type = data[0];
                string name = data[1];
                string company = data[2];
                decimal price = decimal.Parse(
                    data[3],
                    CultureInfo.InvariantCulture);

                switch (type)
                {
                    case "Порохотяг":
                        appliances.Add(
                            new VacuumCleaner(
                                name,
                                company,
                                price,
                                int.Parse(data[4]),
                                data[5]));
                        break;

                    case "Пральна машина":
                        appliances.Add(
                            new WashingMachine(
                                name,
                                company,
                                price,
                                int.Parse(data[4]),
                                double.Parse(
                                    data[5],
                                    CultureInfo.InvariantCulture)));
                        break;

                    case "Комбайн":
                        appliances.Add(
                            new FoodProcessor(
                                name,
                                company,
                                price,
                                int.Parse(data[4]),
                                int.Parse(data[5])));
                        break;

                    default:
                        throw new ArgumentException(
                            $"Невідомий тип електроприладу: {type}");
                }
            }

            return appliances.ToArray();
        }

        public void WriteFile1(
            string fileName,
            ElectricalAppliance[] appliances)
        {
            var sortedAppliances = appliances
                .OrderBy(a => a.Name)
                .ToArray();

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(
                    "Електроприлади, посортовані за назвою");

                writer.WriteLine(new string('-', 80));

                foreach (var appliance in sortedAppliances)
                {
                    writer.WriteLine(appliance.GetInfo());
                }

                writer.WriteLine();
                writer.WriteLine(
                    "Кількість електроприладів:");

                writer.WriteLine(new string('-', 50));

                var groups = appliances
                    .GroupBy(a => a.Name)
                    .OrderBy(g => g.Key);

                foreach (var group in groups)
                {
                    writer.WriteLine(
                        $"{group.Key}: {group.Count()}");
                }
            }
        }

        public void WriteFile2(
            string fileName,
            ElectricalAppliance[] appliances,
            string company)
        {
            var companyAppliances = appliances
                .Where(a => a.Company.Equals(
                    company,
                    StringComparison.OrdinalIgnoreCase))
                .ToArray();

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(
                    $"Електроприлади фірми: {company}");

                writer.WriteLine(new string('-', 50));

                if (companyAppliances.Length == 0)
                {
                    writer.WriteLine(
                        "Приладів цієї фірми не знайдено.");

                    return;
                }

                foreach (var appliance in companyAppliances)
                {
                    writer.WriteLine(
                        $"{appliance.Name} - {appliance.Price:F2}");
                }

                decimal totalCost =
                    companyAppliances.Sum(a => a.Price);

                writer.WriteLine();
                writer.WriteLine(
                    $"Загальна вартість: {totalCost:F2}");
            }
        }
    }
}