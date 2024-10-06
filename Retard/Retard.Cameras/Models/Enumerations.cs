using System;

namespace Retard.Cameras.Models
{
    /// <summary>
    /// Le type de layer à appliquer sur une caméra
    /// et les objets qu'elle doit afficher
    /// </summary>
    [Flags]
    public enum RenderingLayer
    {
        Default = 1,
        UI = 2
    }
}
