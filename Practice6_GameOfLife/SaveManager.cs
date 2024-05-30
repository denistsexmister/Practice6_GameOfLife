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
        private static StorageFolder myDocs = KnownFolders.DocumentsLibrary;

        async public static void SaveFile(string fileName, string savedData)
        {
            if (fileName == null || fileName.Equals(""))
            {
                fileName = "default" + DateTime.Now;
            }
            fileName += ".txt";

            StorageFolder savesFolder;
            try
            {
                savesFolder = await myDocs.GetFolderAsync("gameoflife");
            } 
            catch (FileNotFoundException)
            {
                savesFolder = await myDocs.CreateFolderAsync("gameoflife");
            }
            StorageFile saveFile = await savesFolder.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);

            await FileIO.WriteTextAsync(saveFile, savedData);
        }

        async public static Task<IReadOnlyList<StorageFile>> ReadFileNamesFromSavedFolder()
        {
            try
            {
                StorageFolder savesFolder = await myDocs.GetFolderAsync("gameoflife");
                IReadOnlyList<StorageFile> files = await savesFolder.GetFilesAsync();
                return files;
            } 
            catch (FileNotFoundException)
            {
                return new List<StorageFile>();
            }
        }

        async public static Task<string> LoadSaveFromFile(string fileName)
        {
            StorageFolder savesFolder = await myDocs.GetFolderAsync("gameoflife");
            StorageFile file = await savesFolder.GetFileAsync(fileName);

            return  await FileIO.ReadTextAsync(file);
        }

        async public static Task CreateDefaultSaves()
        {
            StorageFolder savesFolder;
            try
            {
                savesFolder = await myDocs.GetFolderAsync("gameoflife");
            } 
            catch(FileNotFoundException)
            {
                savesFolder = await myDocs.CreateFolderAsync("gameoflife");
            }

            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder localSavedFields = await installedLocation.GetFolderAsync("SavedFields");

            StorageFile gliderGun = await localSavedFields.GetFileAsync("Glider_Gun.txt");
            StorageFile oscillators = await localSavedFields.GetFileAsync("Oscillators.txt");
            StorageFile spaceships = await localSavedFields.GetFileAsync("Spaceships.txt");
            StorageFile stillLivesBlocks = await localSavedFields.GetFileAsync("Still_Lives_Blocks.txt");

            await gliderGun.CopyAsync(savesFolder);
            await oscillators.CopyAsync(savesFolder);
            await spaceships.CopyAsync(savesFolder);
            await stillLivesBlocks.CopyAsync(savesFolder);
        }
    }
}
