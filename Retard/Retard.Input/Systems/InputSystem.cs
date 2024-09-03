using Arch.Core;
using Retard.Core.Models.Arch;
using Retard.Engine.Entities;

namespace Retard.Input.Systems
{
    /// <summary>
    /// Affiche les sprites à l'écran
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="world">Le monde contenant les entités des sprites</param>
    /// <param name="nbMaxControllers">Le nombre max de contrôleurs pris en charge par l'InputSystem
    public readonly struct InputSystem(World world, int nbMaxControllers) : ISystemWorld
    {
        #region Propriétés

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        public readonly World World { get; init; } = world;

        #endregion
        #region Constructeur

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Update()
        {
            // Calcule les résultats des InputBindings

            Queries.ProcessButtonStateInputBindingsQuery(World);
            Queries.ProcessVector1DKeysInputBindingsQuery(World);
            Queries.ProcessVector1DTriggerInputBindingsQuery(World);
            Queries.ProcessVector1DJoystickXInputBindingsQuery(World);
            Queries.ProcessVector1DJoystickYInputBindingsQuery(World);
            Queries.ProcessVector2DKeysInputBindingsQuery(World);
            Queries.ProcessVector2DJoystickInputBindingsQuery(World);

            // Appelle les events pour chaque InputAction

            Queries.ProcessButtonStateInputActionsQuery(World, World);
            Queries.ProcessVector1DInputActionsQuery(World, World, nbMaxControllers);
            Queries.ProcessVector2DInputActionsQuery(World, World, nbMaxControllers);
        }

        #endregion
    }
}
