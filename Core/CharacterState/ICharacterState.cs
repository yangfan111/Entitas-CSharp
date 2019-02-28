﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.CharacterState.Action;
using Core.CharacterState.Movement;
using Core.CharacterState.Posture;
using Core.Fsm;

namespace Core.CharacterState
{
    public interface ICharacterState : ICharacterPosture, ISyncFsmSnapshot, IFsmUpdate, ICharacterSpeed, ICharacterMovement, ICharacterAction, INewCommandFromCall
    {
        void SetName(string name);
        string GetName();

        void Dying();
        void Revive();
        void PlayerReborn();

        void SetMoveInWater(bool value);
        bool IsMoveInWater();
        void SetSteepSlope(bool value);
        bool IsSteepSlope();
        void SetBeenSlowDown(bool value);
        bool IsSlowDown();                 //因为涉水或爬坡禁止冲刺，且一直输入冲刺指令
        bool CanDraw();
<<<<<<< HEAD

        void SetExceedSlopeLimit(bool value);
        bool IsExceedSlopeLimit();

        void SetSlide(bool value);
        bool IsSlide();
=======

        void SetExceedSlopeLimit(bool value);
        bool IsExceedSlopeLimit();
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        
        ICharacterMovementInConfig GetIMovementInConfig();
        ICharacterPostureInConfig GetIPostureInConfig();
    }
}
