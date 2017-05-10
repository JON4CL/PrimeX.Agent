namespace SRM.Commons
{
    public static class SRMPaths
    {
        public static string SRMBase = (new JConfig("SRMPaths")).GetValueByKey("SRM_BASE");
        public static string SRMBin = (new JConfig("SRMPaths")).GetValueByKey("SRM_BIN");
        public static string SRMData = (new JConfig("SRMPaths")).GetValueByKey("SRM_DATA");
        public static string SRMConfig = (new JConfig("SRMPaths")).GetValueByKey("SRM_CONFIG");
        public static string SRMComponents = (new JConfig("SRMPaths")).GetValueByKey("SRM_COMPONENTS");
    }
}