using UnityEngine;
using UnityEngine.UI;

namespace TongueTwister.Examples.Scenes
{
    public class ExampleSceneUi : MonoBehaviour
    {
        [SerializeField] private RectTransform _localeContainer;

        [SerializeField] private ExampleLocaleSelectionButton _localeSelectionPrefab;

        [SerializeField] private RectTransform _displayKeyContainer;

        [SerializeField] private ExampleDisplayKeyLabel _displayKeyLabelPrefab;
        
        private void Awake()
        {
            if (LocalizationManager.Ready) OnLocalizationManagerInitialized();
            else LocalizationManager.OnInitialized += OnLocalizationManagerInitialized; 
        }

        /// <summary>
        /// Called when the LocalizationManager is initialized.
        /// </summary>
        private void OnLocalizationManagerInitialized()
        {
            // get all locales from the current localization manager instance then display them as clickable buttons.
            
            var locales = LocalizationManager.instance.AllLocales;

            foreach (var locale in locales)
            {
                var localeSelectionItem = Instantiate(_localeSelectionPrefab, _localeContainer);
                localeSelectionItem.Locale = locale;
            }

            LayoutRebuilder.MarkLayoutForRebuild(_localeContainer);
            
            // get all display keys from the current localization manager and display them.

            var displayKeys = LocalizationManager.instance.GetConfiguredDisplayKeys();

            foreach (var displayKey in displayKeys)
            {
                var displayKeyLabel = Instantiate(_displayKeyLabelPrefab, _displayKeyContainer);
                displayKeyLabel.DisplayKey = displayKey;
            }

            LayoutRebuilder.MarkLayoutForRebuild(_displayKeyContainer);
        }
    }
}