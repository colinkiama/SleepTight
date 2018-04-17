using Sleepy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Sleepy.Helpers
{
    public class FileIOHelper
    {
        #region Fields
        static StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        const string saveFileName = "saveData.slp";
        #endregion

        #region Properties
        public IList<string> loadedSaveData { get; private set; }
        #endregion
        #region Methods
        public async Task<bool> saveSleepDataAsync(ObservableCollection<Sleep> sleepDataToSave)
        {
            bool saveCompleted = false;
            StringBuilder builder = new StringBuilder();
            foreach (var sleep in sleepDataToSave)
            {
                builder.AppendLine(sleep.ToString());
            }

            string saveFileContent = builder.ToString();
            await saveDataToFileAsync(saveFileContent);
            return saveCompleted;
        }

        private async Task saveDataToFileAsync(string saveFileContent)
        {
            var sleepFile = await CreateSaveFileAsync();
            await FileIO.WriteTextAsync(sleepFile, saveFileContent);
        }

        public async Task<IList<string>> loadDataFromFileAsync()
        {
            // read each new line 
            var sleepFile = await GetSaveFileAsync();
            
            return await FileIO.ReadLinesAsync(sleepFile);

        }

        private async Task<StorageFile> GetSaveFileAsync()
        {
            return await localFolder.GetFileAsync(saveFileName);
        }

        private async Task<StorageFile> CreateSaveFileAsync()
        {
            return await localFolder.CreateFileAsync(saveFileName, CreationCollisionOption.ReplaceExisting);

        }
        #endregion

        #region Constructors
        public FileIOHelper()
        {
            
        }
        #endregion
    }
}
    