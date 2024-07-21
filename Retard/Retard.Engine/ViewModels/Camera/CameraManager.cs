using Arch.Core;
using Microsoft.Xna.Framework;
using Retard.Core.Entities;
using Retard.Core.Models.Arch;
using Retard.Engine.Systems.Camera;

namespace Retard.Engine.ViewModels.Camera
{
    /// <summary>
    /// Chargé de créer et màj les caméras du jeu
    /// </summary>
    public static class CameraManager
    {
        #region Variables d'instance

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        private static World _world;

        /// <summary>
        /// Les systèmes ECS à màj dans Update()
        /// </summary>
        private static readonly Group _updateSystems;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        static CameraManager()
        {
            CameraManager._updateSystems = new Group("Update Systems");
        }

        #endregion

        #region Méthodes statiques publiques

        /// <summary>
        /// Crée une caméra orthographique
        /// </summary>
        /// <param name="position">La position de la caméra en pixels</param>
        /// <param name="origin">Le point d'origine de la caméra</param>
        /// <param name="playerID">L'ID du contrôleur pouvant manipuler cette caméra</param>
        /// <param name="rotation">La rotation de la caméra sur l'axe Z</param>
        /// <returns>L'entité de la caméra</returns>
        public static Entity CreateOrthographicCamera(Vector2 position, Vector2 origin, int playerID = 0, float rotation = 0f)
        {
            return EntityFactory.CreateOrthographicCameraEntity(CameraManager._world, position, origin, playerID, rotation);
        }

        #endregion

        #region Méthodes statiques internes

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="world">Le monde contenant les entités</param>
        internal static void Initialize(World world)
        {
            CameraManager._world = world;
            CameraManager._updateSystems.Add(new CameraSystem(world));
            CameraManager._updateSystems.Initialize();
        }

        /// <summary>
        /// Màj les caméras
        /// </summary>
        internal static void Update()
        {
            CameraManager._updateSystems.Update();
        }

        #endregion
    }
}
