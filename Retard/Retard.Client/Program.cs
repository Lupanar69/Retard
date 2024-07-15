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

            using App game = new();
            game.Run();
        }
    }
}