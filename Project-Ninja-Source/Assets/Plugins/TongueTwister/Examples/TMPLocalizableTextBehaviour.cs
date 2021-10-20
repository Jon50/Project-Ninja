using System;
using TMPro;
using TongueTwister.Extensions;
using TongueTwister.Fields;
using UnityEngine;

namespace TongueTwister.Examples
{
    /// <summary>
    /// Localizable text behaviour for use with <see cref="TMP_Text"/> components. It will automatically change the text
    /// component's text content based on the selected display key and current locale.
    /// </summary>
    public class TMPLocalizableTextBehaviour : MonoBehaviour
    {
        [SerializeField] 
        protected ActiveSelectionDisplayKeyField displayKey = new ActiveSelectionDisplayKeyField();

        [SerializeField]
        protected TMP_Text text;
        
        private void OnEnable()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            // (1) initialize all components on this behaviour 
            
            GetComponents();
            
            // (2) subscribe to the localization manager event that watches for the current locale to change
            
            LocalizationManager.OnCurrentLocaleChanged += OnCurrentLocaleChanged;
            
            // (3) if the localization manager is ready, call OnCurrentLocaleChanged() anyway

            if (LocalizationManager.Ready)
            {
                OnCurrentLocaleChanged();
            }
        }

        protected virtual void GetComponents()
        {
            text = text ? text : GetComponent<TMP_Text>();
        }

        protected virtual Localization GetLocalization()
        {
            return displayKey?.Key?.GetLocalization();
        }
        
        protected virtual void AssignLocalization(string localization)
        {
            if (text)
            {
                text.text = localization;
            }
        }
        
        protected virtual void OnCurrentLocaleChanged()
        {
            try
            {
                var localization = GetLocalization();
                AssignLocalization(localization?.Text);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }
}