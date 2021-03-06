﻿using System.Collections.Generic;

namespace Entitas {

    public interface ICollector {


        void Activate();
        void Deactivate();
        void ClearCollectedEntities();
    }

    public interface ICollector<TEntity> : ICollector where TEntity : class, IEntityExt {

        HashSet<TEntity> CollectedEntities { get; }
    }
}
