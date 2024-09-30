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
    /// <param name="nbMaxControllers">Le nombre max de contrôleurs pris en charge par l'InputSystem
    public readonly struct InputSystem(int nbMaxControllers) : ISystem
    {
        #region Méthodes publiques

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="w">Le monde contenant les entités</param>
        public void Update(World w)
        {
            // Calcule les résultats des InputBindings

            Queries.ProcessButtonStateInputBindingsQuery(w);
            Queries.ProcessVector1DKeysInputBindingsQuery(w);
            Queries.ProcessVector1DTriggerInputBindingsQuery(w);
            Queries.ProcessVector1DJoystickXInputBindingsQuery(w);
            Queries.ProcessVector1DJoystickYInputBindingsQuery(w);
            Queries.ProcessVector2DKeysInputBindingsQuery(w);
            Queries.ProcessVector2DJoystickInputBindingsQuery(w);

            // Appelle les events pour chaque InputAction

            Queries.ProcessButtonStateInputActionsQuery(w, w);
            Queries.ProcessVector1DInputActionsQuery(w, w, nbMaxControllers);
            Queries.ProcessVector2DInputActionsQuery(w, w, nbMaxControllers);
        }

        #endregion
    }
}
