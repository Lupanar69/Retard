﻿using Arch.AOT.SourceGenerator;
using Microsoft.Xna.Framework;

namespace Retard.Core.Components.Sprites
{
    /// <summary>
    /// La position du sprite à l'écran en pixels
    /// </summary>
    [Component]
    public struct SpritePositionCD
    {
        #region Variables d'instance

        /// <summary>
        /// La position du sprite à l'écran en pixels
        /// </summary>
        public Vector2 Value;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="pos">La position du sprite à l'écran en pixels</param>
        public SpritePositionCD(Vector2 pos)
        {
            this.Value = pos;
        }

        #endregion
    }
}
