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
dapper.Update(3, "Dapper UTitle", "Dapper UAuthor", "Dapper UContent");
Console.ReadKey();
