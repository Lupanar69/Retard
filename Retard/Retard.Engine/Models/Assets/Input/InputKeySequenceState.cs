namespace Retard.Engine.Models.Assets.Input
{
    /// <summary>
    /// L'état que doit avoir un InputKeySequenceElement
    /// pour être considéré actif
    /// </summary>
    public enum InputKeySequenceState
    {
        Pressed,
        Held,
        Released
    }
}
