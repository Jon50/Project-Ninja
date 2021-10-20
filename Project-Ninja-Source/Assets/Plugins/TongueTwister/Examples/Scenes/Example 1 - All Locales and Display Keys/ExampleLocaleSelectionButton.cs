using UnityEngine;
using UnityEngine.UI;

namespace TongueTwister.Examples.Scenes
{
    public class ExampleLocaleSelectionButton : MonoBehaviour
    {
        [SerializeField] private Text _text;
        
        public Locale Locale { get; set; }

        private void Start()
        {
            _text.text = Locale?.Metadata?.DisplayName ?? "No Locale";
        }

        public void OnClick()
        {
            LocalizationManager.CurrentLocale = Locale;
        }
    }
}