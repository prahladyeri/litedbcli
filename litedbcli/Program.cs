/*
 * Program.cs - Main entry point and other utilities
 * 
 * @author Prahlad Yeri <prahladyeri@yahoo.com>
 * @license MIT
 */
using LiteDB;
using System;
using System.Linq;

namespace litedbcli
{
    class Program
    {
        private static string _connstr = "";

        static void Perform(string cmd)
        {
            using (LiteDatabase db = new LiteDatabase(_connstr)) 
            {
                if (cmd.Length == 0) {
                    return; // testing the connection
                }
                else if (cmd.Equals("SHOW TABLES", StringComparison.OrdinalIgnoreCase) ||
                    cmd.Equals("LIST COLLECTIONS", StringComparison.OrdinalIgnoreCase))
                {
                    var collections = db.GetCollectionNames(); // Get all collections
                    foreach (var col in collections)
                    {
                        Console.WriteLine(col);
                    }
                    Console.WriteLine($"Total collections: {collections.Count()}");
                    return; // Skip execution of db.Execute(cmd)
                }
                var reader = db.Execute(cmd);
                int affectedRows = 0;
                bool headerPrinted = false;
                //SELECT name FROM sys.collections;
                while (reader.Read()) // Iterating over IBsonDataReader
                {
                    if (cmd.StartsWith("select", StringComparison.OrdinalIgnoreCase))
                    {  //print row
                        var document = reader.Current.AsDocument; // Get the current document
                        if (!headerPrinted)
                        {
                            Console.WriteLine(string.Join("\t", document.Keys)); // Print column names as a header row
                            Console.WriteLine(new string('-', 50)); // Separator line
                            headerPrinted = true;
                        }
                        Console.WriteLine(string.Join("\t", document.Values));
                        Console.WriteLine("-----------");
                    }
                    affectedRows++;
                }
                Console.WriteLine("Total records affected: " + affectedRows);
            }
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: litedbcli <database-file>");
                return;
            }
            var fname = args[0];
            var password = "";
            if (args.Length >= 2) {
                password = args[1];
            }
            try {
                _connstr =  password.Length==0 ? fname : $"Filename=\"{fname}\"; password={password}; ;Connection=shared" ;
                //Console.WriteLine(conn);
                //var db = new LiteDatabase(conn);
                Perform(""); // test the connection
                while (true) 
                {
                    Console.Write("> ");
                    var cmd = Console.ReadLine()?.Trim();
                    if (cmd == "exit" || cmd == "quit") break;
                    if (string.IsNullOrWhiteSpace(cmd)) continue; // Ignore empty input
                    try {
                        Perform(cmd);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error running command: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }
    }
}
