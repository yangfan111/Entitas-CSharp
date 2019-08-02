using System;
using System.Collections.Generic;

namespace Entitas {

    public class EntityEqualityComparer<T> : IEqualityComparer<T> where T:IEntityExt {

        public static readonly EntityEqualityComparer<T> Comparer = new EntityEqualityComparer<T>();

    
        public bool Equals(EntityExt x, EntityExt y)
        {
            return x == y;
        }

        public int GetHashCode(EntityExt obj)
        {
            return obj.CreationIndex;
        }

        public bool Equals(T x, T y)
        {
          return  Object.Equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.CreationIndex;
        }
    }
}
