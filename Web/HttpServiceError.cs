namespace error_handler.backend.Web
{
    public class HttpServiceError
    {
        public ServiceErrorModel ServiceErrorModel { get; set; }

        public Nancy.HttpStatusCode HttpStatusCode { get; set; }
    }
}