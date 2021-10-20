using System;
using UnityEngine;

namespace TongueTwister.Fields
{
    /// <summary>
    /// Given a Type (C# Class), displays a dropdown selection of options that can be supplied to a backing string field
    /// or property within a script.
    /// </summary>
    /// <example>
    ///     [DisplayKey(typeof(MyDKConstType))]
    ///     [SerializeField]
    ///     private string _displayKey;
    /// </example>
    public class DisplayKeyAttribute : PropertyAttribute
    {
        private Type _type;

        /// <summary>
        /// The given type which holds static display key classes and constants.
        /// </summary>
        public Type Type => _type;
        
        /// <summary>
        /// The C# type used to pull display key information from recursively within the editor.
        /// </summary>
        /// <param name="type">The type which holds static display key classes and constants.</param>
        public DisplayKeyAttribute(Type type)
        {
            _type = type;
        }
    }
}