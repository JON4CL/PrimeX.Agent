using System.Reflection;
using log4net;

namespace SRM.Commons
{
    public static class JLogger
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void LogInfo(object o, string format, params object[] args)
        {
            if (o != null)
            {
                Log.InfoFormat(o.GetType().Namespace + "." + o.GetType().Name + " - " + format, args);
            }
        }

        public static void LogDebug(object o, string format, params object[] args)
        {
            if (o != null)
            {
                Log.DebugFormat(o.GetType().Namespace + "." + o.GetType().Name + " - " + format, args);
            }
        }

        public static void LogError(object o, string format, params object[] args)
        {
            if (o != null)
            {
                Log.ErrorFormat(o.GetType().Namespace + "." + o.GetType().Name + " - " + format, args);
            }
        }
    }
}