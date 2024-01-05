namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        internal const int startDependencyId = 1000;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }

    }
    internal static List<DO.Chef> Chefs { get; } = new();
    internal static List<DO.Dependency> Dependencys { get; } = new();
}
