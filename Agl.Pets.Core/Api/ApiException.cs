using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Agl.Pets.Core.Api
{
    [ExcludeFromCodeCoverage]
    class ApiException
        : Exception
    {
        public ApiException(string baseAddress, string relativeUrl, HttpStatusCode statusCode, string content, string contentType)
            : base(CreateMessage(baseAddress, relativeUrl, statusCode, content, contentType))
        {
            BaseAddress = baseAddress;
            RelativeUrl = relativeUrl;
            StatusCode = statusCode;
            Content = content;
            ContentType = contentType;
        }

        public string BaseAddress { get; }
        public string RelativeUrl { get; }
        public HttpStatusCode StatusCode { get; }
        public string Content { get; }
        public string ContentType { get; }

        private static string CreateMessage(string baseAddress, string relativeUrl, HttpStatusCode statusCode, string content, string contentType)
        {
            var sb = new StringBuilder();

            sb.Append("Api Exception -------------------------------------");
            sb.Append("BaseAddress: ").AppendLine(baseAddress);
            sb.Append("RelativeUrl: ").AppendLine(relativeUrl);
            sb.Append("ContentType: ").AppendLine(contentType);
            sb.Append("Content: ").AppendLine(content);
            sb.Append("StatusCode: ").AppendLine(statusCode.ToString());

            return sb.ToString();
        }
    }
}
