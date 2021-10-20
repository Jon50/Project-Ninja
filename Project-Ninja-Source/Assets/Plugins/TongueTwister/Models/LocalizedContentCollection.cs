using System;
using System.Collections.Generic;

namespace TongueTwister.Models
{
    /// <summary>
    /// Represents an easy to use collection of <see cref="LocalizedContent"/>.
    /// </summary>
    [Serializable]
    public class LocalizedContentCollection
    {
        /// <summary>
        /// All <see cref="LocalizedContent"/> in this collection.
        /// </summary>
        public List<LocalizedContent> LocalizedContent = new List<LocalizedContent>();

        public LocalizedContentCollection() { }

        /// <summary>
        /// Access content given an <see cref="identifier"/>. The system will loop over all
        /// items in <see cref="LocalizedContent"/> until it finds one that matches the given value. 
        /// </summary>
        /// <param name="identifier">The <see cref="LocalizedContent"/> identifier.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown when the provided <see cref="identifier"/> value isn't
        /// used by any of the items in the collection.</exception>
        public LocalizedContent this[string identifier]
        {
            get
            {
                for (int i = 0; i < LocalizedContent.Count; i++)
                {
                    if (LocalizedContent[i].Identifier == identifier)
                    {
                        return LocalizedContent[i];
                    }
                }

                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Access content given an <see cref="index"/>. This indexer behaves exactly like an array, returning the exact
        /// item at the provided <see cref="index"/>.
        /// </summary>
        /// <param name="index">The integer position of an item in <see cref="LocalizedContent"/>.</param>
        public LocalizedContent this[int index] => LocalizedContent[index];

        /// <summary>
        /// The number of items in <see cref="LocalizedContent"/>.
        /// </summary>
        public int Count => LocalizedContent?.Count ?? 0;

        /// <summary>
        /// Adds a new <see cref="LocalizedContent"/> item to this collection.
        /// </summary>
        /// <param name="localizedContent"></param>
        public void Add(LocalizedContent localizedContent)
        {
            LocalizedContent.Add(localizedContent);
        }

        /// <summary>
        /// Removes an existing <see cref="LocalizedContent"/> item from this collection.
        /// </summary>
        /// <param name="localizedContent"></param>
        public void Remove(LocalizedContent localizedContent)
        {
            LocalizedContent.Remove(localizedContent);
        }
        
        /// <summary>
        /// Constructor that can be used to clone the values stored on another collection to this new collection.
        /// </summary>
        /// <param name="clone">The <see cref="LocalizedContentCollection"/> to clone from.</param>
        public LocalizedContentCollection(LocalizedContentCollection clone)
        {
            LocalizedContent = new List<LocalizedContent>();

            if (clone?.LocalizedContent != null)
            {
                foreach (var localizedContent in clone.LocalizedContent)
                {
                    LocalizedContent.Add(localizedContent.Clone());
                }
            }
        }
    }
}