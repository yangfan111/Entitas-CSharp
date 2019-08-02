namespace Entitas
{
    public partial class Matcher<TEntity> : IAllOfMatcher<TEntity> where TEntity : class, IEntityExt
    {

        int[] indices;

        Matcher()
        {
        }

        public string[] componentNames { get; set; }

        public int[] Indices
        {
            get
            {
                if (indices == null)
                {
                    indices = mergeIndices(AllOfIndices, AnyOfIndices, NoneOfIndices);
                }

                return indices;
            }
        }
        //缓存需要搜索的索引值，做Entity上查询使用
        public int[] AllOfIndices { get; private set; }
       
        public int[] AnyOfIndices { get; private set; }
        

        public int[] NoneOfIndices { get; private set; }

        IMatcher<TEntity> IAllOfMatcher<TEntity>.AnyOf(params int[] indices)
        {
            AnyOfIndices = distinctIndices(indices);
            indices      = null;
            _isHashCached = false;
            return this;
        }

        IMatcher<TEntity> IAllOfMatcher<TEntity>.AnyOf(params IMatcher<TEntity>[] matchers)
        {
            return ((IAllOfMatcher<TEntity>) this).AnyOf(mergeIndices(matchers));
        }

        public bool Matches(TEntity entity)
        {
            var entityExt = entity as EntityExt;
            return (AllOfIndices == null || entityExt.HasComponents(AllOfIndices)) &&
                            (AnyOfIndices == null || entityExt.HasAnyComponent(AnyOfIndices)) &&
                            (NoneOfIndices == null || !entityExt.HasAnyComponent(NoneOfIndices));
        }

        public IMatcher<TEntity> NoneOf(params int[] indices)
        {
            NoneOfIndices = distinctIndices(indices);
            indices       = null;
            _isHashCached  = false;
            return this;
        }

        public IMatcher<TEntity> NoneOf(params IMatcher<TEntity>[] matchers)
        {
            return NoneOf(mergeIndices(matchers));
        }
        public IMatcher<TEntity> AllOf(params int[] indices)
        {
            AllOfIndices = distinctIndices(indices);
            indices       = null;
            _isHashCached = false;
            return this;
        }

        public IMatcher<TEntity> AllOf(params IMatcher<TEntity>[] matchers)
        {
            return AllOf(mergeIndices(matchers));
        }
    }
}