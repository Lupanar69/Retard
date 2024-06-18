using Arch.AOT.SourceGenerator;

namespace Retard.Core.Components.Sprites
{
    /// <summary>
    /// L'ID du sprite à afficher dans l'atlas
    /// </summary>
    [Component]
    public struct SpriteFrameCD
    {
        #region Variables d'instance

        /// <summary>
        /// L'ID du sprite à afficher dans l'atlas
        /// </summary>
        public int Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="frame">L'ID du sprite à afficher dans l'atlas</param>
        public SpriteFrameCD(int frame)
        {
            this.Value = frame;
        }

        #endregion
    }
}
