using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;


namespace DB_Creation
{
    class Program
    {
        static void Main(string[] args)
        {
            bool dataIsOk = AreTheConfigurationsOK();

            if (dataIsOk)
            {
                Console.WriteLine("Doriti sa se insereze date de test? (Y/N)");                
                string option = Console.ReadLine().ToString();

                if (option.ToUpper() != "Y" && option.ToUpper() != "N")
                {
                    Console.WriteLine("Optiune invalida. Optiuni valabile: Y sau N");
                    Console.WriteLine(Environment.NewLine);
                    Console.ReadLine();
                }
                else {
                    using (SqlConnection con = new SqlConnection(AppSettings.ConnectionString))
                    {
                        try
                        {
                            con.Open();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Nu s-a putut deschide conexiunea cu baza de date");
                            Console.WriteLine(Environment.NewLine);
                        }

                        try
                        {
                            if (CheckDatabaseExists(con, AppSettings.DatabaseName))
                            {
                                DeleteDatabase(con);
                            }

                            ExecuteDatabaseCreationScripts(con);

                            if (option.ToUpper() == "Y")
                            {
                                InsertTestData(con);
                            }
                        }
                        catch (Exception ex)
                        {
                            con.Close();
                            con.Dispose();
                        }

                        Console.WriteLine("Baza de date a fost creata cu succes!");
                        Console.ReadLine();
                    }
                }
            }
        }

        private static void InsertTestData(SqlConnection con)
        {
            var files = (from file in Directory.EnumerateFiles(AppSettings.TestDataScriptsDirectory, "*.sql", SearchOption.AllDirectories)
                         select file).ToList();

            if (files.Count == 0)
            {
                Console.WriteLine("Nu s-a gasit niciun SQL script pentru inserarea datelor de test. Verificati path-ul in fisierul de configurare");
                Console.WriteLine(Environment.NewLine);
            }
            else
            {
                for (int i = 0; i < files.Count; i++)
                {
                    var script = ReadSqlScript(files[i]);
                    try
                    {
                        if (!string.IsNullOrEmpty(script))
                        {
                            ExecuteScript(con, script);
                            Console.WriteLine("Script executat cu succes :" + files[i]);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Nu s-a putut inserta datele de test din fisierul :" + files[i]);
                        Console.WriteLine(Environment.NewLine);
                    }
                }
            }
        }

        private static void ExecuteDatabaseCreationScripts(SqlConnection con)
        {
            var files = (from file in Directory.EnumerateFiles(AppSettings.DatabaseScriptsDirectory, "*.sql", SearchOption.AllDirectories)
                         select file).ToList();

            var orderFilePath = AppSettings.DatabaseScriptsDirectory + "\\" + "DatabaseScriptOrder.txt";

            if (files.Count == 0)
            {
                Console.WriteLine("Nu s-a gasit niciun SQL script pentru crearea bazei de date. Verificati path-ul in sistemul de configurare");
                Console.WriteLine(Environment.NewLine);
            }
            else
            {
                var orderFileLines = File.ReadAllLines(orderFilePath);
                for (int i = 0; i < orderFileLines.Length; i++)
                {
                    var filePath = AppSettings.DatabaseScriptsDirectory + "\\" + orderFileLines[i];
                    if (!files.Contains(filePath)) {
                        return;
                    }

                    var script = ReadSqlScript(filePath);
                    try
                    {
                        if (!string.IsNullOrEmpty(script))
                        {
                            ExecuteScript(con, script);
                            Console.WriteLine("Script executat cu succes :" + files[i]);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Nu s-a putut executa datele din fisierul :" + files[i]);
                        Console.WriteLine(Environment.NewLine);
                    }
                }
            }
        }

        private static void ExecuteScript(SqlConnection con, string script)
        {
            Server server = new Server(new ServerConnection(con));
            var db = new Database(server, AppSettings.DatabaseName);
            if (!CheckDatabaseExists(con, AppSettings.DatabaseName)) {
                db.Create();
            }
            
            db.ExecuteNonQuery(script);
            //server.ConnectionContext.ExecuteNonQuery(script);
        }

        private static string ReadSqlScript(string file)
        {
            return File.ReadAllText(file);
        }

        private static bool CheckDatabaseExists(SqlConnection tmpConn, string databaseName)
        {
            string sqlCreateDBQuery;
            bool result = false;

            try
            {
                sqlCreateDBQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", databaseName);

                using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, tmpConn))
                {
                    object resultObj = sqlCmd.ExecuteScalar();

                    int databaseID = 0;

                    if (resultObj != null)
                    {
                        int.TryParse(resultObj.ToString(), out databaseID);
                    }
                    result = (databaseID > 0);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        private static void DeleteDatabase(SqlConnection con)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("use master; drop database " + AppSettings.DatabaseName, con))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Baza de date a fost stearsa.");
                    Console.WriteLine(Environment.NewLine);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Nu s-a putut sterge baza de date existenta");
                Console.WriteLine(Environment.NewLine);
            }
        }

        private static bool AreTheConfigurationsOK()
        {
            bool dataIsOk = true;
            if (string.IsNullOrEmpty(AppSettings.ConnectionString))
            {
                Console.WriteLine("Lipsesc datele de conexiune la SQL Server. Verificati fisierul de configurare");
                Console.WriteLine(Environment.NewLine);
                dataIsOk = false;
            }

            if (string.IsNullOrEmpty(AppSettings.DatabaseScriptsDirectory))
            {
                Console.WriteLine("Lipseste locatia scriptului pentru creeare bazei de date. Verificati fisierul de configurare");
                Console.WriteLine(Environment.NewLine);
                dataIsOk = false;
            }

            if (string.IsNullOrEmpty(AppSettings.TestDataScriptsDirectory))
            {
                Console.WriteLine("Lipseste locatia directorului pentru inserarea datelor de test. Verificati fisierul de configurare");
                Console.WriteLine(Environment.NewLine);
                dataIsOk = false;
            }

            return dataIsOk;
        }
    }
}
