<<<<<<< HEAD
﻿using Core.Utils;
using System;
=======
﻿using System;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
using System.Collections.Generic;
using System.Linq;

namespace App.Shared
{
    public class GameModuleManagement
    {
        private static readonly HashSet<IDisposable> entireInstances = new HashSet<IDisposable>();

        internal static void Submit<T>(T instance) where T : ModuleLogicActivator<T>, new()
        {
            entireInstances.Add(instance);
        }
        public static T Get<T>() where T : ModuleLogicActivator<T>, new()
        {
            return ModuleLogicActivator<T>.s_Default;
        }
        public static T Get<T>(int cookie) where T : ModuleLogicActivator<T>, new()
        {
            return ModuleLogicActivator<T>.Get(cookie);
        }
        public static T Allocate<T>(System.Action<T> processor = null) where T : ModuleLogicActivator<T>, new()
        {
            var instance = ModuleLogicActivator<T>.Allocate(processor);
            Submit(instance);
            return instance;
        }
        public static T Allocate<T>(int cookie, System.Action<T> processor = null) where T : ModuleLogicActivator<T>, new()
        {
            var instance = ModuleLogicActivator<T>.Allocate(cookie, processor);
            Submit(instance);
            return instance;
        }
<<<<<<< HEAD
        public static T ForceAllocate<T>(int cookie, System.Action<T> processor = null) where T : ModuleLogicActivator<T>, new()
        {
            var instance = ModuleLogicActivator<T>.ForceAllocate(cookie, processor);
            Submit(instance);
            return instance;
        }
=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public static void Dispose()
        {
            var disposeArray = entireInstances.ToList();
            foreach(var data in disposeArray)
            {
                data.Dispose();
            }
<<<<<<< HEAD
=======
            disposeArray.Clear();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        }
        internal static void UnCache(IDisposable instance)
        {
            entireInstances.Remove(instance);
        }
     

    }
    public class ModuleLogicActivator<T> : IDisposable where T : ModuleLogicActivator<T>, new()
    {

        internal static T s_Default { get; private set; }
<<<<<<< HEAD
        private readonly static Dictionary<int, T> logics = new Dictionary<int, T>();
        private int cookie;
        internal static T Get(int cookie)
        {
            
            if (s_Default != null && s_Default.cookie == cookie) return s_Default;
            if (logics.ContainsKey(cookie))
            {
                DebugUtil.LogInUnity(logics[cookie].ToString());
                return logics[cookie];
            }
            return default(T);
        }
      
=======
        private static readonly Dictionary<int, T> logics = new Dictionary<int, T>();
        private int cookie;
        internal static T Get(int cookie)
        {
            if (s_Default.cookie == cookie) return s_Default;
            if (logics.ContainsKey(cookie)) return logics[cookie];
            return default(T);
        }
        internal static void Clear()
        {
            s_Default = null;
            logics.Clear();
        }
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        internal static T Allocate(System.Action<T> processor)
        {
            var instance = new T();
            int cookie = instance.GetHashCode();
            instance.cookie = cookie;
<<<<<<< HEAD
           
            if (s_Default == null) s_Default = instance;
            logics[cookie] = instance;
            if (processor != null)
                processor(instance);
=======
            if (processor != null)
                processor(instance);
            if (s_Default == null) s_Default = instance;
            logics.Add(cookie, instance);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            return instance;
        }
        internal static T Allocate(int cookie, System.Action<T> processor)
        {
<<<<<<< HEAD
            var instance = new T();
            instance.cookie = cookie;
            if (s_Default == null) s_Default = instance;
            logics[cookie] = instance;
            if (processor != null)
                processor(instance);
=======
            if (logics.ContainsKey(cookie)) return logics[cookie] ;
            var instance = new T();
            instance.cookie = cookie;
            if (processor != null)
                processor(instance);
            if (s_Default == null) s_Default = instance;
            logics.Add(cookie, instance);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            return instance;
        }
        internal static T ForceAllocate(int cookie, System.Action<T> processor)
        {
            if (logics.ContainsKey(cookie))
            {
                var tmp = logics[cookie];
                tmp.Dispose();
            }
           return Allocate(cookie, processor);
        }
        public void Dispose()
        {
<<<<<<< HEAD
            if(logics.ContainsKey(cookie))
                logics.Remove(cookie);
=======
            logics.Remove(cookie);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            if (s_Default == this)
                s_Default = null;
            GameModuleManagement.UnCache(this);
        }
       
    }
}