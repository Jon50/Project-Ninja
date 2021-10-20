using System;
using System.Net;
using UnityEngine;

namespace TongueTwister.Editor.Announcements
{
    internal static class TTAnnouncementService
    {
        public static TTAnnouncementCollection currentAnnouncements;
        
        public static bool isGettingAnnouncements { get; private set; }

        public static bool hasAnnouncementsCollection => currentAnnouncements != null;
        
        public static void UpdateAnnouncements(
            Action<TTAnnouncementCollection> onSuccess = null,
            Action onFail = null, 
            Action onFinally = null)
        {
            if (isGettingAnnouncements)
            {
                return;
            }
            
            UpdateAnnouncementsTask(onSuccess, onFail, onFinally);
        }

        private static async void UpdateAnnouncementsTask(
            Action<TTAnnouncementCollection> onSuccess = null,
            Action onFail = null, 
            Action onFinally = null)
        {
            isGettingAnnouncements = true;
            
            try
            {
                var url = "https://www.austephner.com/assets/static-json/tt-announcements.JSON";
                WebClient client = new WebClient();
                var content = await client.DownloadStringTaskAsync(url);
                var announcementCollection = JsonUtility.FromJson<TTAnnouncementCollection>(content);
                currentAnnouncements = announcementCollection;
                onSuccess?.Invoke(announcementCollection);
            }
            catch
            {
                onFail?.Invoke();
            }
            finally
            {
                onFinally?.Invoke();
            }

            isGettingAnnouncements = false;
        }
    }
}