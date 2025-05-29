// See https://aka.ms/new-console-template for more information
using CKMSDotNetTraining.ConsoleApp;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
//Console.ReadLine();

// md = markdown
ADODotNetExample adodotnet = new ADODotNetExample();
//adodotnet.Read();
//adodotnet.Create();
//adodotnet.Edit();
//adodotnet.Update();
//adodotnet.Delete();

DapperExample dapper = new DapperExample();
//dapper.Read();
//dapper.Create("Dapper Title", "Dapper Author", "Dapper Content");
//dapper.Edit(4);
//dapper.Update(3, "Dapper UTitle", "Dapper UAuthor", "Dapper UContent");
//dapper.Delete(10);

EFCoreExample ef = new EFCoreExample();
//ef.Read();
//ef.Create("Ef title", "Ef Author", "Ef Content");
//ef.Edit(5);
//ef.Update(5, "Ef UTitle", "Ef UAuthor", "Ef UContent");
//ef.Delete(9);

AdoDotNetExample2 ado = new AdoDotNetExample2();
//ado.Read();
//ado.Create();
//ado.Edit();
//ado.Update();
//ado.Delete(); 

DapperExample2 dapper1 = new DapperExample2();
//dapper1.Read();
//dapper1.Create("Dapper2 Title", "Dapper2 Author", "Dapper2 Content");
//dapper.Edit(1019);
//dapper1.Update(1019, "Dapper2 UTitle", "Dapper2 UAuthor", "Dapper2 UContent");
dapper1.Delete(1019);
Console.ReadKey();
