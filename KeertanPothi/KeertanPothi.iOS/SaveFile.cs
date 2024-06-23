using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using KeertanPothi.iOS;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(SaveFile))]

namespace KeertanPothi.iOS
{
    public class SaveFile : ISaveFile
    {
        string myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string externalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        void ISaveFile.SaveFile(string fileName, string json, bool autoSave)
        {
            string folderName = "KeertanPothi";
            string folderPath = string.Empty;
            //string libPath = Path.Combine(docsPath, "..", "Library");

            if (autoSave)
                folderPath = System.IO.Path.Combine(externalPath, folderName);
            else
                folderPath = System.IO.Path.Combine(myDocs, folderName);


            DirectoryInfo dir = new DirectoryInfo(folderPath);
            if (!dir.Exists)
                dir.Create();

            var filePath = Path.Combine(folderPath, fileName);
            File.WriteAllText(filePath, json);
        }

        List<string> ISaveFile.ReadFile(bool autoSave)
        {
            string folderName = "KeertanPothi";
            string folderPath = string.Empty;

            if (autoSave)
                folderPath = System.IO.Path.Combine(externalPath, folderName);
            else
                folderPath = System.IO.Path.Combine(myDocs, folderName);

            DirectoryInfo dir = new DirectoryInfo(folderPath);
            if (!dir.Exists)
            {
                return null;
            }
            else
            {
                try
                {
                    FileInfo[] files = dir.GetFiles("*.json");
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
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}