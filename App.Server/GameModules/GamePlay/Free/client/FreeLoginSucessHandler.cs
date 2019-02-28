﻿using Core.Free;
using Free.framework;

namespace App.Server.GameModules.GamePlay.free.client
{
    /// <summary>
    /// 废弃. 使用PlayerSyncStageSystem作为登录完成动作
    /// </summary>
    public class FreeLoginSucessHandler : IFreeMessageHandler
    {
        public bool CanHandle(ServerRoom room, PlayerEntity player, SimpleProto message)
        {
            return message.Key == FreeMessageConstant.ClientLoginSucess;
        }

        public void Handle(ServerRoom room, PlayerEntity player, SimpleProto message)
        {
            //room.GameRule.PlayerEnter(room.RoomContexts, player);
        }
    }
}
