using System;
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
        }

        #endregion
    }
}
