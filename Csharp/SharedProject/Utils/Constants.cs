using System.Text.RegularExpressions;

namespace SharedProject.Utils
{
    public static class Constants
    {
        public const int MaxByte = 1024;
        public const string LocalHost = "127.0.0.1";
        public const string Port = "1234";
        public static readonly Regex RequestRegex = new Regex(@"^[a-z]*:{.*}$");
    }
}