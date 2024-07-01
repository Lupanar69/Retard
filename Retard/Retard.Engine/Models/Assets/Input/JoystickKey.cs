namespace Retard.Engine.Models.Assets.Input
{
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
        RightNorth,
        RightEast,
        RightSouth,
        RightWest
    }
}
