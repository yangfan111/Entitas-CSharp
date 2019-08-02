using System.Text;

namespace Entitas
{
    public class FrameworkUtil
    {
        private static StringBuilder sb = new StringBuilder();
        public static  LoggerLevel loggleLevel;
        public static void ThrowException( string msg, string hit = "")
        {
            if (loggleLevel == LoggerLevel.Error)
            {
                throw new EntitasException(msg, hit);
            }
        }

        public static string ToString(EntityExt ext)
        {
            sb.Length = 0;
            sb.Append("Entity_").Append(ext.CreationIndex)

                            // TODO VD PERFORMANCE
                            //                    .Append("(*")
                            //                    .Append(retainCount)
                            //                    .Append(")")
                            .Append("(");

            const string separator     = ", ";
            var          components    = ext.GetComponents();
            var          lastSeparator = components.Length - 1;
            for (int i = 0; i < components.Length; i++)
            {
                var component = components[i];
                //   var type      = component.GetType();

                // TODO VD PERFORMANCE

                //                    var implementsToString = type.GetMethod("ToString")
                //                        .DeclaringType.ImplementsInterface<IComponent>();
                //                    _toStringBuilder.Append(
                //                        implementsToString
                //                            ? component.ToString()
                //                            : type.ToCompilableString().RemoveComponentSuffix()
                //                    );

                sb.Append(component);

                if (i < lastSeparator)
                {
                    sb.Append(separator);
                }
            }

            sb.Append(")");
            return sb.ToString();
        }
    }
}