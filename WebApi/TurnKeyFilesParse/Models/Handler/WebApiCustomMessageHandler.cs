using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http.Hosting;

namespace Api.Models.Handler
{
    public class WebApiCustomMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request
            , CancellationToken cancellationToken)
        {

            HttpResponseMessage response
                = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                request.Properties.Remove(HttpPropertyKeys.NoRouteMatched);
                var errorResponse
                    = request.CreateResponse(response.StatusCode, "Content not found.");
                return errorResponse;
            }

            return response;
        }
    }
}