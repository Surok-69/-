using System.Collections.Generic;

namespace Lab3
{
    public class WorkRepository
    {
        private readonly List<Work> _works = new List<Work>();

        public int Count => _works.Count;

        public void Add(Work work)
        {
            if (work == null) return;
            _works.Add(work);
        }

        public IReadOnlyList<Work> GetAll() => _works;

        public void Clear() => _works.Clear();
    }
}
