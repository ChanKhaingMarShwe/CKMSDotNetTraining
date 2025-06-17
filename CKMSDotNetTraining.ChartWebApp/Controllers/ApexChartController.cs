using CKMSDotNetTraining.ChartWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CKMSDotNetTraining.ChartWebApp.Controllers
{
    public class ApexChartController : Controller
    {
        public IActionResult PieChart()
        {
            ApexChartPieChartModel model = new ApexChartPieChartModel
            {
                Series = new int[] { 44, 55, 13, 43, 22 },
                Labels = new string[] { "Team A", "Team B", "Team C", "Team D", "Team E" }
            };
            return View(model);
        }

        public IActionResult MixedChart()
        {
            var model = new ApexChartMixedChartModel
            {
                Series = new List<ApexChartSeriesModel>
        {
            new ApexChartSeriesModel
            {
                Name = "TEAM A",
                Type = "column",
                Data = new int[] { 23, 11, 22, 27, 13, 22, 37, 21, 44, 22, 30 }
            },
            new ApexChartSeriesModel
            {
                Name = "TEAM B",
                Type = "area",
                Data = new int[] { 44, 55, 41, 67, 22, 43, 21, 41, 56, 27, 43 }
            },
            new ApexChartSeriesModel
            {
                Name = "TEAM C",
                Type = "line",
                Data = new int[] { 30, 25, 36, 30, 45, 35, 64, 52, 59, 36, 39 }
            }
        },
                Labels = new string[] {
            "2003-01-01", "2003-02-01", "2003-03-01", "2003-04-01", "2003-05-01",
            "2003-06-01", "2003-07-01", "2003-08-01", "2003-09-01", "2003-10-01", "2003-11-01"
        }
            };

            return View(model);
        }


        public IActionResult LineChart()
        {
            var model = new ApexChartLineChartModel
            {
                SeriesData = new List<ApexChartLineChartModel.DataPoint>
                {
                    new() { Date = "2003-01-01", Value = 34 },
                new() { Date = "2003-02-01", Value = 44 },
                new() { Date = "2003-03-01", Value = 54 },
                new() { Date = "2003-04-01", Value = 32 },
                new() { Date = "2003-05-01", Value = 45 },
                new() { Date = "2003-06-01", Value = 48 },
                new() { Date = "2003-07-01", Value = 37 },
                new() { Date = "2003-08-01", Value = 41 },
                new() { Date = "2003-09-01", Value = 52 },
                new() { Date = "2003-10-01", Value = 63 },
                new() { Date = "2003-11-01", Value = 72 },
                }
            };
            return View(model);
        }


        public IActionResult RadialBarChart()
        {
            ApexChartReadialBarChartModel model = new ApexChartReadialBarChartModel
            {
                series = new int[] { 76, 67, 61, 90 }
            };
            return View(model);
        }

    }
}
