using Arch.AOT.SourceGenerator;

namespace Retard.Rendering2D.Components.Sprite
{
    /// <summary>
    /// L'ID du sprite à afficher dans l'atlas
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="frame">L'ID du sprite à afficher dans l'atlas</param>
    [Component]
    public struct SpriteFrameCD(int frame)
    {
        #region Variables d'instance

        /// <summary>
        /// L'ID du sprite à afficher dans l'atlas
        /// </summary>
        public int Value = frame;

        #endregion
    }
}
