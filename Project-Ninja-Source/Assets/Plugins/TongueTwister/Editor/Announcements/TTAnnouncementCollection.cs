using System;
using System.Collections.Generic;

namespace TongueTwister.Editor.Announcements
{
    /// <summary>
    /// Contains a list of <see cref="TTAnnouncement"/>s.
    /// </summary>
    [Serializable]
    internal class TTAnnouncementCollection
    {
        public List<TTAnnouncement> announcements = new List<TTAnnouncement>();
        
        public bool HasAnnouncements() => announcements != null && announcements.Count > 0;
    }
}