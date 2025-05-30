using CKMSDotNetTraining.Database.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection.Metadata;

namespace CKMSDotNetTraining.MinimalApi
{
    public class JsonStr
    {

        private readonly AppDbContext _db = new AppDbContext();

        public JsonStr()
        {

            var lst = _db.TblBlogs
                  .AsNoTracking()
                  .Where(x => x.DeleteFlag == false)
                  .ToList();

            string jsonStr = JsonConvert.SerializeObject(lst);


            Console.WriteLine(jsonStr);

        }


        
    }
}
