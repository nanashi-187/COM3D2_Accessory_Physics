using BepInEx.Logging;

namespace COM3D2.AccessoryPhysics
{
    internal static class Log
    {
        private static ManualLogSource _source;

        public static void Init(ManualLogSource source)
        {
            _source = source;
        }

        public static void Info(string message)
        {
            if (_source != null)
            {
                _source.LogInfo(message);
            }
        }

        public static void Warning(string message)
        {
            if (_source != null)
            {
                _source.LogWarning(message);
            }
        }
    }
}
