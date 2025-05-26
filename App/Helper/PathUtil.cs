namespace App.Helper
{
    public static class PathUtil
    {
        public static string ResolveFromProjectRoot()
        {
            var baseDir = AppContext.BaseDirectory;

            if (baseDir.Contains(Path.Combine("bin", "Debug")) || baseDir.Contains(Path.Combine("bin", "Release")))
            {
                baseDir = Path.GetFullPath(Path.Combine(baseDir, "..", "..", ".."));
            }

            return Path.Combine(baseDir, "Data", "jobs.json");
        }
    }
}
