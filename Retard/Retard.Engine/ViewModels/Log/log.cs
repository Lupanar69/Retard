using System.Diagnostics;
using System.Text;

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

        /// <summary>
        /// Affiche un message dans la fenêtre Output
        /// </summary>
        /// <param name="msg">Les objets à afficher</param>
        [Conditional("ENABLE_LOGS")]
        public static void p2(params object[] args)
        {
            StringBuilder sb = new(args.Length * (150));

            sb.Append($"{args[0].ToString()}");

            for (int i = 1; i < args.Length; i++)
            {
                sb.Append($" ; {args[i].ToString()}");
            }

            Trace.WriteLine(sb.ToString());
        }
    }
}
