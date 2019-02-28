using Core.CharacterState;
using Core.Configuration;
using Core.Fsm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utils.Appearance;
using Utils.CharacterState;

namespace Core.AnimatorClip
{
    public class AnimatorClipManager
    {
        private readonly AnimatorClipMonitor _thirdClipMonitor = new AnimatorClipMonitor();
        private readonly AnimatorClipMonitor _firstClipMonitor = new AnimatorClipMonitor();

        private float _reloadSpeedBuff = 1.0f;

        public void SetAnimationCleanEventCallback(Action<AnimationEvent> animationEventCallback)
        {
            _thirdClipMonitor.SetAnimationCleanEventCallback(animationEventCallback);
            _firstClipMonitor.SetAnimationCleanEventCallback(animationEventCallback);
        }

<<<<<<< HEAD
        public void Update(Action<FsmOutput> addOutput, Animator thirdAnimator, Animator firstAnimator, int? weaponId)
=======
        public void Update(IAdaptiveContainer<IFsmInputCommand> commands, Action<FsmOutput> addOutput, Animator thirdAnimator, Animator firstAnimator, 
            int? weaponId, bool needRewind)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {
            _thirdClipMonitor.UpdateClipBehavior(thirdAnimator);
            _thirdClipMonitor.Update(addOutput, CharacterView.ThirdPerson, _reloadSpeedBuff);

            _firstClipMonitor.UpdateClipBehavior(firstAnimator);
            _firstClipMonitor.Update(addOutput, CharacterView.FirstPerson, _reloadSpeedBuff);

            GetAnimatorClipTimesByWeaponId(weaponId);
        }

        public void SetReloadSpeedBuff(float value)
        {
            _reloadSpeedBuff = value;
        }

        public void ResetReloadSpeedBuff()
        {
            _reloadSpeedBuff = AnimatorParametersHash.DefaultAnimationSpeed;
        }

<<<<<<< HEAD
        private void GetAnimatorClipTimesByWeaponId(int? weaponId)
=======
        private void GetAnimatorClipTimesByWeaponId(IAdaptiveContainer<IFsmInputCommand> commands, bool needRewind, int? weaponId)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {
            _thirdClipMonitor.SetAnimatorClipsTime(weaponId);
            _firstClipMonitor.SetAnimatorClipsTime(weaponId);
        }
    }
}
