﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.EntityComponent;

namespace App.Shared.DebugSystem
{
    public interface IServerDebugInfoAccessor
    {
        ServerDebugInfo GetDebugInfo();
    }

    public class PlayerDebugInfo
    {
        public bool HasPlayerEntity;
        public bool HasPlayerInfo;

        public EntityKey EntityKey;
        public long EntityId;
        public long PlayerId;
        public long TeamId;
        public string Name;

        public bool IsRobot;
        public string Token;
        public bool IsLogin;
        public long CreateTime;
        public int GameStartTime;
    }

    public class RoomDebugInfo
    {
        public string State;
        public long HallRoomId;
        public long RoomId;
<<<<<<< HEAD
        public bool HasHallServer;
=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
    }

    public class ServerDebugInfo
    {
        public IList<PlayerDebugInfo> PlayerDebugInfo;
        public RoomDebugInfo RoomDebugInfo;
    }
}
