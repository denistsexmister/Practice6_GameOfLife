using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace Practice6_GameOfLife
{
    internal class SaveManager
    {
        private static string saveFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\gameoflife";

        async public static void SaveFile(string fileName, string savedData)
        {
            if (fileName == null || fileName.Equals(""))
            {
                fileName = "default" + DateTime.Now + ".txt";
            }

            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
            }
            StorageFolder savesFolder = await StorageFolder.GetFolderFromPathAsync(saveFolderPath);
            StorageFile saveFile = await savesFolder.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);

            await FileIO.WriteTextAsync(saveFile, savedData);
        }

        async public static Task<IReadOnlyList<StorageFile>> ReadFileNamesFromSavedFolder()
        {
            if (Directory.Exists(saveFolderPath))
            {
                StorageFolder savesFolder = await StorageFolder.GetFolderFromPathAsync(saveFolderPath);
                IReadOnlyList<StorageFile> files = await savesFolder.GetFilesAsync();
                return files;
            }
            else { return new List<StorageFile>(); }
        }
    }
}
