using System.Collections.Generic;

namespace Entitas
{
    /// Automatic EntityExt Reference Counting (AERC)
    /// is used internally to prevent pooling retained entities.
    /// If you use retain manually you also have to
    /// release it manually at some point.
    /// SafeAERC checks if the entity has already been
    /// retained or released. It's slower, but you keep the information
    /// about the owners.
    public sealed class SafeAERC : IAERC
    {
        readonly EntityExt _entity;
        readonly HashSet<object> _owners = new HashSet<object>();

        public SafeAERC(EntityExt entity)
        {
            _entity = entity;
        }

        public HashSet<object> owners
        {
            get { return _owners; }
        }

        public int RetainCount
        {
            get { return _owners.Count; }
        }

        public void Retain(object owner, bool throwIfRepeated=true)
        {
            if (!owners.Add(owner) && throwIfRepeated)
            {
                throw new EntityIsAlreadyRetainedByOwnerException(_entity, owner);
            }
        }

        public void InternalRelease(object owner, bool throwIfNotExisted=true)
        {
            if (!owners.Remove(owner) && throwIfNotExisted)
            {
                throw new EntityIsNotRetainedByOwnerException(_entity, owner);
            }
        }

      
    }
}