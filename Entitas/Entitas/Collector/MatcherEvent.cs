using System;

namespace Entitas {

    public struct MatcherEvent<TEntity> where TEntity : class, IEntityExt {

        public readonly IMatcher<TEntity> matcher;
        private GroupExt<TEntity> matchedGroup;
        public readonly EGroupEvent EGroupEvent;

        public MatcherEvent(IMatcher<TEntity> matcher, EGroupEvent eGroupEvent) {
            this.matcher = matcher;
            this.EGroupEvent = eGroupEvent;
            matchedGroup = null;
        }

        public GroupExt<TEntity> MatchedGroup
        {
            get
            {
                if(matchedGroup == null)
                    throw new Exception("Group Not Initialize");
                return matchedGroup;
            }
            set { matchedGroup = value; }
        }
    }
}
