using System;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;

namespace ProductAPI.VSA.Common
{
    public static class PolicyConfig
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(30, attempt)),
                    onRetry: (outcome, timespan, attempt, context) =>
                    {
                        Console.WriteLine($"Retry {attempt} after {timespan.TotalSeconds}s due to {(outcome.Exception != null ? outcome.Exception.Message : outcome.Result.StatusCode.ToString())}");
                    }
                );

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30),
                    onBreak: (outcome, breakDelay) =>
                    {
                        Console.WriteLine($"Circuit broken for {breakDelay.TotalSeconds}s due to {(outcome.Exception != null ? outcome.Exception.Message : outcome.Result.StatusCode.ToString())}");
                    },
                    onReset: () => Console.WriteLine("Circuit reset"),
                    onHalfOpen: () => Console.WriteLine("Circuit half-open: next call is a trial")
                );
    }
}