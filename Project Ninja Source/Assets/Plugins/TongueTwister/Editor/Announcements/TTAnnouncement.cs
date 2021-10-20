using System;
using System.Collections.Generic;
using UnityEngine;

namespace TongueTwister.Editor.Announcements
{
    /// <summary>
    /// Used for displaying TongueTwister related announcements.
    /// </summary>
    [Serializable]
    internal class TTAnnouncement : ISerializationCallbackReceiver
    {
        public int id = -1;
        public string title = "Announcement";
        public string body = "";
        [NonSerialized] public DateTime dateTime;
        [SerializeField] private string date;
        public List<TTAnnouncementLink> links = new List<TTAnnouncementLink>();

        public bool HasLinks() => links != null && links.Count > 0;

        public string dateText => date ?? dateTime.ToString("d");
        
        public void OnBeforeSerialize()
        {
            date = dateTime.ToString("d");
        }

        public void OnAfterDeserialize()
        {
            try
            {
                dateTime = DateTime.Parse(date);
            }
            catch
            {
                dateTime = DateTime.Today;
            }
        }
    }
}