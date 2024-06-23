using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using KeertanPothi.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(SaveFile))]

namespace KeertanPothi.iOS
{
    public class SaveFile : ISaveFile
    {
        string externalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        void ISaveFile.SaveFile(string fileName, string json, bool autoSave)
        {
            string folderName = "KeertanPothi";
            string folderPath = System.IO.Path.Combine(externalPath.ToString(), folderName);
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            if (!dir.Exists)
                dir.Create();

            var filePath = System.IO.Path.Combine(folderPath, fileName);
            System.IO.File.WriteAllText(filePath, json);
        }

        List<string> ISaveFile.ReadFile(bool autoSave)
        {
            string folderName = "KeertanPothi";
            string folderPath = System.IO.Path.Combine(externalPath.ToString(), folderName);
            DirectoryInfo dir = new DirectoryInfo(folderPath);
            if (!dir.Exists)
            {
                return null;
            }
            else
            {
                FileInfo[] files = dir.GetFiles("*.json"); ;
                if (files.Length > 0)
                {
                    List<string> jsons = new List<string>();
                    foreach (FileInfo file in files)
                    {
                        FileStream stream = file.OpenRead();
                        string js = string.Empty;
                        using (StreamReader read = new StreamReader(stream))
                        {
                            js = read.ReadToEnd();
                        }
                        jsons.Add(js);
                    }
                    return jsons;
                }
                else
                    return null;
            }
        }
    }
}
