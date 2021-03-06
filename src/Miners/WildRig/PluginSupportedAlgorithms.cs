﻿using NHM.Common;
using NHM.Common.Algorithm;
using NHM.Common.Enums;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WildRig
{
    // TODO move this into PluginBase when we break 3.x plugins with monero fork
    internal static class PluginSupportedAlgorithms
    {
        internal static bool UnsafeLimits(string PluginUUID)
        {
            try
            {
                var unsafeLimits = Path.Combine(Paths.MinerPluginsPath(), PluginUUID, "unsafe_limits");
                return File.Exists(unsafeLimits);
            }
            catch
            { }
            return false;
        }

        internal static Dictionary<DeviceType, List<AlgorithmType>> SupportedDevicesAlgorithmsDict()
        {
            var amdAlgos = new HashSet<AlgorithmType>(GetSupportedAlgorithmsAMD("").SelectMany(a => a.IDs)).ToList();
            var ret = new Dictionary<DeviceType, List<AlgorithmType>>
            {
                { DeviceType.AMD, amdAlgos },
            };
            return ret;
        }

        internal static List<Algorithm> GetSupportedAlgorithmsAMD(string PluginUUID)
        {
            var algorithms = new List<Algorithm>
            {
                new Algorithm(PluginUUID, AlgorithmType.Lyra2REv3),
                new Algorithm(PluginUUID, AlgorithmType.X16R),
                new Algorithm(PluginUUID, AlgorithmType.X16Rv2)
            };
            return algorithms;
        }

        internal static string AlgorithmName(AlgorithmType algorithmType)
        {
            switch (algorithmType)
            {
                case AlgorithmType.Lyra2REv3: return "lyra2v3";
                case AlgorithmType.X16R: return "x16r";
                case AlgorithmType.X16Rv2: return "x16rv2";
                default: return "";
            }
        }

        internal static double DevFee(AlgorithmType algorithmType)
        {
            return 2.0;
        }
    }
}
