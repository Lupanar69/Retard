using MonoGame.Extended.Entities;

namespace Retard.Core.Models.Components.Map
{
    /// <summary>
    /// La liste des entités représentant les salles de la carte
    /// </summary>
    internal sealed class MapRoomsEntitiesBuffer
    {
        #region Variables d'instance

        /// <summary>
        /// La liste des entités représentant les salles de la carte
        /// </summary>
        internal Entity[] Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        /// <param name="roomsEs">Les salles de la carte</param>
        internal MapRoomsEntitiesBuffer(Entity[] roomsEs)
        {
            this.Value = roomsEs;
        }

        #endregion
    }
}
