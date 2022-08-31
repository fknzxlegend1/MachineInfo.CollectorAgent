﻿using MachineInfo.System.Collectors;
using MachineInfo.System.Collectors.Implementation;

namespace MachineInfo.System
{
    public static class SystemInfoCollectorFactory
    {
        private static ISystemInfoCollector systemMonitor;

        public static ISystemInfoCollector Create(Action<MachineInfoCollectorOptions> options = null)
        {
            if (systemMonitor != null)
                return systemMonitor;

            var collectorOptions = new MachineInfoCollectorOptions();
            options?.Invoke(collectorOptions);
            
            ICPUInfoCollector cpuMonitor = new CPUInfoCollector();
            IDiskDriveInfoCollector diskDriveMonitor = new DiskDriveInfoCollector();
            IDiskPartitionInfoCollector diskPartitionMonitor = new DiskPartitionInfoCollector();
            IMemoryBankInfoCollector memoryBankMonitor = new MemoryBankInfoCollector();
            IMemoryInfoCollector memoryMonitor = new MemoryInfoCollector();
            IPlatformInfoCollector platformMonitor = new PlatformInfoCollector();
            IVideoControllerInfoCollector videoControllerMonitor = new VideoControllerInfoCollector();

            systemMonitor = new SystemInfoCollector(collectorOptions,
                                                    cpuMonitor, 
                                                    diskDriveMonitor, 
                                                    diskPartitionMonitor, 
                                                    memoryBankMonitor, 
                                                    memoryMonitor, 
                                                    platformMonitor, 
                                                    videoControllerMonitor);

            return systemMonitor;
        }
    }
}
