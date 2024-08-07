﻿using System;
using System.Collections.Generic;
using Arch.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Retard.Core.Models;
using Retard.Core.Models.Assets.Input;
using Retard.Core.Models.ValueTypes;
using Retard.Core.ViewModels.App;
using Retard.Core.ViewModels.Input;
using Retard.Core.ViewModels.Scenes;
using Retard.Engine.Models.App;
using Retard.Engine.Models.DTOs.Input;

namespace Retard.Engine.ViewModels.Engine
{
    /// <summary>
    /// Chargée d'initialiser et màj tous les systèmes nécessaires
    /// au fonctionnement du jeu
    /// </summary>
    public abstract class BaseEngine : IDisposable
    {
        #region Variables d'instance

        /// <summary>
        /// Pour charger les ressources du jeu
        /// </summary>
        protected readonly ContentManager _content;

        /// <summary>
        /// Pour afficher les sprites à l'écran
        /// </summary>
        protected readonly SpriteBatch _spriteBatch;

        /// <summary>
        /// Le monde contenant les entités
        /// </summary>
        protected readonly World _world;

        /// <summary>
        /// Passerelle entre les entrées du joueur et les commandes à effectuer
        /// </summary>
        protected readonly InputManager _inputManager;

        /// <summary>
        /// Gère l'ajout, màj et suppression des scènes
        /// </summary>
        protected readonly SceneManager _sceneManager;

        /// <summary>
        /// Gère les paramètres de la fenêtre de jeu
        /// </summary>
        protected readonly AppViewport _appViewport;

        /// <summary>
        /// Gère les paramètres de la fenêtre de jeu
        /// </summary>
        protected readonly AppPerformance _appPerformance;

        /// <summary>
        /// L'instance du jeu
        /// </summary>
        private readonly Game _game;

        /// <summary>
        /// TRUE si l'objet a été disposé
        /// </summary>
        private bool disposedValue;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="game">Le jeu</param>
        /// <param name="graphicsDeviceManager">Configurateur des paramètres de la fenêtre du jeu</param>
        public BaseEngine(Game game, GraphicsDeviceManager graphicsDeviceManager)
        {
            // Initialise les components

            game.Activated += GameState.OnFocusEvent;
            game.Deactivated += GameState.OnFocusLostEvent;

            this._content = game.Content;
            this._spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this._world = World.Create();
            this._game = game;

            // Initialise les managers

            CreateDefaultConfigFiles();
            IInputScheme[] inputSchemes = this.GetInputSchemes();
            InputConfigDTO inputConfig = this.GetInputConfig();
            WindowSettings ws = this.GetWindowSettings();

            this._inputManager = new InputManager(inputSchemes, inputConfig, this._world);
            this._sceneManager = new SceneManager(1);
            this._appViewport = new AppViewport(game, graphicsDeviceManager, ws);
            this._appPerformance = new AppPerformance(game);
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BaseEngine()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Charge le contenu externe du jeu
        /// </summary>
        public abstract void LoadContent();

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        /// <param name="game">L'application</param>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public void Update(Game game, GameTime gameTime)
        {
            if (this._sceneManager.IsEmpty)
            {
                game.Exit();
            }

            // Màj les inputs

            this._inputManager.Update();

            // Màj les scènes

            this._sceneManager.UpdateInput(gameTime);
            this._sceneManager.Update(gameTime);
        }

        /// <summary>
        /// Màj à chaque frame
        /// </summary>
        public void AfterUpdate()
        {
            // Appelé en dernier pour ne pas écraser le précédent KeyboardState
            // avant les comparaisons

            this._inputManager.AfterUpdate();
        }

        /// <summary>
        /// Pour afficher des éléments à l'écran
        /// </summary>
        /// <param name="gameTime">Le temps écoulé depuis le début du jeu</param>
        public void Draw(GraphicsDevice graphicsDevice, GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Black);

            this._sceneManager.Draw(gameTime);
        }

        /// <summary>
        /// Libère les allocations mémoire
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Méthodes protégées

        /// <summary>
        /// Crée les fichiers JSON de config par défaut
        /// spécifiques à ce jeu
        /// </summary>
        protected abstract void CreateDefaultConfigFiles();

        /// <summary>
        /// Obtient les contrôleurs acceptés par le jeu
        /// </summary>
        /// <returns>La liste des contrôleurs acceptés par le jeu</returns>
        protected abstract IInputScheme[] GetInputSchemes();

        /// <summary>
        /// Obtient les données de configuration des entrées
        /// </summary>
        /// <returns>Le fichier de configuration des entrées</returns>
        protected abstract InputConfigDTO GetInputConfig();

        /// <summary>
        /// Obtient les paramètres de la fenêtre
        /// </summary>
        protected abstract WindowSettings GetWindowSettings();

        /// <summary>
        /// Crée les scènes du jeu
        /// </summary>
        /// <param name="textures2D">Les Textures2D du jeu</param>
        protected abstract void CreateScenes(Dictionary<NativeString, Texture2D> textures2D);

        /// <summary>
        /// Libère les allocations mémoire
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)

                    this._game.Activated -= GameState.OnFocusEvent;
                    this._game.Deactivated -= GameState.OnFocusLostEvent;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        #endregion
    }
}