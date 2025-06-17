
namespace CKMSDotNetTraining.ChartWebApp.Models
{

    public class ApexChartSeriesModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int[] Data { get; set; } // Must not be null!
    }

    public class ApexChartMixedChartModel
    {
        public List<ApexChartSeriesModel> Series { get; set; }
        public string[] Labels { get; set; }


    }


}


