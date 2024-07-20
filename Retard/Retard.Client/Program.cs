namespace Retard.Client
{
    /// <summary>
    /// Lance le jeu
    /// </summary>
    internal static class Program
    {
        private static void Main()
        {
            // Lance le jeu

            using GameApp game = new();
            game.Run();
        }
    }
}