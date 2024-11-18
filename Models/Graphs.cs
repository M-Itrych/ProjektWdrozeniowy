using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottPlot;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

class Graphs
{
    static void CreateImgGraph()
    {
        string filePath = Directory.GetCurrentDirectory() + @"\temp\data.json";
        string jsonContent = File.ReadAllText(filePath);
        var jsonData = JsonConvert.DeserializeObject<CountryInfo>(jsonContent);
        

        
        
        ScottPlot.Plot plot = new();
        plot.SavePng("graph.png", 400, 400);
        Debug.WriteLine("Zapisano graf :)");
    }

}