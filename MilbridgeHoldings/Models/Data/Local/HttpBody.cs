using MilbridgeHoldings.Models.Data.Local;
using System.ComponentModel.DataAnnotations;

namespace MilbridgeHoldings.Models.Local
{
    public class HttpBody<T>
    {
        [Url, Required(ErrorMessage = "Url is required")]
        public string? Url { get; set; }

        [Required(ErrorMessage = "Content type is required")]
        public HttpContentType? ContentType { get; set; }

        [Required(ErrorMessage = "Method is required")]
        public HttpMethod? Method { get; set; }

        public List<HttpHeader>? Header { get; set; }

        public T? Body { get; set; }
    }
}
