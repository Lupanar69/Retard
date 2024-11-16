using System.Diagnostics;

namespace Retard.Core.ViewModels.Utilities
{
    public static class Log
    {
        /// <summary>
        /// Affiche un message dans la fenêtre de sortie
        /// </summary>
        /// <param name="msg">Le message à afficher</param>
        [Conditional("ENABLE_LOGS")]
        public static void p(string msg)
        {
            Trace.WriteLine(msg);
        }

        /// <summary>
        /// Affiche un message dans la fenêtre de sortie
        /// </summary>
        /// <param name="msg">Le message à afficher</param>
        [Conditional("ENABLE_LOGS")]
        public static void p2(params string[] msgs)
        {
            if (msgs.Length == 0)
                return;

            Trace.Write(msgs[0]);

            for (int i = 1; i < msgs.Length; ++i)
            {
                Trace.Write($" ; {msgs[i]}");
            }

            Trace.WriteLine("");
        }
    }
}
