namespace Retard.Input.Models
{
    #region Input

    /// <summary>
    /// L'état du bouton lié à un InputBinding
    /// </summary>
    public enum ButtonStateType
    {
        Inert,
        Started,
        Performed,
        Finished
    }

    /// <summary>
    /// Le type de valeur retournée par une InputAciton
    /// </summary>
    public enum InputActionReturnValueType
    {
        ButtonState,
        Vector1D,
        Vector2D,
    }

    /// <summary>
    /// Représente les boutons de la souris
    /// </summary>
    public enum MouseKey
    {
        None,
        Mouse0, // Bouton gauche
        Mouse1, // Bouton droit
        Mouse2, // Bouton milieu
        Mouse3, // Bouton additionel gauche
        Mouse4, // Bouton additionel droit
    }

    /// <summary>
    /// Représente une direction du joystick lorsqu'il est poussé par le joueur. 
    /// Evalué lorsqu'il approche sa valeur maximale ~1f
    /// </summary>
    public enum JoystickKey
    {
        None,
        LeftNorth,
        LeftEast,
        LeftSouth,
        LeftWest,
        LeftNorthEast,
        LeftSouthEast,
        LeftSouthWest,
        LeftNorthWest,
        RightNorth,
        RightEast,
        RightSouth,
        RightWest,
        RightNorthEast,
        RightSouthEast,
        RightSouthWest,
        RightNorthWest,
    }

    /// <summary>
    /// Représente les différents types d'entrées de type bouton
    /// </summary>
    public enum InputBindingKeyType
    {
        MouseKey,
        KeyboardKey,
        GamePadKey,
        JoystickKey
    }

    /// <summary>
    /// Le type de joystick utilisé par l'InputBinding
    /// </summary>
    public enum JoystickType
    {
        None,
        Left,
        Right
    }

    /// <summary>
    /// Le type de gâchette utilisée par l'InputBinding
    /// </summary>
    public enum TriggerType
    {
        None,
        LeftTrigger,
        RightTrigger,
        MouseWheel
    }

    /// <summary>
    /// L'axe du joystick à évaluer
    /// </summary>
    public enum JoystickAxisType
    {
        XAxis,
        YAxis,
        Both
    }

    /// <summary>
    /// L'état que doit avoir un InputKeySequenceElement
    /// pour être considéré actif
    /// </summary>
    public enum InputKeySequenceState
    {
        Pressed,
        Held,
        Released,
        Inert
    }

    /// <summary>
    /// Permet de sélectionner le handle auquel
    /// ajouter une nouvelle méthode dans un InputControls
    /// </summary>
    public enum InputEventHandleType
    {
        Started,
        Performed,
        Finished
    }

    #endregion
}
