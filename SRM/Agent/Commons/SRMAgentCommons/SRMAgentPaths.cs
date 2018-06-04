using SRM.Commons;

namespace SRM.Agent.Commons
{
    public static class SRMAgentPaths
    {
        public static readonly string SRMBase = (new JConfig("SRMPaths")).GetValueByKey("SRM_BASE");
        public static readonly string SRMBin = (new JConfig("SRMPaths")).GetValueByKey("SRM_BIN");
        public static readonly string SRMData = (new JConfig("SRMPaths")).GetValueByKey("SRM_DATA");
        public static readonly string SRMConfig = (new JConfig("SRMPaths")).GetValueByKey("SRM_CONFIG");
        public static readonly string SRMComponents = (new JConfig("SRMPaths")).GetValueByKey("SRM_COMPONENTS");
        public static readonly string SRMFacts = (new JConfig("SRMPaths")).GetValueByKey("SRM_FACTS");
    }
}