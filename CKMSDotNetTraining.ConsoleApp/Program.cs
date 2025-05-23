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
adodotnet.Update();
Console.ReadKey();
