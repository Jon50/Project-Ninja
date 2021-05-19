using System;
using UnityEngine;

namespace TongueTwister
{
    /// <summary>
    /// A configurable locale which contains information surrounding an accepted means of presentation for localizations
    /// within the TongueTwister system.
    /// </summary>
    [Serializable]
    public class Locale
    {
        /// <summary>
        /// An unique ID based on a <see cref="System.Guid"/>.
        /// </summary>
        [SerializeField] 
        public string Id;
        
        /// <summary>
        /// Information about this <see cref="Locale"/>.
        /// </summary>
        [SerializeField] 
        public LocaleMetadata Metadata = new LocaleMetadata();

        /// <summary>
        /// The icon of this locale.
        /// </summary>
        [SerializeField] 
        public Texture Icon;

        /// <summary>
        /// Creates a new locale.
        /// </summary>
        public Locale()
        {
            Id = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Creates a new locale based on an existing one, copying all of the field values.
        /// </summary>
        /// <param name="copy">The locale to copy from.</param>
        /// <param name="newId">Creates a new ID if true, otherwise uses the copied locale's ID.</param>
        public Locale(Locale copy, bool newId = false)
        {
            Id = newId ? Guid.NewGuid().ToString() : copy.Id;
            Metadata = new LocaleMetadata(copy.Metadata);
            Icon = copy.Icon;
        }

        /// <summary>
        /// Creates a new <see cref="Locale"/> based on a <see cref="LocaleMetadata"/>.
        /// </summary>
        /// <param name="metadata"></param>
        public Locale(LocaleMetadata metadata)
        {
            Id = Guid.NewGuid().ToString();
            Metadata = new LocaleMetadata(metadata);
        }
    }
}