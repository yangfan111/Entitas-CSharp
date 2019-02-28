using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Core.GameModule.Interface;

namespace App.Shared.DebugSystem
{
    public abstract class AbstractDebugInfoSystem<T, TInfo> : IGamePlaySystem where T: AbstractDebugInfoSystem<T, TInfo>
    {
        private static int _prevStartKey;
        private static int _startKey;

        private static TInfo _DebugInfo;
<<<<<<< HEAD
        private static object _param;
=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a

        private static void Start()
        {

            Interlocked.Add(ref _startKey, 1);
        }

        private static bool Ready
        {
            get { return _prevStartKey == _startKey; }
        }

        public void OnGamePlay()
        {
            if (_prevStartKey != _startKey)
            {
<<<<<<< HEAD
                _DebugInfo = GetDebugInfo(_param);
=======
                _DebugInfo = GetDebugInfo();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                _prevStartKey = _startKey;
            }
        }

<<<<<<< HEAD
        protected abstract TInfo GetDebugInfo(object param);

        public static TInfo GetDebugInfoOnBlock(object param = null)
        {
            _param = param;
=======
        protected abstract TInfo GetDebugInfo();

        public static TInfo GetDebugInfoOnBlock()
        {
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            Start();

            int sleepCount = 0;
            while (!Ready)
            {
                Thread.Sleep(1000);
                sleepCount++;
                if (sleepCount > 60)
                {
                    return default(TInfo);
                }
            }

            return _DebugInfo;
        }
    }
}
