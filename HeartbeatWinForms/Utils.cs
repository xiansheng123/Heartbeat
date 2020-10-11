using System.IO;

namespace HeartbeatWinForms
{
    public static class Utils
    {
        public static void DeleteFile(string fullFileName)
        {
            if (File.Exists(fullFileName))
            {
                File.Delete(fullFileName);
            }
        }
    }
}