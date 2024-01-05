namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        //for Dependency Id
        internal const int startDependencyId = 1000;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }

        //for task Id
        internal const int startTaskId = 10;
        private static int nextTaskId = startTaskId;
        internal static int NextTaskId { get => nextTaskId++; }

    }

    internal static List<DO.Chef> Chefs { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
    internal static List<DO.Dependency> Dependencys { get; } = new();
}
