using Newtonsoft.Json;
using Retard.Engine.Models.Assets.Input;

namespace Retard.Engine.Models.DTOs.Input
{
    /// <summary>
    /// Représente les données d'un InputAction
    /// </summary>
    public sealed class InputActionDTO
    {
        #region Propriétés

        /// <summary>
        /// L'ID de cette action
        /// </summary>
        public string Name
        {
            get;
            init;
        }

        /// <summary>
        /// Le type de valeur retournée par une InputAciton
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public InputActionReturnValueType ValueType
        {
            get;
            init;
        }

        /// <summary>
        /// La liste des bindings de cette action
        /// </summary>
        public InputBindingDTO[] Bindings
        {
            get;
            init;
        }

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
            Name = name;
            ValueType = valueType;
            Bindings = bindings;
        }

        #endregion
    }
}
