using System.Diagnostics;

namespace Tester.src.Microservices.System
{
    class PerformanceCounterWrapper
    {
        public PerformanceCounter PerformanceCounter { get; }

        public PerformanceCounterWrapper()
        {

        }
    }

    enum SystemMetricType
    {
        MEMORY,
        DISK,
        QUERY
    }
}
