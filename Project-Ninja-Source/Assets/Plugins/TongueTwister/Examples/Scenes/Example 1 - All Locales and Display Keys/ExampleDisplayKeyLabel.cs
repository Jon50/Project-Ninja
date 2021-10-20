using TongueTwister.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace TongueTwister.Examples.Scenes
{
    public class ExampleDisplayKeyLabel : MonoBehaviour
    {
        [SerializeField] private Text _localizationText, _displayKeyText;

        public string DisplayKey
        {
            set
            {
                _localizationText.text = value.GetLocalizationText();
                _displayKeyText.text = value;
            }
        }

        private void Awake()
        {
            LocalizationManager.OnCurrentLocaleChanged += OnCurrentLocaleChanged;
        }

        private void OnCurrentLocaleChanged()
        {
            _localizationText.text = _displayKeyText.text.GetLocalizationText();
        }
    }
}