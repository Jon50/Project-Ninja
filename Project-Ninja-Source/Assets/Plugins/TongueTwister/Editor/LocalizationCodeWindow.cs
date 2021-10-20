using System;
using System.Collections.Generic;
using System.Linq;
using TongueTwister.StaticLabels;
using TongueTwister.Utilities;
using UnityEditor;
using UnityEngine;

namespace TongueTwister.Editor
{
    public class LocalizationCodeWindow : EditorWindow
    {
        private WindowMode _windowMode = WindowMode.LanguageISO639Alpha2;
        
        private string _filter = "";

        private IDictionary<ISO639Alpha2, string> _iso639alpha2 = new Dictionary<ISO639Alpha2, string>();

        private IDictionary<ISO3166Alpha2, string> _iso3166alpha2 = new Dictionary<ISO3166Alpha2, string>();

        private IDictionary<ISO3166Alpha3, string> _iso3166alpha3 = new Dictionary<ISO3166Alpha3, string>();

        private Vector2 _scrollPos = new Vector2();
        
        private GUIStyle 
            _filterStyle,
            _linkStyle,
            _toolbarSearchFieldStyle,
            _toolbarClearButtonStyle;

        private GUIStyle filterStyle => _filterStyle ?? (_filterStyle = GUI.skin.FindStyle("toolbar"));

        private GUIStyle toolbarSearchFieldStyle => _toolbarSearchFieldStyle ?? (_toolbarSearchFieldStyle = GUI.skin.FindStyle("ToolbarSeachTextField"));
        
        private GUIStyle toolbarSeachCancelButton => _toolbarClearButtonStyle ?? (_toolbarClearButtonStyle = GUI.skin.FindStyle("ToolbarSeachCancelButton"));

        private GUIStyle linkStyle
        {
            get
            {
                if (_linkStyle == null)
                {
                    _linkStyle = new GUIStyle();
                    _linkStyle.normal.textColor = new Color(0f, 0.50f, 1f);
                }

                return _linkStyle;
            }
        }

        [MenuItem("Tools/Tongue Twister/ISO Localization Codes")]
        public static void Open()
        {
            GetWindow<LocalizationCodeWindow>(
                EditorLabels.LocalizationCodeWindow.Title,
                true,
                typeof(TongueTwisterWindow));
        }

        private void OnEnable()
        {
            var languageAlpha2 = Enum.GetValues(typeof(ISO639Alpha2)).OfType<ISO639Alpha2>();
            foreach (var language in languageAlpha2)
            {
                _iso639alpha2.Add(language, LanguageUtility.GetEnglishName(language));
            }

            var countryAlpha2 = Enum.GetValues(typeof(ISO3166Alpha2)).OfType<ISO3166Alpha2>();
            foreach (var country in countryAlpha2)
            {
                _iso3166alpha2.Add(country, CountryUtility.GetEnglishName(country));
            }
            
            var countryAlpha3 = Enum.GetValues(typeof(ISO3166Alpha3)).OfType<ISO3166Alpha3>();
            foreach (var country in countryAlpha3)
            {
                _iso3166alpha3.Add(country, CountryUtility.GetEnglishName(country));
            }
        }

        private void OnGUI()
        {
            DrawFilter();
            DrawModeSwap();
            
            switch (_windowMode)
            {
                case WindowMode.LanguageISO639Alpha2: 
                    DrawUrl(
                        new GUIContent(
                            "https://www.LOC.gov", 
                            "https://www.loc.gov/standards/iso639-2/php/code_list.php"),
                        "https://www.loc.gov/standards/iso639-2/php/code_list.php");
                    break;
                case WindowMode.CountryISO3166Alpha2:
                case WindowMode.CountryISO3166Alpha3:
                    DrawUrl(
                        new GUIContent(
                            "https://www.ISO.org", 
                            "https://www.iso.org/iso-3166-country-codes.html"),
                        "https://www.iso.org/iso-3166-country-codes.html");
                    break;
            }

            GUILayout.BeginVertical(GUI.skin.box);
            _scrollPos = GUILayout.BeginScrollView(_scrollPos);
            
            switch (_windowMode)
            {
                case WindowMode.LanguageISO639Alpha2: 
                    DrawAlpha2LanguageCodes();
                    break;
                case WindowMode.CountryISO3166Alpha2:
                    DrawAlpha2CountryCodes();
                    break;
                case WindowMode.CountryISO3166Alpha3:
                    DrawAlpha3CountryCodes();
                    break;
            }
            
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
        }

        private void DrawFilter()
        {
            GUILayout.BeginHorizontal(filterStyle);
            
            // filter
            
            GUILayout.Label(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Filter.Text,
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Filter.Tooltip),
                GUILayout.ExpandWidth(false));
            
            _filter = EditorGUILayout.TextField(_filter, toolbarSearchFieldStyle);

            // clear button
            
            if (GUILayout.Button(
                new GUIContent(
                    "",
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Filter.ClearTooltip),
                toolbarSeachCancelButton,
                GUILayout.ExpandWidth(false)))
            {
                _filter = "";
                GUI.FocusControl(null);
            }
            
            GUILayout.EndHorizontal();
        }

        private void DrawModeSwap()
        {
            _windowMode = (WindowMode) EditorGUILayout.EnumPopup(
                EditorLabels.LocalizationCodeWindow.TypeSelector, 
                _windowMode);
        }

        private void DrawAlpha2LanguageCodes()
        {
            var alternate = false;
            var hasSearchString = !string.IsNullOrWhiteSpace(_filter);
            var showSetButton = 
                TongueTwisterWindow.isOpen && 
                TongueTwisterWindow.CurrentWindow.HasLocalizationManager &&
                TongueTwisterWindow.CurrentWindowMode == TongueTwisterWindow.TongueTwisterWindowMode.LocaleEditor &&
                TongueTwisterWindow.CurrentWindow.localeEditorTreeView.HasSelection();
            
            foreach (var key in _iso639alpha2.Keys)
            {
                if (DrawLine(key.ToString(), _iso639alpha2[key], alternate, hasSearchString, showSetButton,
                    () =>
                    {
                        TongueTwisterWindow.CurrentWindow.SetCurrentLocaleISOCode(key);
                    }))
                {
                    alternate = !alternate;
                };
            }
        }

        private void DrawAlpha2CountryCodes()
        {
            var alternate = false;
            var hasSearchString = !string.IsNullOrWhiteSpace(_filter);
            var showSetButton = 
                TongueTwisterWindow.isOpen && 
                TongueTwisterWindow.CurrentWindow.HasLocalizationManager &&
                TongueTwisterWindow.CurrentWindowMode == TongueTwisterWindow.TongueTwisterWindowMode.LocaleEditor &&
                TongueTwisterWindow.CurrentWindow.localeEditorTreeView.HasSelection();
            
            foreach (var key in _iso3166alpha2.Keys)
            {
                if (DrawLine(key.ToString(), _iso3166alpha2[key], alternate, hasSearchString, showSetButton,
                    () =>
                    {
                        TongueTwisterWindow.CurrentWindow.SetCurrentLocaleISOCode(key);
                    }))
                {
                    alternate = !alternate;
                };
            }
        }

        private void DrawAlpha3CountryCodes()
        {
            var alternate = false;
            var hasSearchString = !string.IsNullOrWhiteSpace(_filter);
            
            foreach (var key in _iso3166alpha3.Keys)
            {
                if (DrawLine(key.ToString(), _iso3166alpha3[key], alternate, hasSearchString, false))
                {
                    alternate = !alternate;
                };
            }
        }

        private bool DrawLine(string key, string value, bool alternate, bool useFilter, bool showSetButton, Action onSetButtonClick = null)
        {
            if (useFilter)
            {
                if (key.IndexOf(_filter, StringComparison.OrdinalIgnoreCase) < 0 &&
                    value.IndexOf(_filter, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    return false;
                }
            }

            if (alternate)
            {
                var originalGuiColor = GUI.color;
                GUI.color = originalGuiColor / 1.5f;
                GUILayout.BeginHorizontal(GUI.skin.box);
                GUI.color = originalGuiColor;
            }
            else
            {
                GUILayout.BeginHorizontal(GUI.skin.box);
            }

            GUILayout.Space(15);
            GUILayout.Label(key);
            GUILayout.FlexibleSpace();
            GUILayout.Label(value);
            GUILayout.Space(15);

            if (showSetButton && onSetButtonClick != null)
            {
                if (GUILayout.Button("Set"))
                {
                    onSetButtonClick.Invoke();
                }

                GUILayout.Space(15);
            }
            
            GUILayout.EndHorizontal();

            return true;
        }
        
        private void DrawUrl(GUIContent guiContent, string url)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            GUILayout.Label(guiContent, linkStyle);
            var lastRect = GUILayoutUtility.GetLastRect(); 
            if (Event.current.rawType == EventType.MouseDown && lastRect.Contains(Event.current.mousePosition))
                Application.OpenURL(url);

            GUILayout.Space(15);
            
            GUILayout.EndHorizontal();
        }

        private enum WindowMode
        {
            LanguageISO639Alpha2,
            CountryISO3166Alpha2,
            CountryISO3166Alpha3,
        }
    }
}