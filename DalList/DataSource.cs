

namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        internal const int startChefId = 1000;
        private static int nextChefId = startChefId;
        internal static int NextChefId { get => nextCourseId++; }

    }
    internal static List<DO.Chef> Chefs { get; } = new();
}
