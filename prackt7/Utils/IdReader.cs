using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace prackt7
{

    public class IdReader
    {
        static public List<int> docsId = new List<int>();
        static public List<int> pacientsId = new List<int>();

        static public void read()
        {
            docsId.Clear();
            pacientsId.Clear();
            try
            {
                string directoryPath = @"C:\Users\Neven\Desktop\Ucheba\TRPO\prackt7\prackt7\bin\Debug\net8.0-windows";
                string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json");

                foreach (string filePath in jsonFiles)
                {
                    string[] strings = filePath.Split('.');
                    string[] strings2 = strings[1].Split('-');
                    string[] strings3 = strings2[1].Split(@"\");
                    string[] strings4 = strings3[1].Split("_");

                    if (strings4[0] == "D")
                    {
                        try
                        {
                            if (int.TryParse(strings4[1], out int id))
                            {
                                docsId.Add(id);
                            }
                        }
                        catch
                        {

                        }

                    }

                    else if (strings4[0] == "P")
                    {
                        try
                        {
                            if (int.TryParse(strings4[1], out int id))
                            {
                                pacientsId.Add(id);
                            }
                        }
                        catch
                        {

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }

        }
    }
}
