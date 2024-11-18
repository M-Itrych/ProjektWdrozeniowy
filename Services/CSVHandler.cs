using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Diagnostics;

public class CSVHandler
{
    public static void ToJson()
    {
        string tempPath = Path.Combine(Directory.GetCurrentDirectory(), "temp");
        string[] csvFilePaths = Directory.GetFiles(tempPath);

        var countryDict = new Dictionary<string, List<dynamic>>();
        string safetyIndexKey = "MostDangerousCountriesForWomen_WomenPeaceAndSecurityIndex_Score_2023";

        foreach (string filePath in csvFilePaths)
        {
            var fileRecords = ReadFile(filePath);
            if (fileRecords.Any())
            {
                var firstRecord = fileRecords.First();
                string countryKey = firstRecord.ContainsKey("Country") ? "Country" : "country";

                foreach (var record in fileRecords)
                {
                    string? country = record.ContainsKey(countryKey) ? record[countryKey] : null;
                    if (country != null)
                    {
                        if (!countryDict.ContainsKey(country))
                        {
                            countryDict[country] = new List<dynamic>();
                        }
                        countryDict[country].Add(record);
                    }
                }
            }
            File.Delete(filePath);
        }

        var mergedData = new List<dynamic>();
        foreach (var country in countryDict)
        {
            var countryInfo = country.Value.FirstOrDefault(x => x.ContainsKey("Country"));
            var safetyIndex = country.Value.FirstOrDefault(x => x.ContainsKey(safetyIndexKey));

            if (safetyIndex != null && !string.IsNullOrEmpty(safetyIndex[safetyIndexKey]?.ToString()))
            {
                var mergedRecord = new
                {
                    Country = country.Key,
                    Continent = countryInfo?["Continent"] ?? string.Empty,
                    Population = countryInfo?["Population"] ?? string.Empty,
                    IMF_GDP = countryInfo?["IMF_GDP"] ?? string.Empty,
                    UN_GDP = countryInfo?["UN_GDP"] ?? string.Empty,
                    GDP_per_capita = countryInfo?["GDP_per_capita"] ?? string.Empty,
                    WomenPeaceAndSecurityIndex_Score_2023 = safetyIndex?[safetyIndexKey] ?? string.Empty,
                    WomensDangerIndexWDI_TotalScore_2019 = safetyIndex?["MostDangerousCountriesForWomen_WomensDangerIndexWDI_TotalScore_2019"] ?? string.Empty,
                    WDIStreetSafety_2019 = safetyIndex?["MostDangerousCountriesForWomen_WDIStreetSafety_2019"] ?? string.Empty,
                    WDIIntentionalHomicide_2019 = safetyIndex?["MostDangerousCountriesForWomen_WDIIntentionalHomicide_2019"] ?? string.Empty,
                    WDINonPartnerViolence_2019 = safetyIndex?["MostDangerousCountriesForWomen_WDINonPartnerViolence_2019"] ?? string.Empty,
                    WDIIntimatePartnerViolence_2019 = safetyIndex?["MostDangerousCountriesForWomen_WDIIntimatePartnerViolence_2019"] ?? string.Empty,
                    WDILegalDiscrimination_2019 = safetyIndex?["MostDangerousCountriesForWomen_WDILegalDiscrimination_2019"] ?? string.Empty,
                    WDIGlobalGenderGap_2019 = safetyIndex?["MostDangerousCountriesForWomen_WDIGlobalGenderGap_2019"] ?? string.Empty,
                    WDIGenderInequality_2019 = safetyIndex?["MostDangerousCountriesForWomen_WDIGenderInequality_2019"] ?? string.Empty,
                    WDIAttitudesTowardViolence_2019 = safetyIndex?["MostDangerousCountriesForWomen_WDIAttitudesTowardViolence_2019"] ?? string.Empty
                };
                mergedData.Add(mergedRecord);
            }
        }

        string outputPath = Path.Combine(tempPath, "data.json");
        var json = JsonConvert.SerializeObject(mergedData, Formatting.Indented);
        File.WriteAllText(outputPath, json);

        Debug.WriteLine("Merged data saved to JSON at " + outputPath);
    }

    private static List<Dictionary<string, string>> ReadFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        });

        var records = csv.GetRecords<dynamic>().ToList();
        return records.Select(record =>
            ((IDictionary<string, object>)record)
                .ToDictionary(pair => pair.Key, pair => pair.Value?.ToString() ?? string.Empty)
        ).ToList();
    }
}
