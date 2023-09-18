using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using FemsaKofSignedUR.Validators;
using FemsaKofSignedURL.Amazon;
using FemsaKofSignedURL.Amazon.Abstractions;
using FemsaKofSignedURL.Models;
using FemsaKofSignedURL.Validators.Abstractions;
using Newtonsoft.Json;
using static FemsaKofSignedURL.Constants;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace FemsaKofSignedURL;

public class Function
{    
    private readonly IRequestValidator _requestValidator;
    private readonly IAmazonS3Methods _amazonS3Methods;

    public Function()
    {                  
        _requestValidator = new RequestValidator();
        _amazonS3Methods = new AmazonS3Methods();
    }

    /// <summary>
    /// Esta función devuelve una URL firmada para que KOF pueda subir un archivo al bucket de premia
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
    {
        try
        {
            var requestBody = JsonConvert.DeserializeObject<RequestBody>(request.Body);          

            if(!_requestValidator.IsValidRequest(requestBody, out string message))
                return ReturnBadRequest(message);

            if (!_requestValidator.IsValidFileExtension(requestBody.FileName, out string contentType))
                return ReturnBadRequest(contentType);

            var signedUrl = await _amazonS3Methods.GenerateSignedUrl(requestBody.FileName, contentType);

            if (!string.IsNullOrEmpty(signedUrl))
            {
                var responseBody = new ResponseBody(signedUrl, requestBody.FileName, contentType);

                context.Logger.Log($"La respuesta con la URL firmada es: {JsonConvert.SerializeObject(responseBody)} " +
                    $" para el archivo {requestBody.FileName}");

                return ReturnOk(JsonConvert.SerializeObject(responseBody));
            }

            return ReturnBadRequest(RequestValidation.InvalidBucketName);            
           
        }
        catch (Exception ex)
        {
            context.Logger.LogError($"Error en la lambda.  Detalle: {ex.Message}");

            ErrorResponse response = new ErrorResponse(ex.Message.ToString());             
            return ReturnInternalServerError(JsonConvert.SerializeObject(response));
        }
    }

    private APIGatewayProxyResponse ReturnOk(string body) => new () 
    { 
        StatusCode = (int)HttpStatusCode.OK,
        Headers = new Dictionary<string, string> { { "Access-Control-Allow-Origin", "*" } },
        Body = body
    };

    private APIGatewayProxyResponse ReturnInternalServerError(string body) => new ()
    {
        StatusCode = (int)HttpStatusCode.InternalServerError,        
        Body = body
    };

    private APIGatewayProxyResponse ReturnBadRequest(string body) => new() 
    { 
        StatusCode = (int)HttpStatusCode.BadRequest, 
        Body = body 
    };
}

