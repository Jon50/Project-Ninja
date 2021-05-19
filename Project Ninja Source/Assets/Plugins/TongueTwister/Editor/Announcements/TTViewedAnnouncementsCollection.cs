using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TongueTwister.Editor.Announcements
{
    /// <summary>
    /// Stores a list of IDs that represent viewed attachments.
    /// </summary>
    [Serializable]
    internal class TTViewedAnnouncementsCollection
    {
        private const string PLAYER_PREF_KEY = "TT:ViewedAnnouncements";

        private static TTViewedAnnouncementsCollection _current;

        public static TTViewedAnnouncementsCollection current
        {
            get
            {
                if (_current == null)
                {
                    _current = Create();
                }

                return _current;
            }
        }
        
        public List<int> ids = new List<int>();

        public bool IsAnnouncementRead(TTAnnouncement announcement)
        {
            return ids.Contains(announcement.id);
        }

        public bool HasUnreadAnnouncements()
        {
            if (!TTAnnouncementService.hasAnnouncementsCollection || 
                TTAnnouncementService.isGettingAnnouncements ||
                !TTAnnouncementService.currentAnnouncements.HasAnnouncements())
            {
                return false;
            }

            return TTAnnouncementService.currentAnnouncements.announcements.Any(announcement =>
                !IsAnnouncementRead(announcement));
        }

        public void Clear()
        {
            ids.Clear();
        }

        public void MarkAllAsRead()
        {
            MarkAsRead(TTAnnouncementService.currentAnnouncements?.announcements);
        }

        public void MarkAsRead(List<TTAnnouncement> announcements)
        {
            if (announcements == null)
            {
                return;
            }
            
            foreach (var announcement in announcements)
            {
                MarkAsRead(announcement);
            }
        }

        public void MarkAsRead(TTAnnouncement announcement)
        {
            if (announcement == null)
            {
                return;
            }
            
            if (IsAnnouncementRead(announcement))
            {
                return;
            }
            
            ids.Add(announcement.id);
        }

        public void Trim(List<TTAnnouncement> announcements)
        {
            if (announcements == null)
            {
                return;
            }
            
            try
            {
                var announcementIds = announcements.Select(announcement => announcement.id);

                for (int i = ids.Count - 1; i >= 0; i--)
                {
                    if (ids.All(id => !announcementIds.Contains(id)))
                    {
                        ids.RemoveAt(i);
                    }
                }
            }
            catch { /* do nothing */ }
        }

        public void Save()
        {
            Trim(TTAnnouncementService.currentAnnouncements?.announcements);
            PlayerPrefs.SetString(PLAYER_PREF_KEY, JsonUtility.ToJson(this));
        }
        
        private static TTViewedAnnouncementsCollection Create()
        {
            try
            {
                return JsonUtility.FromJson<TTViewedAnnouncementsCollection>(PlayerPrefs.GetString(PLAYER_PREF_KEY)) 
                       ?? new TTViewedAnnouncementsCollection();
            }
            catch
            {
                return new TTViewedAnnouncementsCollection();
            }
        }
    }
}