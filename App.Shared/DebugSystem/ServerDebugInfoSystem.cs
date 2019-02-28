using System;
using System.Collections.Generic;
using App.Shared.DebugSystem;
using Core.GameModule.Interface;

namespace App.Shared.DebugSystem
{
    public class ServerDebugInfoSystem : AbstractDebugInfoSystem<ServerDebugInfoSystem, ServerDebugInfo>
    {
        private IServerDebugInfoAccessor _accessor;
        public ServerDebugInfoSystem(IServerDebugInfoAccessor accessor)
        {
            _accessor = accessor;
        }

        public void Update()
        {
            OnGamePlay();
        }

<<<<<<< HEAD
        protected override ServerDebugInfo GetDebugInfo(object param)
=======
        protected override ServerDebugInfo GetDebugInfo()
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {
            var debugInfo = _accessor.GetDebugInfo();
            return debugInfo;
        }
    }
}
