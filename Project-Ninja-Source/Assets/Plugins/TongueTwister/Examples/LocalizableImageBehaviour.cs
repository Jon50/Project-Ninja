using System;
using TongueTwister.Extensions;
using TongueTwister.Fields;
using UnityEngine;
using UnityEngine.UI;

namespace TongueTwister.Examples
{
    /// <summary>
    /// Basic localizable image behaviour for use with <see cref="UnityEngine.UI.Image"/> components. It will
    /// automatically change the image component's sprite/texture based on the selected display key and current locale.
    /// This behaviour works well for generic cases but may require tweaking/extension.
    /// </summary>
    public class LocalizableImageBehaviour : MonoBehaviour
    {
        [SerializeField] 
        protected ActiveSelectionDisplayKeyField displayKey = new ActiveSelectionDisplayKeyField();

        [SerializeField] 
        protected Image image;

        [Tooltip("Automatically sets the height and width of the rect to the localization's texture size.")]
        [SerializeField] 
        protected bool autoSetRectDimensions = true;
        
        [SerializeField] 
        protected Rect rect;

        [SerializeField] 
        protected Vector2 pivot = new Vector2(0.5f, 0.5f);
        
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
            image = image ? image : GetComponent<Image>();
        }

        protected virtual Localization GetLocalization()
        {
            return displayKey?.Key?.GetLocalization();
        }
        
        protected virtual void AssignLocalization(Texture2D localization)
        {
            if (!localization)
            {
                image.sprite = null;
                return;
            }
            
            if (image)
            {
                if (autoSetRectDimensions)
                {
                    rect.height = localization.height;
                    rect.width = localization.width;
                }
                
                image.sprite = Sprite.Create(
                    localization,
                    rect,
                    pivot);
            }
        }
        
        protected virtual void OnCurrentLocaleChanged()
        {
            try
            {
                var localization = GetLocalization();
                AssignLocalization(localization?.Texture as Texture2D);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }
    }
}