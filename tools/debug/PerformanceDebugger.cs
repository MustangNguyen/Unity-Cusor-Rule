using System;
using UnityEngine;

namespace YourProject.Core.Debug
{
    /// <summary>
    /// Advanced debug cho performance metrics và monitoring
    /// </summary>
    public class PerformanceDebugger
    {
        private const string TAG = "[PerformanceDebugger]";
        private readonly IDebugLogger logger;
        
        // Performance tracking
        private float lastFrameTime;
        private int frameCount;
        private float fpsUpdateInterval = 1.0f;
        private float fpsAccumulator;
        private float fpsTimeLeft;

        public PerformanceDebugger(IDebugLogger debugLogger)
        {
            logger = debugLogger ?? throw new ArgumentNullException(nameof(debugLogger));
        }

        /// <summary>
        /// Log frame time nếu vượt quá threshold
        /// </summary>
        /// <param name="maxFrameTime">Threshold frame time (default: 33ms cho 30fps)</param>
        public void LogFrameTime(float maxFrameTime = 0.033f)
        {
            lastFrameTime = Time.deltaTime;
            logger.LogIf(lastFrameTime > maxFrameTime, TAG, 
                $"Frame time: {lastFrameTime:F3}s (Target: {maxFrameTime:F3}s) - FPS: {1f/lastFrameTime:F1}");
        }

        /// <summary>
        /// Log memory usage nếu vượt quá threshold
        /// </summary>
        /// <param name="maxMemoryMB">Threshold memory in MB (default: 100MB)</param>
        public void LogMemoryUsage(float maxMemoryMB = 100f)
        {
            long memory = System.GC.GetTotalMemory(false);
            float memoryMB = memory / (1024f * 1024f);
            
            logger.LogIf(memoryMB > maxMemoryMB, TAG, 
                $"Memory usage: {memoryMB:F1}MB (Threshold: {maxMemoryMB}MB)");
        }

        /// <summary>
        /// Log FPS trung bình
        /// </summary>
        public void LogAverageFPS()
        {
            fpsTimeLeft -= Time.deltaTime;
            fpsAccumulator += Time.timeScale / Time.deltaTime;
            frameCount++;

            if (fpsTimeLeft <= 0.0f)
            {
                float fps = fpsAccumulator / frameCount;
                logger.Log(TAG, $"Average FPS: {fps:F1}");
                
                fpsTimeLeft = fpsUpdateInterval;
                fpsAccumulator = 0.0f;
                frameCount = 0;
            }
        }

        /// <summary>
        /// Log draw calls và batches
        /// </summary>
        public void LogRenderingStats()
        {
#if ENABLE_DEBUG_LOGS && UNITY_EDITOR
            var stats = UnityEditor.UnityStats;
            logger.Log(TAG, $"Draw Calls: {stats.drawCalls}, Batches: {stats.batches}");
#endif
        }

        /// <summary>
        /// Log physics performance
        /// </summary>
        public void LogPhysicsStats()
        {
            logger.LogIf(Time.fixedDeltaTime > 0.02f, TAG, 
                $"Physics timestep: {Time.fixedDeltaTime:F3}s (Target: 0.02s)");
        }

        /// <summary>
        /// Log garbage collection info
        /// </summary>
        public void LogGarbageCollection()
        {
            long beforeGC = System.GC.GetTotalMemory(false);
            System.GC.Collect();
            long afterGC = System.GC.GetTotalMemory(false);
            long collected = beforeGC - afterGC;
            
            logger.LogIf(collected > 1024 * 1024, TAG, 
                $"GC collected: {collected / (1024f * 1024f):F1}MB");
        }

        /// <summary>
        /// Log tất cả performance metrics
        /// </summary>
        public void LogAllMetrics()
        {
            LogFrameTime();
            LogMemoryUsage();
            LogAverageFPS();
            LogRenderingStats();
            LogPhysicsStats();
        }
    }
}
