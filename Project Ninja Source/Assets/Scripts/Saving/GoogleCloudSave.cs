//using UnityEngine;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using GooglePlayGames.BasicApi.SavedGame;
//using System.Text;
//using System;
//using UnityEngine.Purchasing.MiniJSON;

//public static class GoogleCloudSave<T>
//{
//    private static bool _isSaving = false;
//    private const string _saveName = "Play_Games_Services_Test";

//    private static bool Authenticated => PlayGamesPlatform.Instance.IsAuthenticated();
//    private static ISavedGameClient SavedGame => PlayGamesPlatform.Instance.SavedGame;
//    private static Action<T> _loadValueCallback;

//    public static bool SaveToCloud( object obj )
//    {
//        if(Authenticated)
//        {
//            _isSaving = true;
//            SavedGame.OpenWithAutomaticConflictResolution(_saveName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, ( status, data ) =>
//            {
//                OnDataReadyToProcessCallback(status, data, obj);
//            });
//        }

//        return Authenticated;
//    }

//    public static bool LoadFromCloud( Action<T> callback )
//    {
//        _loadValueCallback = callback;
//        _isSaving = false;

//        if(Authenticated /*|| true*/)
//        {
//            SavedGame.OpenWithAutomaticConflictResolution(_saveName, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, ( status, data ) =>
//            {
//                OnDataReadyToProcessCallback(status, data);
//            });
//        }

//        return Authenticated;
//    }

//    private static void OnDataReadyToProcessCallback( SavedGameRequestStatus status, ISavedGameMetadata metadata, object obj = null )
//    {
//        if(status == SavedGameRequestStatus.Success /*|| true*/)
//        {
//            if(_isSaving)
//            {
//                if(obj == null)
//                {
//                    Debug.LogError("No object passed to be saved.");
//                }

//                var data = obj.ConvertToByteArray();
//                var updatedMetadata = new SavedGameMetadataUpdate.Builder().Build();
//                SavedGame.CommitUpdate(metadata, updatedMetadata, data, ( reqStatus, mdata ) =>
//                {
//                    if(reqStatus == SavedGameRequestStatus.Success)
//                        Debug.Log($"Data Successfully Saved: {mdata.Description}");
//                    else
//                        Debug.LogWarning($"Error Saving Data: {reqStatus} :: {mdata.Description}");
//                });
//            }
//            else
//            {
//                SavedGame.ReadBinaryData(metadata, ( reqStatus, mdata ) =>
//                {
//                    if(reqStatus == SavedGameRequestStatus.Success)
//                    {
//                        var data = mdata.ConvertFromByteArray<T>();
//                        _loadValueCallback?.Invoke(data);
//                    }
//                    else
//                        Debug.LogWarning($"Data Reading Failed! :: {reqStatus}");
//                });
//            }
//        }
//        else
//        {
//            Debug.LogWarning($"Error Processing Save File: {status}");
//        }
//    }
//}

//public static class ConvertExtention
//{
//    public static byte[] ConvertToByteArray( this object obj ) => Encoding.UTF8.GetBytes(Json.Serialize(obj));

//    public static T ConvertFromByteArray<T>( this byte[] arr ) => (T)Json.Deserialize(Encoding.UTF8.GetString(arr));
//}
