using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TongueTwister.Examples
{
    /// <summary>
    /// Fills a drop-down with Locale options and switches the current locale whenever a new choice is made. This is
    /// only an example behaviour but could work when put anywhere.
    /// </summary>
    public class LocaleDropdownBehaviour : MonoBehaviour
    {
        [Tooltip("The dropdown that this behaviour will manage.")] [SerializeField] 
        private Dropdown _dropdown;

        [Tooltip("How do you want to display the locale data?")] [SerializeField]
        private LocaleDisplayType _localeDisplayType = LocaleDisplayType.DisplayName;

        private void OnEnable()
        {
            if (LocalizationManager.Ready)
            {
                UpdateOptions();
            }
            else
            {
                LocalizationManager.OnInitialized += UpdateOptions;
            }

            LocalizationManager.OnExternalLocalesReloaded += UpdateOptions;

            _dropdown.onValueChanged.AddListener(OnSelectionChanged);
        }

        private void OnDisable()
        {
            _dropdown.onValueChanged.RemoveListener(OnSelectionChanged);
        }

        private void UpdateOptions()
        {
            var locales = LocalizationManager.instance.AllLocales;

            _dropdown.ClearOptions();
            _dropdown.AddOptions(locales.Select(CreateOptionData).ToList());
            _dropdown.value = locales.IndexOf(LocalizationManager.CurrentLocale);
        }

        private Dropdown.OptionData CreateOptionData(Locale locale)
        {
            var optionData = new Dropdown.OptionData();

            if (locale.Icon)
            {
                optionData.image =
                    Sprite.Create(locale.Icon as Texture2D,
                        new Rect(0, 0, 30, 30),
                        new Vector2(0.5f, 0.5f),
                        100);
            }

            optionData.text = GetLocaleName(locale);
            
            return optionData;
        }

        private string GetLocaleName(Locale locale)
        {
            switch (_localeDisplayType)
            {
                case LocaleDisplayType.DisplayName: return locale.Metadata.DisplayName;
                case LocaleDisplayType.LocalizationCode: return locale.Metadata.CustomCode;
                case LocaleDisplayType.LocalizedName: return locale.Metadata.NativeName;
                case LocaleDisplayType.SystemLanguage: return locale.Metadata.UnitySystemLanguage.ToString();
                default: return null;
            }
        }

        private void OnSelectionChanged(int index)
        {
            LocalizationManager.CurrentLocale = LocalizationManager.instance.AllLocales[index];
        }

        private enum LocaleDisplayType
        {
            DisplayName,
            LocalizedName,
            LocalizationCode,
            SystemLanguage
        }
    }
}