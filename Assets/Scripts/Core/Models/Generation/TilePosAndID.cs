using Unity.Collections;
using Unity.Mathematics;

namespace Assets.Scripts.Core.Models.Generation
{
    /// <summary>
    /// Représente la position et l'ID d'une case sur la carte
    /// </summary>
    public struct TilePosAndID
    {
        #region Variables d'instance

        /// <summary>
        /// La position
        /// </summary>
        public int2 Pos;

        /// <summary>
        /// L'id
        /// </summary>
        public FixedString32Bytes ID;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="pos">La position</param>
        /// <param name="id">L'id</param>
        public TilePosAndID(int2 pos, FixedString32Bytes id)
        {
            this.Pos = pos;
            this.ID = id;
        }

        #endregion
    }
}