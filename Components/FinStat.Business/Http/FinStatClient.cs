using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FinStat.Domain.Exceptions;
using FinStat.Domain.Models;
using Newtonsoft.Json;

namespace FinStat.Business.Http
{
    public class FinStatClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _apiKey;

        public FinStatClient(HttpClient httpClient, string baseUrl, string apiKey)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            _apiKey = apiKey;
        }

        public Task<IncomeStatement[]> GetIncomeStatement(string ticker, bool isQuarterPeriod, int limit, CancellationToken cancellationToken = default)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(_baseUrl.TrimEnd('/')).Append("/api/v3/income-statement/{ticker}?limit={limit}");
            urlBuilder.Append($"&apikey={_apiKey}");
            urlBuilder.Replace("{ticker}", Uri.EscapeDataString(ConvertToString(ticker, CultureInfo.InvariantCulture)));
            urlBuilder.Replace("{limit}", Uri.EscapeDataString(ConvertToString(limit, CultureInfo.InvariantCulture)));
            if (isQuarterPeriod)
            {
                urlBuilder.Append("&period=quarter");
            }

            return SendRequestAsync<IncomeStatement[]>(urlBuilder, cancellationToken);
        }

        private async Task<T> SendRequestAsync<T>(StringBuilder urlBuilder, CancellationToken cancellationToken)
        {
            var client = _httpClient;
            using (var request = new HttpRequestMessage())
            {
                request.Method = new HttpMethod("GET");
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                var url = urlBuilder.ToString();
                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);

                try
                {
                    var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
                    if (response.Content != null && response.Content.Headers != null)
                    {
                        foreach (var item in response.Content.Headers)
                        {
                            headers[item.Key] = item.Value;
                        }
                    }

                    var status = (int)response.StatusCode;
                    if (status == 200)
                    {
                        var objectResponse = await ReadObjectResponseAsync<T>(response, headers).ConfigureAwait(false);
                        if (objectResponse.Result == null)
                        {
                            throw new ApiException("Response was null which was not expected.", status, objectResponse.Text, headers, null);
                        }

                        return objectResponse.Result;
                    }
                    else
                    {
                        var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ApiException($"The HTTP status code of the response was not expected ({status}).", status, responseData, headers, null);
                    }
                }
                finally
                {
                    response.Dispose();
                }
            }
        }

        private async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(HttpResponseMessage response, IReadOnlyDictionary<string, IEnumerable<string>> headers)
        {
            if (response?.Content == null)
            {
                return new ObjectResponseResult<T>(default, string.Empty);
            }

            try
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                using (var streamReader = new StreamReader(responseStream))
                using (var jsonTextReader = new JsonTextReader(streamReader))
                {
                    var serializer = JsonSerializer.Create();
                    var typedBody = serializer.Deserialize<T>(jsonTextReader);
                    return new ObjectResponseResult<T>(typedBody, string.Empty);
                }
            }
            catch (JsonException exception)
            {
                var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
            }
        }

        private string ConvertToString(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is Enum)
            {
                var name = Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = value.GetType().GetTypeInfo().GetDeclaredField(name);
                    if (field != null)
                    {
                        if (field.GetCustomAttribute(typeof(EnumMemberAttribute)) is EnumMemberAttribute attribute)
                        {
                            return attribute.Value ?? name;
                        }
                    }

                    var converted = Convert.ToString(Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                    return converted;
                }
            }
            else if (value is bool x)
            {
                return Convert.ToString(x, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[] bytes)
            {
                return Convert.ToBase64String(bytes);
            }
            else if (value.GetType().IsArray)
            {
                var array = ((Array)value).OfType<object>();
                return string.Join(",", array.Select(o => ConvertToString(o, cultureInfo)));
            }

            var result = Convert.ToString(value, cultureInfo);
            return result ?? string.Empty;
        }
    }
}
