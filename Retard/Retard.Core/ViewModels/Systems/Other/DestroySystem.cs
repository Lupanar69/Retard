using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using Retard.Core.Models.Components.Other;

namespace Retard.Core.ViewModels.Systems.Other
{
    /// <summary>
    /// Détruit toutes les entités possédant un DestroyTag
    /// </summary>
    public class DestroySystem : EntityUpdateSystem
    {
        #region Constructeur

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public DestroySystem()
            : base(Aspect.All(typeof(DestroyTag)))
        {

        }

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="mapperService">Pour initialiser les ComponentMappers</param>
        public override void Initialize(IComponentMapperService mapperService)
        {

        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le lancement de l'application</param>
        public override void Update(GameTime gameTime)
        {
            foreach (int entityID in this.ActiveEntities)
            {
                this.DestroyEntity(entityID);
            }
        }

        #endregion
    }
}
