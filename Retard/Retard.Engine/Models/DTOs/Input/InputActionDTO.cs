using Newtonsoft.Json;
using Retard.Core.Models.DTOs;

namespace Retard.Engine.Models.DTOs.Input
{
    /// <summary>
    /// Représente les données d'un InputAction
    /// </summary>
    public sealed class InputActionDTO : DataTransferObject
    {
        #region Propriétés

        /// <summary>
        /// L'ID de cette action
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Le type de valeur retournée par une InputAciton
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public readonly InputActionReturnValueType ValueType;

        /// <summary>
        /// La liste des bindings de cette action
        /// </summary>
        public readonly InputBindingDTO[] Bindings;

        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="name">L'ID de l'action</param>
        /// <param name="valueType">Le type de valeur retournée par l'action</param>
        /// <param name="bindings">Les entrées liées à cette action</param>
        public InputActionDTO(string name, InputActionReturnValueType valueType, params InputBindingDTO[] bindings)
        {
            this.Name = name;
            this.ValueType = valueType;
            this.Bindings = bindings;
        }

        #endregion
    }
}
