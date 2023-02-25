namespace MiniORM
{
    internal class ChangeTracker<T>
    where T : class, new()
    {
        private readonly IList<T> allEntities;
        private readonly IList<T> added;
        private readonly IList<T> removed;

        public ChangeTracker(IEnumerable<T> allEntities)
        {
            this.allEntities = CloneEntities(allEntities);
        }
    }
}