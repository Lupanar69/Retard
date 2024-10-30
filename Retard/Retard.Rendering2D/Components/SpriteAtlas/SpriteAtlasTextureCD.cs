using Arch.AOT.SourceGenerator;
using Arch.LowLevel;
using Microsoft.Xna.Framework.Graphics;
using Retard.Sprites.ViewModels;

namespace Retard.Sprites.Components.SpriteAtlas
{
    /// <summary>
    /// La Texture2D liée à ce SpriteAtlas
    /// </summary>
    /// <remarks>
    /// Constructeur
    /// </remarks>
    /// <param name="textureRef">Référence de la texture du SpritAtlas</param>
    [Component]
    public readonly struct SpriteAtlasTextureCD(Handle<Texture2D> textureRef)
    {
        #region Propriétés

        /// <summary>
        /// La texture du SpritAtlas
        /// </summary>
        public readonly Texture2D Value
        {
            get => SpriteManager.Instance.GetTexture(in this._value);
        }

        #endregion

        #region Variables d'instance

        /// <summary>
        /// Référence de la texture du SpritAtlas
        /// </summary>
        private readonly Handle<Texture2D> _value = textureRef;

        #endregion
    }
}
