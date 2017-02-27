using System;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;

namespace error_handler.backend.Web
{
    /// <summary>
    /// Логирование системных ошибок
    /// </summary>
    /// <remarks>
    /// Возможно, для ускорения работы, класс стоит сделать статическим
    /// </remarks>
    /// <see cref="https://github.com/bytefish/NancySamples/tree/master/ErrorHandling/RestSample.Server"/>
    public class ErrorHandler
    {
        private ILogger _logger;

        public ErrorHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ErrorHandler>();
        }

        public void Enable(IPipelines pipelines, IResponseNegotiator responseNegotiator)
        {
            if (pipelines == null)
                throw new ArgumentNullException("pipelines");

            if (responseNegotiator == null)
                throw new ArgumentNullException("responseNegotiator");

            pipelines.OnError += (context, exception) => HandleException(context, exception, responseNegotiator);
        }

        private void LogException(NancyContext context, Exception exception)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError("An exception occured during processing a request. (Exception={0}).", exception);
        }

        private Response HandleException(NancyContext context, Exception exception, IResponseNegotiator responseNegotiator)
        {
            LogException(context, exception);

            return CreateNegotiatedResponse(context, responseNegotiator, exception);
        }

        private Response CreateNegotiatedResponse(NancyContext context, IResponseNegotiator responseNegotiator, Exception exception)
        {
            HttpServiceError httpServiceError = HttpServiceErrorUtilities.ExtractFromException(exception, HttpServiceErrorDefinition.GeneralError);

            Negotiator negotiator = new Negotiator(context)
                .WithStatusCode(httpServiceError.HttpStatusCode)
                .WithModel(httpServiceError.ServiceErrorModel);

            return responseNegotiator.NegotiateResponse(negotiator, context);
        }
    }
}
