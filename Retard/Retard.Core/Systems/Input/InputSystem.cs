using Arch.Core;
using Retard.Core.Models.Arch;

namespace Retard.Core.Systems.Input
{
    /// <summary>
    /// Affiche les sprites à l'écran
    /// </summary>
    public struct InputSystem : ISystemWorld
    {
        #region Propriétés

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        public World World { get; set; }

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="world">Le monde contenant les entités des sprites</param>
        public InputSystem(World world)
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

        /// <summary>
        /// Libère les allocations
        /// </summary>
        public void Dispose()
        {

        }

        #endregion
    }
}
