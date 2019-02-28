﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace App.Shared.Util
{
    public static class CommonUtil
    {

        public static T GetAttribute<T>(MemberInfo t) where T : Attribute
        {
            return Attribute.GetCustomAttribute(t, typeof(T)) as T;
        }
        public static void WeakAssert(bool condition)
        {
#if UNITY_EDITOR
            UnityEngine.Debug.Assert(condition);
#endif
        }
        public static void WeakAssert(bool condition, string message, params object[] args)
        {

#if UNITY_EDITOR
            var text = string.Format(message, args);
            UnityEngine.Debug.Assert(condition, text);
#endif
        }

        public static void ProcessDerivedTypes(Type baseType, bool includeSelf, Action<Type> process)
        {
            var types = baseType.Assembly.GetTypes();
            for (var i = 0; i < types.Length; i++)
            {
                if (types[i].IsClass &&
                    (types[i].IsSubclassOf(baseType) || baseType.IsAssignableFrom(types[i])))
                {
                    if (!includeSelf && baseType == types[i]) continue;
                    //   instance = Activator.CreateInstance(types[i]);
                    process(types[i]);
                }
            }
        }
    }
}