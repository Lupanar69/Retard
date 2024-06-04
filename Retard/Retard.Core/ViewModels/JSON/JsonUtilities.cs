using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Retard.Core.ViewModels.JSON
{
    /// <summary>
    /// Utilisée pour lire et écrire dans des fichiers Json
    /// </summary>
    public static class JsonUtilities
    {
        #region Méthodes publiques

        /// <summary>
        /// Crée le chemin d'accès s'il n'existe pas
        /// </summary>
        /// <param name="fullPath">Le chemin d'accès complet</param>
        public static void CreatPathIfNotExists(string fullPath)
        {
            try
            {
                FileInfo fileInfo = new(fullPath);

                if (!fileInfo.Directory.Exists)
                {
                    fileInfo.Directory.Create();
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Récupère le contenu d'un fichier
        /// </summary>
        /// <param name="path">Le chemin d'accès au fichier Json</param>
        public static string ReadFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Ecrit dans un fichier
        /// </summary>
        /// <param name="content">La donnée à écrire</param>
        /// <param name="path">Le chemin d'accès au fichier Json</param>
        public static void WriteToFile(string content, string path)
        {
            try
            {
                File.WriteAllText(path, content, Encoding.Unicode);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Convertit la donnée en chaîne Json
        /// </summary>
        /// <param name="data">La donnée à écrire</param>
        public static string SerializeObject(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Convertit la chaîne Json en objet
        /// </summary>
        /// <param name="json">Le contenu Json à convertir</param>
        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Convertit la chaîne Json en JObject
        /// </summary>
        /// <param name="json">Le contenu Json à convertir</param>
        public static JObject Parse(string json)
        {
            return JObject.Parse(json);
        }

        #endregion
    }
}
