﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utils.Appearance;
using XmlConfig;

namespace App.Shared.GameModules.Player.CharacterBone
{
    public class IKFilter
    {
        private static readonly ActionInConfig[] _actionStateFilters = new ActionInConfig[]
        {
            ActionInConfig.Reload,
            ActionInConfig.SpecialReload,
            ActionInConfig.OpenDoor,
            ActionInConfig.SwitchWeapon,
            ActionInConfig.PickUp
        };

        private static readonly ActionKeepInConfig[] _keepActionStateFilters = new ActionKeepInConfig[]
        {
            ActionKeepInConfig.Rescue
        };

        private static readonly PostureInConfig[] _postureStateFilters = new PostureInConfig[]
        {
            PostureInConfig.ProneTransit,
            PostureInConfig.ProneToCrouch,
            PostureInConfig.ProneToStand,
            PostureInConfig.Prone,
            PostureInConfig.Climb
        };

        private static readonly MovementInConfig[] _movementStateFilters =
        {
            MovementInConfig.Sprint
        };

<<<<<<< HEAD
        private static ThirdPersonPosture[] _thirdPersonPostureFilters = new ThirdPersonPosture[]
        {
            ThirdPersonPosture.Swim, ThirdPersonPosture.Dive,
            ThirdPersonPosture.Prone,
=======
        private static ThirdPersonPosture[] _thirdPersonPostureFilters = new ThirdPersonPosture[] { ThirdPersonPosture.Swim , ThirdPersonPosture.Dive,
            ThirdPersonPosture.Prone ,
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            ThirdPersonPosture.ProneTransit,
            ThirdPersonPosture.ProneToCrouch,
            ThirdPersonPosture.ProneToStand,
            ThirdPersonPosture.Climb
        };

<<<<<<< HEAD
        public static bool FilterPlayerIK(ActionInConfig action, ActionKeepInConfig keepAction, PostureInConfig posture,
            PostureInConfig nextPosture, MovementInConfig movement)
=======
        public static bool FilterPlayerIK(ActionInConfig action, ActionKeepInConfig keepAction, PostureInConfig posture, MovementInConfig movement)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {
            return !(IsStateInActionFilter(action) ||
                     IsStateInKeepActionFilter(keepAction) ||
                     IsStateInPostureFilter(posture) ||
<<<<<<< HEAD
                     IsStateInPostureFilter(nextPosture) ||
=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                     IsStateInMovementFilter(movement));
        }

        private static bool IsStateInActionFilter(ActionInConfig state)
        {
            for (var i = 0; i < _actionStateFilters.Length; ++i)
            {
                if (_actionStateFilters[i] == state)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsStateInKeepActionFilter(ActionKeepInConfig state)
        {
            for (var i = 0; i < _keepActionStateFilters.Length; ++i)
            {
                if (_keepActionStateFilters[i] == state)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsStateInPostureFilter(PostureInConfig state)
        {
            for (var i = 0; i < _postureStateFilters.Length; ++i)
            {
                if (_postureStateFilters[i] == state)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsStateInMovementFilter(MovementInConfig state)
        {
            for (var i = 0; i < _movementStateFilters.Length; ++i)
            {
                if (_movementStateFilters[i] == state)
                {
                    return true;
                }
            }

            return false;
        }
        
        private static bool IsStateInMovementFilter(MovementInConfig state)
        {
            for (var i = 0; i < _movementStateFilters.Length; ++i)
            {
                if (_movementStateFilters[i] == state)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsInThirdPersonPostureFilters(ThirdPersonPosture posture)
        {
            for (var i = 0; i < _thirdPersonPostureFilters.Length; ++i)
            {
                if (_thirdPersonPostureFilters[i] == posture)
                {
                    return true;
                }
            }

            return false;
        }
    }
}