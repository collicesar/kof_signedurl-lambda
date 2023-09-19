# kof_signedurl-lambda

Lambda que retorna una URL firmada para subir un archivo al bucket de S3 en AWS.

Nota:  

Necesita tener configurado 2 variables de entorno

1. Variable : BucketName          Contenido:  Nombre del bucket S3 en AWS
2. Variable : ExpiredMinutesTime  Contenido:  Tiempo en minutos que durará la URL firmada antes de expirar.
