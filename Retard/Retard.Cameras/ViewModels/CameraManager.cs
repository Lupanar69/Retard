using System;
using System.Runtime.CompilerServices;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Retard.Cameras.Components.Camera;
using Retard.Cameras.Entities;
using Retard.Cameras.Models;
using Retard.Cameras.Systems;
using Retard.Core.Models.Arch;

namespace Retard.Cameras.ViewModels
{
    /// <summary>
    /// Gère la création, édition et destruction des caméras du jeu
    /// </summary>
    public sealed class CameraManager
    {
        #region Singleton

        /// <summary>
        /// Singleton
        /// </summary>
        public static CameraManager Instance => CameraManager._instance.Value;

        /// <summary>
        /// Singleton
        /// </summary>
        private static readonly Lazy<CameraManager> _instance = new(() => new CameraManager());

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Les systèmes ECS à màj dans Update()
        /// </summary>
        private readonly Group _updateSystems;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        private CameraManager()
        {
            this._updateSystems = new Group("Update Systems");
            this._updateSystems.Add(new CameraDirtySystem());
            this._updateSystems.Initialize();
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        public void Update(World w)
        {
            this._updateSystems.Update(w);
        }

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Crée une caméra orthographique
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="pos">La position de l'entité dans la scène</param>
        /// <param name="viewport">Le cadre d'affichage de la caméra à l'écran</param>
        /// <param name="layers">Les layers à appliquer à la caméra</param>
        /// <returns>L'entité représentant une caméra orthographique</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity CreateOrthographicCamera(World w, Vector2 pos, Viewport viewport, RenderingLayer layers = RenderingLayer.Default)
        {
            return EntityFactory.CreateOrthographicCamera(w, pos, viewport, layers);
        }

        /// <summary>
        /// Déplace une caméra 2D dans l'espace
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="camE">L'entité de la caméra</param>
        /// <param name="direction">La direction de déplacement</param>
        public static void MoveCamera2D(World w, Entity camE, Vector2 direction)
        {
            ref var pos = ref w.Get<Camera2DPositionCD>(camE);
            var rot = w.Get<Camera2DRotationCD>(camE);
            pos.Value += Vector2.Transform(direction, Matrix.CreateRotationZ(0f - rot.Value));

            // Arrondit la position pour éviter des artefacts lors du rendu

            Vector2 temp = pos.Value;
            temp = new Vector2((int)temp.X, (int)temp.Y);
            pos.Value = temp;

            // Marque la caméra comme modifiée

            CameraManager.SetCameraDirty(w, camE);
        }

        /// <summary>
        /// Positionne une caméra 2D dans l'espace
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="camE">L'entité de la caméra</param>
        /// <param name="position">La nouvelle position</param>
        public static void SetCamera2DPosition(World w, Entity camE, Vector2 position)
        {
            ref var pos = ref w.Get<Camera2DPositionCD>(camE);

            // Arrondit la position pour éviter des artefacts lors du rendu

            Vector2 temp = position;
            temp = new Vector2((int)temp.X, (int)temp.Y);
            pos.Value = temp;

            // Marque la caméra comme modifiée

            CameraManager.SetCameraDirty(w, camE);
        }

        /// <summary>
        /// Récupère la matrice de la caméra
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="camE">L'entité de la caméra</param>
        public static Matrix GetCamera2DViewMatrix(World w, Entity camE)
        {
            return w.Get<Camera2DViewMatrixCD>(camE).Value;
        }

        /// <summary>
        /// Recaulcule le viewport de la caméra lorsque la fenêtre change de résolution
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="camE">L'entité de la caméra</param>
        /// <param name="newViewport">Le nouveau rect du viewport</param>
        public static void SetCamera2DViewport(World w, Entity camE, Viewport newViewport)
        {
            w.Set(camE, new Camera2DViewportCD(newViewport));
            w.Set(camE, new Camera2DOriginCD(new Vector2(newViewport.X / 2f, newViewport.Y / 2f)));
            w.Set(camE, new Camera2DProjectionMatrixCD(Matrix.CreateOrthographicOffCenter(newViewport.Bounds, 1f, 10000f)));

            // Marque la caméra comme modifiée

            CameraManager.SetCameraDirty(w, camE);
        }

        #endregion

        #region Méthodes statiques privées

        /// <summary>
        /// Marque la caméra comme modifiée
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        /// <param name="camE">L'entité de la caméra</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SetCameraDirty(World w, Entity camE)
        {
            w.Add<CameraMatrixIsDirtyTag>(camE);
        }

        #endregion
    }
}
