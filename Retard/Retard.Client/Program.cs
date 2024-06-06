namespace Retard.Client
{
    /// <summary>
    /// Lance le jeu
    /// </summary>
    internal static class Program
    {
        private static void Main(string[] args)
        {
            // Lance le jeu

            using App game = new();
            game.Run();
        }
    }
}