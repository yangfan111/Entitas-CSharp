using System.Text;

namespace Entitas
{
    public partial class Matcher<TEntity>
    {
        StringBuilder _toStringBuilder;

        string _toStringCache;

        public override string ToString()
        {
            if (_toStringCache == null)
            {
                if (_toStringBuilder == null)
                {
                    _toStringBuilder = new StringBuilder();
                }

                _toStringBuilder.Length = 0;
                if (AllOfIndices != null)
                {
                    appendIndices(_toStringBuilder, "AllOf", AllOfIndices, componentNames);
                }

                if (AnyOfIndices != null)
                {
                    if (AllOfIndices != null)
                    {
                        _toStringBuilder.Append(".");
                    }

                    appendIndices(_toStringBuilder, "AnyOf", AnyOfIndices, componentNames);
                }

                if (NoneOfIndices != null)
                {
                    appendIndices(_toStringBuilder, ".NoneOf", NoneOfIndices, componentNames);
                }

                _toStringCache = _toStringBuilder.ToString();
            }

            return _toStringCache;
        }

        static void appendIndices(StringBuilder sb, string prefix, int[] indexArray, string[] componentNames)
        {
            const string separator = ", ";
            sb.Append(prefix);
            sb.Append("(");
            var lastSeparator = indexArray.Length - 1;
            for (int i = 0; i < indexArray.Length; i++)
            {
                var index = indexArray[i];
                if (componentNames == null)
                {
                    sb.Append(index);
                }
                else
                {
                    sb.Append(componentNames[index]);
                }

                if (i < lastSeparator)
                {
                    sb.Append(separator);
                }
            }

            sb.Append(")");
        }
    }
}