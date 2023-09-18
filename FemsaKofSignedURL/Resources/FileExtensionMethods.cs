using FemsaKofSignedURL.Helpers;
using static FemsaKofSignedURL.Constants;

namespace FemsaKofSignedURL.Resources
{
    public static class FileExtensionMethods
    {
        public static string ToGetFileExtension(this string fileName)
        {                
            return  GetFileExtensionFromFileName(fileName).ToLower();
        }

        public static string ToGetContentType(this string fileExtension)
        {    
            return Helper.fileExtensions.ContainsKey(fileExtension) ? Helper.fileExtensions[fileExtension] : RequestValidation.InvalidNameExtension;
        }
        
        private static string GetFileExtensionFromFileName(string fileName)
        {
            // Encuentra la última aparición de un punto en el nombre del archivo
            int lastDotIndex = fileName.LastIndexOf('.');

            // Verifica si se encontró un punto y si no está al final del nombre del archivo
            if (lastDotIndex >= 0 && lastDotIndex < fileName.Length - 1)
                // Extrae la extensión a partir del último punto
                return fileName.Substring(lastDotIndex + 1);

            // Si no se encontró un punto o no está al final del nombre, retorna una extensión vacía
            return string.Empty;
        }
    }
}
