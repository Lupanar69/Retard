using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Models
{
    /// <summary>
    /// Crée un dictionnaire sérialisable dans l'Inspector
    /// </summary>
    /// <typeparam name="TKey">Le type de la clé</typeparam>
    /// <typeparam name="TValue">Le type de la valeurs</typeparam>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        #region Variables Unity

        /// <summary>
        /// Les clés
        /// </summary>
        [SerializeField]
        private List<TKey> _keys = new();

        /// <summary>
        /// Les valeurs
        /// </summary>
        [SerializeField]
        private List<TValue> _values = new();

        #endregion

        #region Fonctions publiques

        /// <summary>
        /// Enregistre le dictionnaire dans les listes
        /// </summary>
        public void OnBeforeSerialize()
        {
            this._keys.Clear();
            this._values.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                this._keys.Add(pair.Key);
                this._values.Add(pair.Value);
            }
        }

        /// <summary>
        /// Charge le dictionnaire depuis les listes
        /// </summary>
        /// <exception cref="Exception">Erreur si les clés et les valeurs ne sont pas au même nombre</exception>
        public void OnAfterDeserialize()
        {
            this.Clear();

            if (this._keys.Count != this._values.Count)
                throw new Exception($"there are {this._keys.Count} keys and {this._values.Count} values after deserialization. Make sure that both key and value types are serializable.");

            for (int i = 0; i < _keys.Count; i++)
                this.Add(this._keys[i], this._values[i]);

        }

        #endregion
    }
}