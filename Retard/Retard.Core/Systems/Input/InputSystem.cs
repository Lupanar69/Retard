using Arch.Core;
using Arch.System;

namespace Retard.Core.Systems.Input
{
    /// <summary>
    /// Affiche les sprites à l'écran
    /// </summary>
    public sealed class InputSystem : BaseSystem<World, float>
    {
        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="world">Le monde contenant les entités des sprites</param>
        public InputSystem(World world) : base(world)
        {

        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public override void Update(in float _)
        {

        }

        #endregion
    }
}
