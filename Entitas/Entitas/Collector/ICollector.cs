using System.Collections.Generic;

namespace Entitas {

    public interface ICollector {

        int count { get; }

        void Activate();
        void Deactivate();
        void ClearCollectedEntities();

        IEnumerable<TCast> GetCollectedEntities<TCast>() where TCast : class, IEntityExt;
    }

    public interface ICollector<TEntity> : ICollector where TEntity : class, IEntityExt {

        HashSet<TEntity> collectedEntities { get; }
    }
}
