namespace FemsaKofSignedURL
{
    public class Constants
    {
        public class RequestValidation
        {
            public const string InvalidFileName = "El json de la solicitud debe contener un nombre de archivo definido en el atributo 'FileName'.";
            public const string InvalidFile = "El nombre del archivo tiene caracteres especiales y/o no tiene una extensión válida";
            public const string InvalidNameExtension = "Solo puede subir archivos con extensión csv o json";
            public const string InvalidBucketName = "El nombre del bucket S3 es inválido";
            public const string Success = "OK";
        }
       
        public const string pattern = @"^[a-zA-Z0-9_\-]+\.[a-zA-Z]{3}$";     
    }
}
