using System;
using System.Collections.Generic;
using TongueTwister.Editor.Announcements;
using TongueTwister.Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace TongueTwister.Editor
{
    public class TTAnnouncementsWindow : EditorWindow
    {
        #region Private

        private bool _debug;

        private Vector2 _announcementScrollPosition = new Vector2();

        private GUIStyle _richText, _linkStyle;

        #endregion
        
        #region Unity Events

        private void OnEnable() => Initialize();

        private void OnDisable() => Shutdown();

        private void OnGUI() => DrawWindow();

        #endregion

        #region Initialization

        private void Initialize()
        {
            CreateWindowTitle();
            EnsureHasOrGettingAnnouncements();
            CreateGuiStyles();
        }

        private void CreateWindowTitle()
        {
            titleContent = new GUIContent(StaticLabels.EditorLabels.TTAnnouncementsWindow.WindowTitle);
        }

        private void CreateGuiStyles()
        {
            _richText = new GUIStyle() {richText = true, wordWrap = true};
            _linkStyle = new GUIStyle();
            _linkStyle.normal.textColor = new Color(0f, .5f, 1f);
        }
        
        private void EnsureHasOrGettingAnnouncements()
        {
            if (TTAnnouncementService.isGettingAnnouncements || TTAnnouncementService.hasAnnouncementsCollection)
            {
                return;
            }

            RefreshAnnouncements();
        }

        #endregion

        #region Destruction

        private void Shutdown()
        {
            MarkAllAnnouncementsAsRead();
        }

        #endregion
        
        #region Static Utilities

        [MenuItem("Tools/Tongue Twister/Announcements")]
        public static TTAnnouncementsWindow Open()
        {
            return GetWindow<TTAnnouncementsWindow>(true);
        }

        #endregion

        #region Public Utilities

        public void MarkAllAnnouncementsAsRead()
        {
            try
            {
                TTViewedAnnouncementsCollection.current.MarkAllAsRead();
                TTViewedAnnouncementsCollection.current.Save();
            }
            catch (Exception exception)
            {
                if (_debug)
                {
                    Debug.LogError(exception.Message);
                }
            }
        }

        public void RefreshAnnouncements()
        {
            TTAnnouncementService.UpdateAnnouncements();
        }

        #endregion

        #region Main Window

        private void DrawWindow()
        {
            DrawUtilityButtons();
            DrawTitle();

            if (TTAnnouncementService.isGettingAnnouncements)
            {
                DrawFullyFlexibleLabel(StaticLabels.EditorLabels.TTAnnouncementsWindow.GettingAnnouncements);
                return;
            }

            var announcementCollection = TTAnnouncementService.currentAnnouncements;

            if (announcementCollection == null || !announcementCollection.HasAnnouncements())
            {
                DrawFullyFlexibleLabel(StaticLabels.EditorLabels.TTAnnouncementsWindow.NoAnnouncementsAtThisTime);
                return;
            }

            DrawAnnouncements(announcementCollection.announcements);
        }

        private void DrawUtilityButtons()
        {
            GUILayout.BeginVertical(GUI.skin.box, GUILayout.Height(30));
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUI.enabled = !(TTAnnouncementService.isGettingAnnouncements);
            if (GUILayout.Button(StaticLabels.EditorLabels.TTAnnouncementsWindow.Refresh))
            {
                TTAnnouncementService.UpdateAnnouncements();
            }
            if (GUILayout.Button(StaticLabels.EditorLabels.TTAnnouncementsWindow.MarkAllAsRead))
            {
                MarkAllAnnouncementsAsRead();
            }
            if (_debug && GUILayout.Button(StaticLabels.EditorLabels.TTAnnouncementsWindow.MarkAllAsUnread))
            {
                TTViewedAnnouncementsCollection.current.Clear();
                TTViewedAnnouncementsCollection.current.Save();
            }
            GUI.enabled = true;
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }

        private void DrawTitle()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(
                TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Logo),
                GUILayout.MaxHeight(25));
            GUILayout.Label(
                StaticLabels.EditorLabels.TTAnnouncementsWindow.Announcements, 
                EditorStyles.largeLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            DrawHorizontalBreak(2.0f, 5.0f, 5.0f, 5.0f, 5.0f);
        }

        private void DrawAnnouncements(List<TTAnnouncement> announcements)
        {
            _announcementScrollPosition = GUILayout.BeginScrollView(
                _announcementScrollPosition, 
                GUIStyle.none,
                GUI.skin.verticalScrollbar);
            
            foreach (var announcement in announcements)
            {
                DrawAnnouncement(announcement);
            }
            
            GUILayout.EndScrollView();
        }

        private void DrawAnnouncement(TTAnnouncement announcement)
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Space(5);
            GUILayout.BeginVertical();
                
            // title
            GUILayout.BeginHorizontal();
            GUILayout.Label(CreateAnnouncementTitle(announcement), _richText);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(15);
                
            // body
            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            GUILayout.Label(announcement.body, EditorStyles.wordWrappedLabel);
            GUILayout.EndHorizontal();

            // links
            if (announcement.HasLinks())
            {
                GUILayout.Space(15);
                foreach (var link in announcement.links)
                {
                    DrawUrl(new GUIContent(link.text), link.url);
                }
            }

            GUILayout.Space(15);
                
            GUILayout.EndVertical();
            GUILayout.Space(5);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            GUILayout.EndVertical();
        }

        #endregion

        #region Private Utilities

        private string CreateAnnouncementTitle(TTAnnouncement announcement)
        {
            var color = EditorGUIUtility.isProSkin ? "FFFFFFFF" : "000000FF";
            var unread = !TTViewedAnnouncementsCollection.current.IsAnnouncementRead(announcement);
            var announcementTitle = announcement.title;
            var announcementDate = announcement.dateText;
            return $"{(unread ? StaticLabels.EditorLabels.TTAnnouncementsWindow.NewLabel : "")} <color=#{color}><b>{announcementTitle}</b> ({announcementDate})</color>";
        }

        #endregion

        #region GUI Utilities

        private void DrawFullyFlexibleLabel(string label)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label(label);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        
        private void DrawHorizontalBreak(float height, float marginTop = 0.0f, float marginBottom = 0.0f, 
            float marginLeft = 0.0f, float marginRight = 0.0f)
        {
            var hasProLicense = EditorGUIUtility.isProSkin;
            var color = Color.black;
            color.a = hasProLicense ? 0.9f : 0.1f; 
            
            GUILayout.BeginVertical();
            GUILayout.Space(marginTop); 
            GUILayout.BeginHorizontal();
            GUILayout.Space(marginLeft);
            var originalColor = GUI.color;
            GUI.color = color;
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(height));
            GUI.color = originalColor;
            GUILayout.Space(marginRight); 
            GUILayout.EndHorizontal();
            GUILayout.Space(marginBottom); 
            GUILayout.EndVertical();
        }
        
        private void DrawUrl(GUIContent guiContent, string url)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("•");
            GUILayout.Label(guiContent, _linkStyle);
            var lastRect = GUILayoutUtility.GetLastRect(); 
            if (Event.current.rawType == EventType.MouseDown && lastRect.Contains(Event.current.mousePosition))
                Application.OpenURL(url);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        #endregion
    }
}