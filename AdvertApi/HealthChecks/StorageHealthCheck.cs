using System;
using System.Threading;
using System.Threading.Tasks;
using AdvertApi.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.HealthChecks;

namespace AdvertApi.HealthChecks
{
    public class StorageHealthCheck: Microsoft.Extensions.Diagnostics.HealthChecks.IHealthCheck
    {
        private readonly IAdvertStorageService _storageService;

        public StorageHealthCheck(IAdvertStorageService storageService)
        {
            _storageService = storageService;
        }

        public async ValueTask<IHealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
        {
            var isStorageOk = await _storageService.CheckHealthAsync();
            return Microsoft.Extensions.HealthChecks.HealthCheckResult.FromStatus(isStorageOk ? CheckStatus.Healthy : CheckStatus.Unhealthy, "");
        }

        public async Task<Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isStorageOk = await _storageService.CheckHealthAsync();
            return isStorageOk ? Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy("A healthy result.") : Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Unhealthy("An unhealthy result.");
        }
    }
}
