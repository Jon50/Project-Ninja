using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace TGM.FutureRacingGP.Save
{
    public static class SavingSystem
    {
        public static void SaveValue( string saveFile, object data, string newDirectory = null, bool cloudSave = false )
        {
            //if(cloudSave)
            //{
            //    var authenticated = GoogleCloudSave<object>.SaveToCloud(data);
            //    if(!authenticated)
            //    {
            //        Debug.LogWarning("Google authentication failed! User is not Signed In.");
            //        LocalSave(saveFile, data, newDirectory);
            //    }
            //}
            //else
            LocalSave(saveFile, data, newDirectory);
        }

        private static void LocalSave( string saveFile, object data, string newDirectory = null )
        {
            saveFile = saveFile.GetRandomString('/');
            var savePath = GetSavePath(saveFile);
            var directoryPath = GetSavePath(newDirectory);

            if(!string.IsNullOrEmpty(newDirectory) && !Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath).Attributes = FileAttributes.Hidden;

            using(FileStream stream = File.Open(savePath, FileMode.Create))
            {
                DataContractSerializer dataContract = new DataContractSerializer(data.GetType());
                dataContract.WriteObject(stream, data);
            }
        }

        public static T LoadValue<T>( string saveFile, T defaultValue = default, bool cloudSave = false )
        {
            var value = (T)default;
            //if(cloudSave)
            //{
            //    var authenticated = GoogleCloudSave<T>.LoadFromCloud(( loadedValue ) =>
            //    {
            //        value = loadedValue;
            //    });

            //    if(!authenticated)
            //    {
            //        Debug.LogWarning("Google authentication failed! User is not Signed In.");
            //        value = LocalLoad(saveFile, defaultValue);
            //    }
            //}
            //else
            value = LocalLoad(saveFile, defaultValue);

            return value;
        }

        private static T LocalLoad<T>( string saveFile, T defaultValue )
        {
            saveFile = saveFile.GetRandomString('/');
            var savePath = GetSavePath(saveFile);
            if(!File.Exists(savePath))
                return defaultValue;

            var value = (T)default;

            using(FileStream stream = File.Open(savePath, FileMode.Open))
            {
                DataContractSerializer dataContract = new DataContractSerializer(typeof(T));
                value = (T)dataContract.ReadObject(stream);
            }

            return value;
        }


        public static void DeleteSave( string saveFile )
        {
            if(File.Exists(GetSavePath(saveFile)))
                File.Delete(GetSavePath(saveFile));
        }


        public static void DeleteAllSaves()
        {
            var directory = new DirectoryInfo(Application.persistentDataPath);
            foreach(var file in directory.GetFiles())
                file.Delete();
        }


        public static string GetSavePath( string saveFile ) => Path.Combine(Application.persistentDataPath + "/" + saveFile);
    }
}
