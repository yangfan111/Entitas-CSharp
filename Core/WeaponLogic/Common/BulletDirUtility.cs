﻿using Core.Utils;
using UnityEngine;

namespace Core.WeaponLogic
{

    public class BulletDirUtility
    {
        private static LoggerAdapter _logger = new LoggerAdapter(typeof(BulletDirUtility));
        public static Vector3 GetBulletDir(IPlayerWeaponState playerWeapon, int seed, float spreadX, float spreadY)
        {
            var yaw = playerWeapon.ViewYaw - playerWeapon.NegPunchYaw * 2;
            var pitch = playerWeapon.ViewPitch - playerWeapon.NegPunchPitch * 2;
            var dir = CalculateShotingDir(seed, yaw, pitch, spreadX,
                spreadY);

            return dir.normalized;
        }

        public static Vector3 GetSightBulletDir(IPlayerWeaponState playerWeapon, int seed, float spreadX, float spreadY, float sightYawDelta, float sightPitchDelta)
        {
            var yaw = playerWeapon.ViewYaw - playerWeapon.NegPunchYaw * 2 + sightYawDelta;
            var pitch = playerWeapon.ViewPitch - playerWeapon.NegPunchPitch * 2 + sightPitchDelta;
            var dir = CalculateShotingDir(seed, yaw, pitch, spreadX,
                spreadY);

            return dir.normalized;
        }

        public static Vector3 GetThrowingDir(IPlayerWeaponState playerWeapon)
        {
            var yaw = playerWeapon.ViewYaw - playerWeapon.NegPunchYaw * 2;
            var pitch = playerWeapon.ViewPitch - playerWeapon.NegPunchPitch * 2;

            Quaternion q = Quaternion.Euler(pitch, yaw, 0);
            Vector3 forward = q.Forward();

            forward.y += 0.2f;
            return forward.normalized;
        }

        public static Vector3 CalculateShotingDir(int seed, float yaw, float pitch, float spreadX, float spreadY)
        {
            Quaternion q = Quaternion.Euler(pitch, yaw, 0);
            Vector3 forward = q.Forward();
            Vector3 right = q.Right();
            Vector3 up = q.Up();

            float x;
            float y;
            //,z;
            x = (float) UniformRandom.RandomFloat(seed + 0, -0.5, 0.5) +
                (float) UniformRandom.RandomFloat(seed + 1, -0.5, 0.5);
            y = (float) UniformRandom.RandomFloat(seed + 2, -0.5, 0.5) +
                (float) UniformRandom.RandomFloat(seed + 3, -0.5, 0.5);
            //z = x * x + y * y;
            float res1 = spreadX * x;
            float res2 = spreadY * y;
            right = Vector3Ext.Scale(right, res1);
            up = Vector3Ext.Scale(up, res2);
            var newForward = Vector3Ext.Add(forward, right);
            newForward = Vector3Ext.Add(newForward, up);
            //_logger.InfoFormat("spreadX {0}, spreadY {1}, x {2} y {3}, right{4}, up{5}", spreadX, spreadY, x, y, right, up);
            return newForward;
        }


    }
}