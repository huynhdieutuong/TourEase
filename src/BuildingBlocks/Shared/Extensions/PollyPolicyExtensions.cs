using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using Serilog;

namespace BuildingBlocks.Shared.Extensions;
public static class PollyPolicyExtensions
{
    public static IHttpClientBuilder UseImmediateHttpRetryPolicy(this IHttpClientBuilder builder, int retryCount = 3)
    => builder.AddPolicyHandler(ConfigureImmediateHttpRetry(retryCount));

    public static IHttpClientBuilder UseLinearHttpRetryPolicy(this IHttpClientBuilder builder, int retryCount = 3)
        => builder.AddPolicyHandler(ConfigureLinearHttpRetry(retryCount));

    public static IHttpClientBuilder UseExponentialHttpRetryPolicy(this IHttpClientBuilder builder, int retryCount = 3)
        => builder.AddPolicyHandler(ConfigureExponentialHttpRetry(retryCount));

    public static IHttpClientBuilder UseCircuitBreakerPolicy(this IHttpClientBuilder builder,
                                                             int eventAllowedBeforeBreaking = 5,
                                                             int fromSeconds = 30)
        => builder.AddPolicyHandler(ConfigureCircuitBreaker(eventAllowedBeforeBreaking, fromSeconds));

    public static IHttpClientBuilder ConfigureTimeoutPolicy(this IHttpClientBuilder builder, int seconds = 10)
        => builder.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(seconds));

    #region Configure
    private static IAsyncPolicy<HttpResponseMessage> ConfigureImmediateHttpRetry(int retryCount)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .RetryAsync(retryCount,
                (exception, retryCount, context) =>
                {
                    Log.Error($"Retry {retryCount} of {context.PolicyKey} at" +
                              $"{context.OperationKey}, due to: {exception.Exception.Message}");
                });
    }

    private static IAsyncPolicy<HttpResponseMessage> ConfigureLinearHttpRetry(int retryCount)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(5),
                (exception, retryCount, context) =>
                {
                    Log.Error($"Retry {retryCount} of {context.PolicyKey} at" +
                              $"{context.OperationKey}, due to: {exception.Exception.Message}");
                });
    }

    private static IAsyncPolicy<HttpResponseMessage> ConfigureExponentialHttpRetry(int retryCount)
    {
        // In this case will wait for
        //  2 ^ 1 = 2 seconds then
        //  2 ^ 2 = 4 seconds then
        //  2 ^ 3 = 8 seconds then
        //  2 ^ 4 = 16 seconds then
        //  2 ^ 5 = 32 seconds
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, retryCount, context) =>
                {
                    Log.Error($"Retry {retryCount} of {context.PolicyKey} at" +
                              $"{context.OperationKey}, due to: {exception.Exception.Message}");
                });
    }

    private static IAsyncPolicy<HttpResponseMessage> ConfigureCircuitBreaker(int eventAllowedBeforeBreaking, int fromSeconds)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(eventAllowedBeforeBreaking, TimeSpan.FromSeconds(fromSeconds));
    }
    #endregion
}
