﻿using Core.Fsm;
using System;
using System.Collections.Generic;
using Utils.CharacterState;
using System.Linq;
using System.Text;
using Core;
using Core.CharacterState.Action.States;
using Core.Utils;
using UnityEngine;
using Utils.Appearance;
using Utils.Utils;

namespace Core.CharacterState.Action
{
    public class NewCommandFromCall : NewCommandImpl, INewCommandFromCall
    {
<<<<<<< HEAD
=======
        protected static readonly LoggerAdapter Logger = new LoggerAdapter(typeof(NewCommandFromCall));

        class NewCommandContainer : AdaptiveContainerImpl<INewCommand, NewCommand>
        {

            private int useCount = 0;

            public NewCommandContainer(int initSize) : base(initSize)
            {
                _defaultGetItemCondition = DefaultGetItemCondition;
                Reset();
            }

            public void Reset()
            {
                useCount = 0;
                for (int i = 0; i < this.Length; ++i)
                {
                    this[i].Reset();
                }
            }

            private bool DefaultGetItemCondition(INewCommand newCommand)
            {
                return newCommand.Type == FsmInput.None;
            }

            protected override int GetAvailable(Func<INewCommand, bool> getItemCondition)
            {
                int ret = 0;
                int index = useCount;
                while (index < this.Length)
                {
                    if (getItemCondition(this[index]))
                    {
                        ret = index;
                        useCount = ret + 1;
						return ret;
                    }
                    index++;
                }
                
                Logger.WarnFormat("resize NewCommandContainer list from {0} to {1}, useCount:{2}!!!", this.Length, ret + 1, useCount);
                ret = index;
                useCount = ret + 1;
                Resize(ret + 1);
                var newCommand = new NewCommand();
                newCommand.Reset();
                this[ret] = newCommand;
                return ret;
            }
        }

        interface INewCommand
        {
            FsmInput Type { get; set; }
            float AdditionalValue { get; set; }
            void Reset();
        }

        class NewCommand: INewCommand
        {
            public NewCommand()
            {
                Reset();
            }
            public FsmInput Type { get; set; }
            public float AdditionalValue { get; set; }
            public void Reset()
            {
                Type = FsmInput.None;
                AdditionalValue = 0f;
            }
        }

        class CallBackRegister
        {
            private Dictionary<FsmInput, System.Action> _inputCallBack = new Dictionary<FsmInput, System.Action>(CommonIntEnumEqualityComparer<FsmInput>.Instance);
            private Dictionary<FsmInput, System.Action> _callBackRemove = new Dictionary<FsmInput, System.Action>(CommonIntEnumEqualityComparer<FsmInput>.Instance);

            public void AddNewCallBack(FsmInput trigger, FsmInput removeCondition, System.Action callBack)
            {
                if (!_inputCallBack.ContainsKey(trigger))
                {
                    if (!_callBackRemove.ContainsKey(removeCondition))
                    {
                        _callBackRemove[removeCondition] = default(System.Action);
                    }
                    _callBackRemove[removeCondition] += () => _inputCallBack[trigger] = null;
                }
                _inputCallBack[trigger] = callBack;
            }

            public void TryInvokeCallBack(FsmInput type)
            {
                System.Action callBack;
                _inputCallBack.TryGetValue(type, out callBack);

                if (callBack != null)
                {
                    callBack.Invoke();
                    Logger.DebugFormat("Animation End Callback: {0}", type);
                }
            }

            public void TryRemoveCallBack(FsmInput type)
            {
                System.Action remove;
                _callBackRemove.TryGetValue(type, out remove);
                
                if (remove != null)
                    remove.Invoke();
            }
        }

        private static readonly int CommandsInitLen = 5;

        private NewCommandContainer commandsContainer = new NewCommandContainer(CommandsInitLen);

        private FsmOutputCache _directOutputs = new FsmOutputCache();

        private CallBackRegister _callBackRegister = new CallBackRegister();

        private List<FsmInput> _interruptInputs = new List<FsmInput>();
        
        private List<AnimationCallBackCommand> _animationCallBackCommand = new List<AnimationCallBackCommand>();

        private float _reloadSpeedRatio = 1;

>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        protected NewCommandFromCall()
        {
        }

        #region New FsmInput

        public void Fire()
        {
            SetNewCommandFromFunctionCall(FsmInput.Fire);
        }

        public void SpecialFire(System.Action callBack)
        {
            SetNewCommandFromFunctionCall(FsmInput.SpecialFire);
            SetNewCallbackFromFunctionCall(FsmInput.FireEndFinished, FsmInput.FireEndFinished, callBack);
        }

        public void SpecialFireEnd()
        {
            SetNewCommandFromFunctionCall(FsmInput.SpecialFireEnd);
        }

        public void SightsFire()
        {
            SetNewCommandFromFunctionCall(FsmInput.SightsFire);
        }

        public void SpecialSightsFire(System.Action callBack)
        {
            SetNewCommandFromFunctionCall(FsmInput.SpecialSightsFire);
            SetNewCallbackFromFunctionCall(FsmInput.FireEndFinished, FsmInput.FireEndFinished, callBack);
        }

        public void Reload(System.Action callBack)
        {
            SetNewCommandFromFunctionCall(FsmInput.Reload);

            SetNewCallbackFromFunctionCall(FsmInput.ReloadFinished, FsmInput.ReloadFinished, callBack);
        }

        public void ReloadEmpty(System.Action callBack)
        {
            SetNewCommandFromFunctionCall(FsmInput.ReloadEmpty);
            SetNewCallbackFromFunctionCall(FsmInput.ReloadFinished, FsmInput.ReloadFinished, callBack);
        }

        public void BeenHit()
        {
            SetNewCommandFromFunctionCall(FsmInput.Injury);
        }

        public void Dying()
        {
            SetNewCommandFromFunctionCall(FsmInput.Dying);
        }

        public void Revive()
        {
            SetNewCommandFromFunctionCall(FsmInput.Revive);
            SetPostureCrouch();
        }

        public void Unarm(System.Action callBack, float holsterParam)
        {
            SetNewCommandFromFunctionCall(FsmInput.Unarm, holsterParam);
            SetNewCallbackFromFunctionCall(FsmInput.HolsterFinished, FsmInput.HolsterFinished, callBack);
        }

        public void Draw(System.Action callBack, float drawParam)
        {
            SetNewCommandFromFunctionCall(FsmInput.Draw, drawParam);
            SetNewCallbackFromFunctionCall(FsmInput.SelectFinished, FsmInput.SelectFinished, callBack);
        }

        public void SwitchWeapon(System.Action unarmCallBack, System.Action drawCallBack, float SwitchType)
        {
            SetNewCommandFromFunctionCall(FsmInput.SwitchWeapon, SwitchType);
            SetNewCallbackFromFunctionCall(FsmInput.HolsterFinished, FsmInput.HolsterFinished, unarmCallBack);
            SetNewCallbackFromFunctionCall(FsmInput.SelectFinished, FsmInput.SelectFinished, drawCallBack);
        }

        public void InterruptSwitchWeapon()
        {
            SetNewCommandFromFunctionCall(FsmInput.InterruptSwitchWeapon);
        }

        public void Rescue()
        {
            SetNewCommandFromFunctionCall(FsmInput.Rescue);
        }

        public void RescueEnd()
        {
            SetNewCommandFromFunctionCall(FsmInput.RescueEnd);
            SetPostureCrouch();
        }

        public void SetPostureStand()
        {
            SetNewCommandFromFunctionCall(FsmInput.PostureStand);
        }

        public void SetPostureCrouch()
        {
            SetNewCommandFromFunctionCall(FsmInput.PostureCrouch);
        }

        public void SetPostureProne()
        {
            SetNewCommandFromFunctionCall(FsmInput.PostureProne);
        }

        public void SetPostureStand()
        {
            SetNewCommandFromFunctionCall(FsmInput.PostureStand);
        }

        public void SetPostureCrouch()
        {
            SetNewCommandFromFunctionCall(FsmInput.PostureCrouch);
        }

        public void SetPostureProne()
        {
            SetNewCommandFromFunctionCall(FsmInput.PostureProne);
        }

        public void Stand()
        {
            SetNewCommandFromFunctionCall(FsmInput.Jump);
        }

        public void Crouch()
        {
            SetNewCommandFromFunctionCall(FsmInput.Crouch);
        }

        public void Swim()
        {
            //Logger.InfoFormat("set Swim!!!!!!!!, {0}", new StackTrace());
            SetNewCommandFromFunctionCall(FsmInput.Swim);
        }

        public void Dive()
        {
            //Logger.InfoFormat("set Dive!!!!!!!!, {0}", new StackTrace());
            SetNewCommandFromFunctionCall(FsmInput.Dive);
        }

        public void Ashore()
        {
            //Logger.InfoFormat("set ashore!!!!!!!!, {0}", new StackTrace());
            SetNewCommandFromFunctionCall(FsmInput.Ashore);
        }

        // 第三人称机瞄
        public void SetSight(float speedRatio)
        {
            SetNewCommandFromFunctionCall(FsmInput.Sight, speedRatio);
        }

        // 取消第三人称机瞄
        public void CancelSight(float speedRatio)
        {
            SetNewCommandFromFunctionCall(FsmInput.CancelSight, speedRatio);
        }

        public void DriveStart(int seatId, int postureId)
        {
            var complexId = seatId + postureId * 10;
            SetNewCommandFromFunctionCall(FsmInput.DriveStart, complexId);
        }

        public void DriveEnd()
        {
            SetNewCommandFromFunctionCall(FsmInput.DriveEnd);
        }

        // 特殊换弹
        public void SpecialReload(System.Action callBack, int count, System.Action finishReloadCallBack)
        {
            SetNewCommandFromFunctionCall(FsmInput.SpecialReload, count);
            SetNewCallbackFromFunctionCall(FsmInput.SpecialReloadTrigger, FsmInput.ReloadFinished, callBack);
            SetNewCallbackFromFunctionCall(FsmInput.ReloadFinished, FsmInput.ReloadFinished, finishReloadCallBack);
        }

        // 打断特殊换弹
        public void BreakSpecialReload()
        {
            SetNewCommandFromFunctionCall(FsmInput.BreakSpecialReload);
        }

        public void ForceBreakSpecialReload(System.Action callBack)
        {
            SetNewCommandFromFunctionCall(FsmInput.ForceBreakSpecialReload);
            SetNewCallbackFromFunctionCall(FsmInput.ForceBreakSpecialReload, FsmInput.ForceBreakSpecialReload,
                callBack);
        }

        // 拾取
        public void PickUp()
        {
            SetNewCommandFromFunctionCall(FsmInput.PickUp);
        }

        // 开门
        public void OpenDoor()
        {
            SetNewCommandFromFunctionCall(FsmInput.OpenDoor);
        }

        // 使用道具
        public void UseProps(float PropKind)
        {
            SetNewCommandFromFunctionCall(FsmInput.Props, PropKind);
        }

        public void FinishProps()
        {
            SetNewCommandFromFunctionCall(FsmInput.FinishProps);
        }

        //近战动作
        public void LightMeleeAttackOne(System.Action callBack)
        {
            SetNewCommandFromFunctionCall(FsmInput.LightMeleeAttackOne);
            SetNewCallbackFromFunctionCall(FsmInput.MeleeAttackFinished, FsmInput.MeleeAttackFinished, callBack);
        }

        public void LightMeleeAttackTwo(System.Action callBack)
        {
            SetNewCommandFromFunctionCall(FsmInput.LightMeleeAttackTwo);
            SetNewCallbackFromFunctionCall(FsmInput.MeleeAttackFinished, FsmInput.MeleeAttackFinished, callBack);
        }

        public void MeleeSpecialAttack(System.Action callBack)
        {
            SetNewCommandFromFunctionCall(FsmInput.MeleeSpecialAttack);
            SetNewCallbackFromFunctionCall(FsmInput.MeleeAttackFinished, FsmInput.MeleeAttackFinished, callBack);
        }

        // c4动作
        public void C4Animation(System.Action callBack)
        {
            SetNewCommandFromFunctionCall(FsmInput.BuriedBomb);
            SetNewCallbackFromFunctionCall(FsmInput.BuriedBombFinished, FsmInput.BuriedBombFinished, callBack);
        }

        public void BuriedBomb(System.Action callBack)
        {
            Logger.InfoFormat("BuriedBomb--------------");
            SetNewCommandFromFunctionCall(FsmInput.BuriedBomb);
            SetNewCallbackFromFunctionCall(FsmInput.BuriedBombFinished, FsmInput.BuriedBombFinished, callBack);
            SetPostureStand();
        }

        public void DismantleBomb(System.Action callBack)
        {
            SetNewCommandFromFunctionCall(FsmInput.DismantleBomb);
            SetNewCallbackFromFunctionCall(FsmInput.DismantleBombFinished, FsmInput.DismantleBombFinished, callBack);
            SetPostureCrouch();
        }

        //投掷动作
        public void StartNearGrenadeThrow()
        {
            SetNewCommandFromFunctionCall(FsmInput.StartNearGrenade);
        }

        public void StartFarGrenadeThrow()
        {
            SetNewCommandFromFunctionCall(FsmInput.StartFarGrenade);
        }

        public void ChangeThrowDistance(float weight)
        {
            SetNewCommandFromFunctionCall(FsmInput.ChangeGrenadeDistance, weight);
        }

        public void FinishGrenadeThrow()
        {
            SetNewCommandFromFunctionCall(FsmInput.FinishGrenade);
        }

        public void ForceFinishGrenadeThrow()
        {
            Logger.Debug("InterruptGrenade :   ForceEnd");
            SetNewCommandFromFunctionCall(FsmInput.ForceFinishGrenade);
        }

        public void StartClimb(float kind, System.Action callBack)
        {
            Logger.Debug("StartClimb");
            SetNewCommandFromFunctionCall(FsmInput.Climb, kind);
            SetNewCallbackFromFunctionCall(FsmInput.GenericActionFinished, FsmInput.GenericActionFinished, callBack);
        }

        // 滑翔
        public void Gliding()
        {
            //Logger.InfoFormat("gliding !!!!!!!!!!!!!");
            SetNewCommandFromFunctionCall(FsmInput.Gliding);
        }

        // 伞降
        public void Parachuting(System.Action callBack)
        {
            //Logger.InfoFormat("Parachuting !!!!!!!!!!!!!");
            SetNewCommandFromFunctionCall(FsmInput.Parachuting);
            SetNewCallbackFromFunctionCall(FsmInput.ParachuteOpen1Finished, FsmInput.ParachuteOpen1Finished, callBack);
        }

        public void ParachutingEnd()
        {
            //Logger.InfoFormat("ParachutingEnd !!!!!!!!!!!!!");

            SetNewCommandFromFunctionCall(FsmInput.ParachutingEnd);
        }

        // 自由坠落
        public void Freefall()
        {
            Logger.InfoFormat("Freefall !!!!!!!!!!!!!");

            SetNewCommandFromFunctionCall(FsmInput.Freefall);
        }

        // 下滑
        public void Slide()
        {
            Logger.InfoFormat("Slide!!!");
            SetNewCommandFromFunctionCall(FsmInput.Slide);
        }

        // 下滑
        public void SlideEnd()
        {
            Logger.InfoFormat("SlideEnd!!!");
            SetNewCommandFromFunctionCall(FsmInput.SlideEnd);
        }

        // 打断行为
        public void InterruptAction()
        {
            Logger.InfoFormat("Request Interrupt Action");
            SetNewCommandFromFunctionCall(FsmInput.InterruptAction);
            InterruptInputs();
        }

        public void SetDiveUpDownValue(float value)
        {
            if (Logger.IsDebugEnabled)
            {
                //Logger.DebugFormat("SetDiveUpAngle value to:{0}", value);
            }

            SetNewCommandFromFunctionCall(FsmInput.DiveUpDown, value);
        }

        #endregion
    }
}