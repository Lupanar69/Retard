using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Retard.Engine.Models.ValueTypes;

namespace Retard.Engine.Models.Assets
{
    /// <summary>
    /// Contient toutes les resources du jeu,
    /// chargées dans la méthode LoadContent() de BaseEngine
    /// </summary>
    public readonly struct GameResources
    {
        #region Propriétés

        /// <summary>
        /// Les textures du jeu
        /// </summary>
        public readonly Dictionary<NativeString, Texture2D> Textures2D { get; init; }

        #endregion
    }
}
