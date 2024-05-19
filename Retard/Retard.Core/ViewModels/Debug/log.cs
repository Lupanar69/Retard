using System.Diagnostics;


namespace Retard.Core.ViewModels.Tests
{
    /// <summary>
    /// Permet d'écrire dans la console plus facilement
    /// </summary>
    public static class log
    {
        /// <summary>
        /// Affiche un message dans la fenêtre Output
        /// </summary>
        /// <param name="msg">L'objet à afficher</param>
        [Conditional("ENABLE_LOGS")]
        public static void p(object msg)
        {
            Trace.WriteLine(msg);
        }
    }
}
