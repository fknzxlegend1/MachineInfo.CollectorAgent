﻿#region License information

/*

Copyright 2022 Bogdan Bara

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

#endregion

using MachineInfo.System.Collectors;
using MachineInfo.System.Collectors.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MachineInfo.System
{
    public static class MicrosoftDependencyInjection
    {
        public static void AddSystemInfoCollector(this IServiceCollection services, Action<SystemInfoCollectorOptions> options = null)
        {
            services.Configure(options);

            services.AddSingleton<ICPUInfoCollector>((serviceProvider) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<CPUInfoCollector>>();
                return new CPUInfoCollector(logger);
            });

            services.AddSingleton<IDiskDriveInfoCollector>((serviceProvider) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<DiskDriveInfoCollector>>();
                return new DiskDriveInfoCollector(logger);
            });

            services.AddSingleton<IDiskPartitionInfoCollector>((serviceProvider) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<DiskPartitionInfoCollector>>();
                return new DiskPartitionInfoCollector(logger);
            });

            services.AddSingleton<IMemoryBankInfoCollector>((serviceProvider) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<MemoryBankInfoCollector>>();
                return new MemoryBankInfoCollector(logger);
            });

            services.AddSingleton<IMemoryInfoCollector>((serviceProvider) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<MemoryInfoCollector>>();
                return new MemoryInfoCollector(logger);
            });

            services.AddSingleton<IPlatformInfoCollector>((serviceProvider) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<PlatformInfoCollector>>();
                return new PlatformInfoCollector(logger);
            });

            services.AddSingleton<IVideoControllerInfoCollector>((serviceProvider) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<VideoControllerInfoCollector>>();
                return new VideoControllerInfoCollector(logger);
            });

            services.AddSingleton<ISystemInfoCollector>((serviceProvider) =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<SystemInfoCollector>>();

                var options = serviceProvider.GetRequiredService<IOptions<SystemInfoCollectorOptions>>().Value;

                var cpuMonitor = serviceProvider.GetService<ICPUInfoCollector>();
                var diskDriveMonitor = serviceProvider.GetService<IDiskDriveInfoCollector>();
                var diskPartitionMonitor = serviceProvider.GetService<IDiskPartitionInfoCollector>();
                var memoryBankMonitor = serviceProvider.GetService<IMemoryBankInfoCollector>();
                var memoryMonitor = serviceProvider.GetService<IMemoryInfoCollector>();
                var platformMonitor = serviceProvider.GetService<IPlatformInfoCollector>();
                var videoControllerMonitor = serviceProvider.GetService<IVideoControllerInfoCollector>();

                return new SystemInfoCollector(logger, options, cpuMonitor, diskDriveMonitor, diskPartitionMonitor, memoryBankMonitor, memoryMonitor, platformMonitor, videoControllerMonitor);
            });
        }
    }
}
