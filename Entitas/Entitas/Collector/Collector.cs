using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entitas
{
    /// A Collector can observe one or more groups from the same context
    /// and collects changed entities based on the specified groupEvent.
    public class Collector<TEntity> : ICollector<TEntity> where TEntity : class, IEntityExt
    {
        readonly HashSet<TEntity> _collectedEntities;
        readonly EGroupEvent[] _eGroupEvents;
        readonly IGroup<TEntity>[] _groups;

        GroupChanged<TEntity> _addEntityCache;
        StringBuilder _toStringBuilder;
        string _toStringCache;

        /// Creates a Collector and will collect changed entities
        /// based on the specified groupEvent.
        public Collector(IGroup<TEntity> group, EGroupEvent eGroupEvent) : this(new[] {group}, new[] {eGroupEvent})
        {
        }

        /// Creates a Collector and will collect changed entities
        /// based on the specified groupEvents.
        public Collector(IGroup<TEntity>[] groups, EGroupEvent[] eGroupEvents)
        {
            _groups            = groups;
            _collectedEntities = new HashSet<TEntity>(EntityEqualityComparer<TEntity>.comparer);
            _eGroupEvents      = eGroupEvents;

            if (groups.Length != eGroupEvents.Length)
            {
                throw new CollectorException(
                    "Unbalanced count with groups (" + groups.Length + ") and group events (" + eGroupEvents.Length +
                    ").", "Group and group events count must be equal.");
            }

            _addEntityCache = addEntity;
            Activate();
        }

        /// Returns all collected entities.
        /// Call collector.ClearCollectedEntities()
        /// once you processed all entities.
        public HashSet<TEntity> collectedEntities
        {
            get { return _collectedEntities; }
        }

        /// Returns the number of all collected entities.
        public int count
        {
            get { return _collectedEntities.Count; }
        }

        /// Activates the Collector and will start collecting
        /// changed entities. Collectors are activated by default.
        public void Activate()
        {
            for (int i = 0; i < _groups.Length; i++)
            {
                var group      = _groups[i];
                var groupEvent = _eGroupEvents[i];
                switch (groupEvent)
                {
                    case EGroupEvent.Added:
                        group.OnEntityAddedWithComp -= _addEntityCache;
                        group.OnEntityAddedWithComp += _addEntityCache;
                        break;
                    case EGroupEvent.Removed:
                        group.OnEntityRemovedWithCmp -= _addEntityCache;
                        group.OnEntityRemovedWithCmp += _addEntityCache;
                        break;
                    case EGroupEvent.AddedOrRemoved:
                        group.OnEntityAddedWithComp  -= _addEntityCache;
                        group.OnEntityAddedWithComp  += _addEntityCache;
                        group.OnEntityRemovedWithCmp -= _addEntityCache;
                        group.OnEntityRemovedWithCmp += _addEntityCache;
                        break;
                }
            }
        }

        /// Deactivates the Collector.
        /// This will also clear all collected entities.
        /// Collectors are activated by default.
        public void Deactivate()
        {
            for (int i = 0; i < _groups.Length; i++)
            {
                var group = _groups[i];
                group.OnEntityAddedWithComp  -= _addEntityCache;
                group.OnEntityRemovedWithCmp -= _addEntityCache;
            }

            ClearCollectedEntities();
        }

        /// Returns all collected entities and casts them.
        /// Call collector.ClearCollectedEntities()
        /// once you processed all entities.
        public IEnumerable<TCast> GetCollectedEntities<TCast>() where TCast : class, IEntityExt
        {
            return _collectedEntities.Cast<TCast>();
        }

        /// Clears all collected entities.
        public void ClearCollectedEntities()
        {
            foreach (var entity in _collectedEntities)
            {
                entity.Release(this);
            }

            _collectedEntities.Clear();
        }

        void addEntity(IGroup<TEntity> group, TEntity entity, int index, IComponent component)
        {
            var added = _collectedEntities.Add(entity);
            if (added)
            {
                entity.Retain(this);
            }
        }

        public override string ToString()
        {
            if (_toStringCache == null)
            {
                if (_toStringBuilder == null)
                {
                    _toStringBuilder = new StringBuilder();
                }

                _toStringBuilder.Length = 0;
                _toStringBuilder.Append("Collector(");

                const string separator     = ", ";
                var          lastSeparator = _groups.Length - 1;
                for (int i = 0; i < _groups.Length; i++)
                {
                    _toStringBuilder.Append(_groups[i]);
                    if (i < lastSeparator)
                    {
                        _toStringBuilder.Append(separator);
                    }
                }

                _toStringBuilder.Append(")");
                _toStringCache = _toStringBuilder.ToString();
            }

            return _toStringCache;
        }

        ~Collector()
        {
            Deactivate();
        }
    }
}