using Arch.Core;
using Retard.Core.Models.Arch;

namespace Retard.Engine.Systems.Camera
{
    /// <summary>
    /// Chargé de màj les caméras du jeu
    /// </summary>
    public readonly struct CameraSystem : ISystemWorld
    {
        #region Propriétés

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        public World World { get; init; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="world">Le monde contenant les entités des sprites</param>
        public CameraSystem(World world)
        {
            this.World = world;
        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public void Update()
        {

        }

        #endregion

        #region Méthodes privées

        #endregion
    }
}
