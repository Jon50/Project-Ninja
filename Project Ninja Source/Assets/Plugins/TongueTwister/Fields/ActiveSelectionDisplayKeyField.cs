using System;
using TongueTwister.Extensions;
using UnityEngine;

namespace TongueTwister.Fields
{
    /// <summary>
    /// Display Key field for scripts that provides a dropdown selection of display keys, referencing the available
    /// display keys from the TongueTwisterWindow's actively selected LocalizationManager. A LocalizationManager
    /// therefore must be selected in the TongueTwisterWindow for this field to populate with options/values.
    /// </summary>
    [Serializable]
    public class ActiveSelectionDisplayKeyField
    {
        /// <summary>
        /// The model chosen through the editor to represent this display key. The model ID is used to search the
        /// active LocalizationManager. Hence these display key fields are very tied to a single Localization Manager.
        /// Using an LM that wasn't used to provide a value for this field may result in errors or issues.
        /// </summary>
        [SerializeField] private int _modelId = -1;

        /// <summary>
        /// The string backup field which is written to every time the selection in the editor changes. This field is
        /// used as a secondary attempt if the current localization manager doesn't have a model with the stored
        /// value in _modelId on this object. This value will be used for localization instead.
        /// </summary>
        [SerializeField] private string _stringBackup = "";

        /// <summary>
        /// The full path of the display key (gotten through "GetFullDotName()"). This value is initially null but
        /// called the first time will result in a "Get" from the current instance of an LM.
        /// </summary>
        private string _key = "";

        /// <summary>
        /// The display key value. May be null if no current instance of the LocalizationManager is available or the
        /// configuration is not valid for the current LocalizationManager instance.
        /// </summary>
        public string Key
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_key))
                {
                    var model = LocalizationManager.instance?.GetModelById(_modelId);
                    if (model != null)
                    {
                        _key = model.GetFullDotName();
                    }
                    else if (!string.IsNullOrWhiteSpace(_stringBackup))
                    {
                        _key = _stringBackup;
                    }
                }

                return _key;
            }
        }

        /// <summary>
        /// Using the value of <see cref="Key"/>, gets a localization for the current locale.
        /// </summary>
        /// <returns>A localization.</returns>
        public Localization GetLocalization() => Key?.GetLocalization();

        /// <summary>
        /// Using the value of <see cref="Key"/>, gets a localization for the given locale.
        /// </summary>
        /// <param name="locale">The locale to use.</param>
        /// <returns>A localization.</returns>
        public Localization GetLocalization(Locale locale) => Key?.GetLocalization(locale);
    }
}