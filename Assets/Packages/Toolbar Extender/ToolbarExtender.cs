using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

// https://github.com/marijnz/unity-toolbar-extender

namespace XDFramework.Editor
{
    /// <summary>
    /// Permet d'étendre la barre d'outils d'Unity
    /// avec des boutons personnalisés
    /// </summary>
    [InitializeOnLoad]
    public static class ToolbarExtender
    {
        #region Constantes

#if UNITY_2019_3_OR_NEWER
        public const float space = 8;
#else
		public const float space = 10;
#endif
        public const float largeSpace = 20;
        public const float buttonWidth = 32;
        public const float dropdownWidth = 80;
#if UNITY_2019_1_OR_NEWER
        public const float playPauseStopWidth = 140;
#else
		public const float playPauseStopWidth = 100;
#endif

        #endregion

        #region Variables statiques

        static int m_toolCount;
        static GUIStyle m_commandStyle = null;

        public static readonly List<Action> LeftToolbarGUI = new List<Action>();
        public static readonly List<Action> RightToolbarGUI = new List<Action>();

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        static ToolbarExtender()
        {
            Type toolbarType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.Toolbar");

#if UNITY_2019_1_OR_NEWER
            string fieldName = "k_ToolCount";
#else
			string fieldName = "s_ShownToolIcons";
#endif

            FieldInfo toolIcons = toolbarType.GetField(fieldName,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

#if UNITY_2019_3_OR_NEWER
            m_toolCount = toolIcons != null ? ((int)toolIcons.GetValue(null)) : 8;
#elif UNITY_2019_1_OR_NEWER
			m_toolCount = toolIcons != null ? ((int) toolIcons.GetValue(null)) : 7;
#elif UNITY_2018_1_OR_NEWER
			m_toolCount = toolIcons != null ? ((Array) toolIcons.GetValue(null)).Length : 6;
#else
			m_toolCount = toolIcons != null ? ((Array) toolIcons.GetValue(null)).Length : 5;
#endif

            ToolbarCallback.OnToolbarGUI = OnGUI;
            ToolbarCallback.OnToolbarGUILeft = GUILeft;
            ToolbarCallback.OnToolbarGUIRight = GUIRight;
        }

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Affiche les boutons de la barre d'outils de gauche
        /// </summary>
        public static void GUILeft()
        {
            GUILayout.BeginHorizontal();
            foreach (var handler in LeftToolbarGUI)
            {
                handler();
            }
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Affiche les boutons de la barre d'outils de droite
        /// </summary>
        public static void GUIRight()
        {
            GUILayout.BeginHorizontal();
            foreach (var handler in RightToolbarGUI)
            {
                handler();
            }
            GUILayout.EndHorizontal();
        }

        #endregion

        #region Fonctions privées

        /// <summary>
        /// Appelée à chaque Update d'éditeur
        /// </summary>
        private static void OnGUI()
        {
            // Crée 2 conteneurs, à gauche et à droite des boutons Play

            if (m_commandStyle == null)
            {
                m_commandStyle = new GUIStyle("CommandLeft");
            }

            var screenWidth = EditorGUIUtility.currentViewWidth;

            // Calculs pour déterminer la taille de la barre
            // et la position des boutons

            float playButtonsPosition = Mathf.RoundToInt((screenWidth - playPauseStopWidth) / 2);

            Rect leftRect = new(0, 0, screenWidth, Screen.height);
            leftRect.xMin += space; // Espacement à gauche
            leftRect.xMin += buttonWidth * m_toolCount; // Boutons
#if UNITY_2019_3_OR_NEWER
            leftRect.xMin += space; // Espace entre outils et pivot
#else
			leftRect.xMin += largeSpace; // Espace entre outils et pivot
#endif
            leftRect.xMin += 64 * 2; // Les boutons pivot
            leftRect.xMax = playButtonsPosition;

            Rect rightRect = new(0, 0, screenWidth, Screen.height);
            rightRect.xMin = playButtonsPosition;
            rightRect.xMin += m_commandStyle.fixedWidth * 3; // Boutons Play
            rightRect.xMax = screenWidth;
            rightRect.xMax -= space; // Espacement à droite
            rightRect.xMax -= dropdownWidth; // Layout
            rightRect.xMax -= space; // Espace entre le layout et les layers
            rightRect.xMax -= dropdownWidth; // Layers
#if UNITY_2019_3_OR_NEWER
            rightRect.xMax -= space; // Espace entre les boutons layout et compte
#else
			rightRect.xMax -= largeSpace; // Espace entre les boutons layout et compte
#endif
            rightRect.xMax -= dropdownWidth; // Compte
            rightRect.xMax -= space; // Espace entre les boutons compte et cloud
            rightRect.xMax -= buttonWidth; // Cloud
            rightRect.xMax -= space; // Espace entre les boutons cloud et collab
            rightRect.xMax -= 78; // Collab

            // Ajoute de l'espacement entre les contrôles existants

            leftRect.xMin += space;
            leftRect.xMax -= space;
            rightRect.xMin += space;
            rightRect.xMax -= space;

            // Marges en haut et en bas

#if UNITY_2019_3_OR_NEWER
            leftRect.y = 4;
            leftRect.height = 22;
            rightRect.y = 4;
            rightRect.height = 22;
#else
			leftRect.y = 5;
			leftRect.height = 24;
			rightRect.y = 5;
			rightRect.height = 24;
#endif

            // Affiche chaque bouton

            if (leftRect.width > 0)
            {
                GUILayout.BeginArea(leftRect);
                GUILayout.BeginHorizontal();
                foreach (var handler in LeftToolbarGUI)
                {
                    handler();
                }

                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }

            if (rightRect.width > 0)
            {
                GUILayout.BeginArea(rightRect);
                GUILayout.BeginHorizontal();
                foreach (var handler in RightToolbarGUI)
                {
                    handler();
                }

                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }
        }

        #endregion
    }
}
