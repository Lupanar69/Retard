using System.Diagnostics;
using System.Text;

namespace Retard.Core.ViewModels.Tests
{
    /// <summary>
    /// Permet d'écrire dans la console plus facilement
    /// </summary>
    public static class log
    {
        #region Variables statiques

        /// <summary>
        /// Pour concaténer les messages
        /// </summary>
        private static readonly StringBuilder _sb = new();

        #endregion

        /// <summary>
        /// Affiche un message dans la fenêtre Output
        /// </summary>
        /// <param name="msg">L'objet à afficher</param>
        [Conditional("ENABLE_LOGS")]
        public static void p(object msg)
        {
            Trace.WriteLine(msg);
        }

        /// <summary>
        /// Affiche un message dans la fenêtre Output
        /// </summary>
        /// <param name="msg">Les objets à afficher</param>
        [Conditional("ENABLE_LOGS")]
        public static void p2(params object[] args)
        {
            log._sb.EnsureCapacity(args.Length * 150);

            log._sb.Append($"{args[0].ToString()}");

            for (int i = 1; i < args.Length; ++i)
            {
                log._sb.Append($" ; {args[i].ToString()}");
            }

            Trace.WriteLine(log._sb.ToString());
        }
    }
}
