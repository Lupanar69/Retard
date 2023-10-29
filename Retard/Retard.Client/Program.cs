namespace Retard.Client
{
    /// <summary>
    /// Lance le jeu
    /// </summary>
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using GameRunner game = new();
            game.Run();
        }
    }
}
