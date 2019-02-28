using App.Server.GameModules.GamePlay.Free.player;
using App.Shared.Components.Player;
using Core.EntityComponent;
using Core.Enums;
using Core.GameModule.Interface;
using Core.Prediction.UserPrediction.Cmd;
using Core.Utils;
using UnityEngine;
using Utils.Utils;
using XmlConfig;

namespace App.Shared.GameModules.Player
{
    public class PlayerSaveSystem : IUserCmdExecuteSystem
    {
        private static LoggerAdapter _logger = new LoggerAdapter(typeof(PlayerSaveSystem));

        private Contexts _contexts;

        public PlayerSaveSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

<<<<<<< HEAD
=======
        private void StopSave(PlayerEntity playerEntity, bool isInteruptSave = false)
        {
            playerEntity.gamePlay.IsSave = false;
            playerEntity.gamePlay.SaveTime = 0;
            playerEntity.stateInterface.State.RescueEnd();
            /*if (playerEntity.gamePlay.SaveEnterState == (int) playerEntity.stateInterface.State.GetCurrentPostureState()
                && playerEntity.gamePlay.SaveEnterState != (int)PostureInConfig.Crouch)
            {
                playerEntity.stateInterface.State.Crouch();
            }*/
            playerEntity.stateInterface.State.Crouch();

            if (isInteruptSave)
            {
                SetRescureInterupt(playerEntity);
            }
        }

        private void StopBeSave(PlayerEntity playerEntity, bool isInteruptSave = false)
        {
            playerEntity.gamePlay.IsBeSave = false;
            playerEntity.gamePlay.SaveTime = 0;
            if (isInteruptSave)
            {
                SetRescureInterupt(playerEntity);
            }
        }

        private float GetAngle(PlayerEntity myEntity, PlayerEntity teamEntity)
        {
            //角度
            float yaw = CommonMathUtil.TransComAngle(myEntity.orientation.Yaw);
            float angle = CommonMathUtil.GetAngle(new Vector2(teamEntity.position.Value.x, teamEntity.position.Value.z), new Vector2(myEntity.position.Value.x, myEntity.position.Value.z));
            return CommonMathUtil.GetDiffAngle(angle, yaw);
        }

        private void SetRescureInterupt(PlayerEntity entity)
        {
            entity.gamePlay.IsInteruptSave = true;
        }

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        public void ExecuteUserCmd(IUserCmdOwner owner, IUserCmd cmd)
        {
            PlayerEntity myEntity = owner.OwnerEntity as PlayerEntity;
            if (null == myEntity) return;
            if (myEntity.gamePlay.IsSave || myEntity.gamePlay.IsBeSave)
            {
                PlayerEntity teamEntity = _contexts.player.GetEntityWithEntityKey(myEntity.gamePlay.SavePlayerKey);
<<<<<<< HEAD
=======
                if (myEntity.time.ClientTime - myEntity.gamePlay.SaveTime >= SharedConfig.SaveNeedTime)
                {
                    //正常结束
                    StopSave(myEntity);
                    //统计救援人数
                    myEntity.statisticsData.Statistics.SaveCount++;
                    return;
                }
                //打断救援（被救援者已不在）
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                if (null == teamEntity)
                {
                    StopSave(myEntity, true);
                    return;
                }

                if (myEntity.time.ClientTime - myEntity.gamePlay.SaveTime >= SharedConfig.SaveNeedTime)
                {
                    if (myEntity.gamePlay.IsSave)
                    {
<<<<<<< HEAD
                        myEntity.statisticsData.Statistics.SaveCount++;
                    }else{
                        myEntity.statisticsData.Statistics.BeSaveCount++;
                        myEntity.gamePlay.CurHp = myEntity.gamePlay.MaxHp * 0.1f;
                        myEntity.gamePlay.ChangeLifeState(EPlayerLifeState.Alive, myEntity.time.ClientTime);
                    }
                    StopSave(myEntity, false);
=======
                       StopBeSave(teamEntity, true);
                    }
                }
                //打断救援（超出距离角度 或 被救援者已死亡）
                if (teamEntity.gamePlay.IsDead() || GetAngle(myEntity, teamEntity) > SharedConfig.MaxSaveAngle
                    || Vector3.Distance(myEntity.position.Value, teamEntity.position.Value) > SharedConfig.MaxSaveDistance)
                {
                    StopSave(myEntity, true);
                    StopBeSave(teamEntity, true);
                }
            }
            else if (myEntity.gamePlay.IsBeSave)
            {
                PlayerEntity teamEntity = _contexts.player.GetEntityWithEntityKey(myEntity.gamePlay.SavePlayerKey);
                if (myEntity.time.ClientTime - myEntity.gamePlay.SaveTime >= SharedConfig.SaveNeedTime)
                {
                    //正常结束
                    StopBeSave(myEntity);
                    //血量恢复10%
                    myEntity.gamePlay.CurHp = (int)(myEntity.gamePlay.MaxHp * 0.1f);
                    myEntity.gamePlay.ChangeLifeState(EPlayerLifeState.Alive, myEntity.time.ClientTime);
                    //蹲下状态
                    //myEntity.stateInterface.State.Crouch();
                    myEntity.statisticsData.Statistics.BeSaveCount++;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                    return;
                }
                if (Vector3.Distance(myEntity.position.Value, teamEntity.position.Value) > SharedConfig.MaxSaveDistance)
                {
                    StopSave(myEntity, true);
                    StopSave(teamEntity, true);
                    return;
                }
<<<<<<< HEAD
                if (myEntity.gamePlay.IsSave)
=======
                //打断救援（超出距离角度 或 自己已死亡）
                /*if (myEntity.gamePlay.IsDead()
                    || Vector3.Distance(myEntity.position.Value, teamEntity.position.Value) > SharedConfig.MaxSaveDistance
                    || GetAngle(teamEntity ,myEntity) > SharedConfig.MaxSaveAngle)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                {
                    if (myEntity.stateInterface.State.NeedInterruptRescue((PostureInConfig) myEntity.gamePlay.SaveEnterState)
                        || !myEntity.gamePlay.IsLifeState(EPlayerLifeState.Alive) || GetAngle(myEntity, teamEntity) > SharedConfig.MaxSaveAngle)
                    {
                        StopSave(myEntity, true);
                        StopSave(teamEntity, true);
                        return;
                    }
<<<<<<< HEAD
                }
                else
=======
                }*/
            }
            else if (cmd.IsUseAction && cmd.UseType == (int) EUseActionType.Player)
            {
                //趴下不能救援
                /*if (myEntity.stateInterface.State.GetCurrentPostureState() == PostureInConfig.Prone)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                {
                    if (myEntity.gamePlay.IsDead())
                    {
                        StopSave(myEntity, true);
                        StopSave(teamEntity, true);
                        return;
                    }
<<<<<<< HEAD
                }
            }
            if (cmd.IsUseAction && cmd.UseType == (int) EUseActionType.Player)
            {
                PlayerEntity saveEntity = _contexts.player.GetEntityWithEntityKey(new EntityKey(cmd.UseEntityId, (int)EEntityType.Player));
                if (saveEntity != null && SharedConfig.IsServer)
=======
                    return;
                }*/
                //实施救援
                PlayerEntity teamEntity = _contexts.player.GetEntityWithEntityKey(new EntityKey(cmd.UseEntityId, (int)EEntityType.Player));
                if (null != teamEntity)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                {
                    PlayerAnimationAction.DoAnimation(_contexts, PlayerAnimationAction.Rescue, myEntity, true);
                    myEntity.gamePlay.IsSave = true;
                    myEntity.gamePlay.SaveEnterState = (int) myEntity.stateInterface.State.GetCurrentPostureState();
                    myEntity.gamePlay.SaveTime = myEntity.time.ClientTime;
                    myEntity.gamePlay.SavePlayerKey = saveEntity.entityKey.Value;
                    saveEntity.gamePlay.IsBeSave = true;
                    saveEntity.gamePlay.SaveTime = saveEntity.time.ClientTime;
                    saveEntity.gamePlay.SavePlayerKey = myEntity.entityKey.Value;
                }
            }
        }

        private void StopSave(PlayerEntity playerEntity, bool isInterrupted)
        {
            if (SharedConfig.IsServer)
            {
                playerEntity.gamePlay.SaveTime = 0;
                if (playerEntity.gamePlay.IsSave)
                {
                    PlayerAnimationAction.DoAnimation(_contexts, PlayerAnimationAction.RescueEnd, playerEntity, true);
                    playerEntity.gamePlay.IsSave = false;
                }
                if (playerEntity.gamePlay.IsBeSave)
                {
                    if (!isInterrupted)
                    {
                        PlayerAnimationAction.DoAnimation(_contexts, PlayerAnimationAction.Revive, playerEntity, true);
                    }
                    playerEntity.gamePlay.IsBeSave = false;
                }
            }
            playerEntity.gamePlay.IsInteruptSave = isInterrupted;
        }

        private float GetAngle(PlayerEntity myEntity, PlayerEntity teamEntity)
        {
            //角度
            float yaw = CommonMathUtil.TransComAngle(myEntity.orientation.Yaw);
            float angle = CommonMathUtil.GetAngle(new Vector2(teamEntity.position.Value.x, teamEntity.position.Value.z), new Vector2(myEntity.position.Value.x, myEntity.position.Value.z));
            return CommonMathUtil.GetDiffAngle(angle, yaw);
        }
    }
}
