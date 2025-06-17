
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



    public class ApexChartLineChartModel
    {
        public List<DataPoint> SeriesData { get; set; }

        public class DataPoint
        {
            public string Date { get; set; } // Format: "yyyy-MM-dd"
            public double Value { get; set; }
        }
    }

    public class ApexChartReadialBarChartModel
    {
        public int[] series;
    }

}


