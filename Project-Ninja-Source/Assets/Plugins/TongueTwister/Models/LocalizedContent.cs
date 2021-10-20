using System;
using UnityEngine;

namespace TongueTwister.Models
{
    /// <summary>
    /// Represents some localized content, in most cases contained by a <see cref="LocalizedContentCollection"/>. These
    /// objects are useful for storing generic <see cref="UnityEngine.Object"/> references and provides additional
    /// functionality for shorthand casts to common Unity types.
    /// </summary>
    [Serializable]
    public class LocalizedContent
    {
        /// <summary>
        /// The identifier or name used to access and identify this <see cref="LocalizedContent"/>.
        /// </summary>
        public string Identifier = "Localized Content";
        
        /// <summary>
        /// A generic <see cref="UnityEngine.Object"/> reference. This field can be casted to other Unity types,
        /// see <see cref="As{T}"/>.
        /// </summary>
        public UnityEngine.Object Object;

        /// <summary>
        /// This <see cref="Object"/> casted <see cref="As{T}"/> an <see cref="AudioClip"/> type.
        /// </summary>
        public AudioClip AudioClip => (AudioClip) Object;

        /// <summary>
        /// This <see cref="Object"/> casted <see cref="As{T}"/> a <see cref="Texture"/> type.
        /// </summary>
        public Texture Texture => (Texture) Object;

        /// <summary>
        /// This <see cref="Object"/> casted <see cref="As{T}"/> a <see cref="Texture2D"/> type.
        /// </summary>
        public Texture2D Texture2D => (Texture2D) Object;

        /// <summary>
        /// This <see cref="Object"/> casted <see cref="As{T}"/> a <see cref="Sprite"/> type.
        /// </summary>
        public Sprite Sprite => (Sprite) Object;

        /// <summary>
        /// This <see cref="Object"/> casted <see cref="As{T}"/> a <see cref="GameObject"/> type.
        /// </summary>
        public GameObject GameObject => (GameObject) Object;

        /// <summary>
        /// Shorthand function for casting this <see cref="Object"/> to the given type <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T">The Unity type to cast to, where <see cref="T"/> is an <see cref="UnityEngine.Object"/>
        /// </typeparam>
        /// <returns>The casted object.</returns>
        public T As<T>() where T : UnityEngine.Object => (T) Object;
        
        /// <summary>
        /// Creates a new <see cref="LocalizedContent"/> object based on the content of this one.
        /// </summary>
        /// <returns></returns>
        public LocalizedContent Clone()
        {
            return new LocalizedContent()
            {
                Identifier = Identifier,
                Object = Object
            };
        }
    }
}