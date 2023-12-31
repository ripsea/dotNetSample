﻿using Api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace TurnKeyFilesParse.Models.Handler
{
    public class WrappingHandler : DelegatingHandler
    {
        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            return BuildApiResponse(request, response);
        }

        private static HttpResponseMessage BuildApiResponse(
            HttpRequestMessage request,
            HttpResponseMessage response)
        {
            object content;
            string errorMessage = null;

            if (response.TryGetContentValue(out content)
                && !response.IsSuccessStatusCode)
            {
                HttpError error = content as HttpError;

                if (error != null)
                {
                    content = null;

                    errorMessage = "System.err.";

#if DEBUG
                    errorMessage = string.Concat(errorMessage, error.ExceptionMessage, error.StackTrace);
                    Logger.Warn(errorMessage);
#else
                    Logger.Warn(error.ExceptionMessage);
#endif
                }
            }

            var newResponse = request
                .CreateResponse(
                    response.StatusCode,
                    new ApiResponse(response.StatusCode, content, errorMessage));

            foreach (var header in response.Headers)
            {
                newResponse.Headers.Add(header.Key, header.Value);
            }

            return newResponse;
        }
    }
}
