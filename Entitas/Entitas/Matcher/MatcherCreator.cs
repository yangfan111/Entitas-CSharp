using System;
using System.Collections.Generic;

namespace Entitas
{
    public partial class Matcher<TEntity>
    {
        static readonly List<int> indexBuffer = new List<int>();
        static readonly HashSet<int> indexSetBuffer = new HashSet<int>();

        public static IMatcher<TEntity> CreateAllOf(params int[] indices)
        {
            var matcher = new Matcher<TEntity>();
            matcher.AllOfIndices = distinctIndices(indices);
            return matcher;
        }
        /// <summary>
        /// 只去matcher的第一个元素
        /// </summary>
        /// <param name="matchers"></param>
        /// <returns></returns>
        public static IMatcher<TEntity> CreateAllOf(params IMatcher<TEntity>[] matchers)
        {
            var allOfMatcher = (Matcher<TEntity>) CreateAllOf(mergeIndices(matchers));
            setComponentNames(allOfMatcher, matchers);
            return allOfMatcher;
        }

        public static IMatcher<TEntity> CreateAnyOf(params int[] indices)
        {
            var matcher = new Matcher<TEntity>();
            matcher.AnyOfIndices = distinctIndices(indices);
            return matcher;
        }

        public static IMatcher<TEntity> CreateAnyOf(params IMatcher<TEntity>[] matchers)
        {
            var anyOfMatcher = (Matcher<TEntity>) CreateAnyOf(mergeIndices(matchers));
            setComponentNames(anyOfMatcher, matchers);
            return anyOfMatcher;
        }

        static int[] mergeIndices(int[] allOfIndices, int[] anyOfIndices, int[] noneOfIndices)
        {
            if (allOfIndices != null)
            {
                indexBuffer.AddRange(allOfIndices);
            }

            if (anyOfIndices != null)
            {
                indexBuffer.AddRange(anyOfIndices);
            }

            if (noneOfIndices != null)
            {
                indexBuffer.AddRange(noneOfIndices);
            }

            var mergedIndices = distinctIndices(indexBuffer);

            indexBuffer.Clear();

            return mergedIndices;
        }

        static int[] mergeIndices(IMatcher<TEntity>[] matchers)
        {
            var indices = new int[matchers.Length];
            for (int i = 0; i < matchers.Length; i++)
            {
                var matcher = matchers[i];
                if (matcher.Indices.Length != 1)
                {
                    throw new MatcherException(matcher.Indices.Length);
                }

                indices[i] = matcher.Indices[0];
            }

            return indices;
        }

        static string[] getComponentNames(IMatcher<TEntity>[] matchers)
        {
            for (int i = 0; i < matchers.Length; i++)
            {
                var matcher = matchers[i] as Matcher<TEntity>;
                if (matcher != null && matcher.componentNames != null)
                {
                    return matcher.componentNames;
                }
            }

            return null;
        }

        static void setComponentNames(Matcher<TEntity> matcher, IMatcher<TEntity>[] matchers)
        {
            var componentNames = getComponentNames(matchers);
            if (componentNames != null)
            {
                matcher.componentNames = componentNames;
            }
        }

        static int[] distinctIndices(IList<int> indices)
        {
            foreach (var index in indices)
            {
                indexSetBuffer.Add(index);
            }

            var uniqueIndices = new int[indexSetBuffer.Count];
            indexSetBuffer.CopyTo(uniqueIndices);
            Array.Sort(uniqueIndices);

            indexSetBuffer.Clear();

            return uniqueIndices;
        }
    }
}