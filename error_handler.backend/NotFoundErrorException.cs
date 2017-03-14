using System;
using error_handler.backend.Web;

namespace error_handler.backend
{
    public class NotFoundErrorException : Exception, IHasHttpServiceError
    {
        public NotFoundErrorException()
            : base()
        { }

        public NotFoundErrorException(string message)
            : base(message)
        { }

        public NotFoundErrorException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public HttpServiceError HttpServiceError
        {
            get { return HttpServiceErrorDefinition.NotFoundError; }
        }
    }
}