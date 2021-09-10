using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FinStat.Business.Http;
using FinStat.Domain.Enums;
using FinStat.Domain.Exceptions;
using FinStat.Domain.Http;
using FinStat.Domain.Interfaces.Configuration;
using FinStat.Domain.Interfaces.Http;
using FinStat.Domain.Interfaces.Services;
using FinStat.Domain.Interfaces.Utils;

namespace FinStat.Business.Utils
{
    public class WebServiceErrorHandler : IWebServiceErrorHandler
    {
        private const int TimeoutSeconds = 600;

        private readonly IConnectivityService _connectivityService;
        private readonly IApplicationSettings _applicationSettings;

        public WebServiceErrorHandler(
            IConnectivityService connectivityService,
            IApplicationSettings applicationSettings)
        {
            _connectivityService = connectivityService;
            _applicationSettings = applicationSettings;
        }

        public async Task<HttpRequestResult<T>> HandleResponseAsync<T>(Func<IWebClient, Task<T>> webServiceCall) where T : class
        {
            if (webServiceCall == null)
                throw new ArgumentNullException(nameof(webServiceCall));

            if (!_connectivityService.IsConnected)
                return new HttpRequestResult<T>(HttpRequestState.Offline);

            var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(TimeoutSeconds) };
            var webClient = new FinStatClient(httpClient, _applicationSettings.ApiKey);

            try
            {
                var payload = await webServiceCall(webClient).ConfigureAwait(false);
                return new HttpRequestResult<T>(HttpRequestState.Success, (int)HttpStatusCode.OK, payload);
            }
            catch (ApiException exception)
            {
                return new HttpRequestResult<T>(HttpRequestState.Error, exception.StatusCode);
            }
            catch (OperationCanceledException)
            {
                return new HttpRequestResult<T>(HttpRequestState.Canceled);
            }
            catch (Exception)
            {
                return new HttpRequestResult<T>(HttpRequestState.Error);
            }
            finally
            {
                httpClient.Dispose();
            }
        }
    }
}
