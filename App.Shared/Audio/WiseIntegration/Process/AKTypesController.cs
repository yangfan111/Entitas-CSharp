using App.Shared.Util;
<<<<<<< HEAD
using Core.Utils;
=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
using System.Collections.Generic;
using UnityEngine;
using XmlConfig;

namespace App.Shared.Audio
{
    public class AKTypesController
    {
        /// <summary>
        /// eventId ==>event
        /// </summary>
        private readonly Dictionary<int, AKEventAtom> events = new Dictionary<int, AKEventAtom>();
        /// <summary>
        /// gameobject=>switchStates
        /// </summary>
<<<<<<< HEAD
        private readonly Dictionary<GameObject, HashSet<AKSwitchAtom>> gameobjectSwitchGrps = new Dictionary<GameObject, HashSet<AKSwitchAtom>>();

        public AKTypesController()
        {
            AKEventAtom.onImplentment += PostEventFinalHandler;
            AKSwitchAtom.onImplentment += SwitchStateFinalHandler;
        }


        private void SwitchStateFinalHandler(AKSwitchAtom atom, GameObject target)
=======
        private readonly Dictionary<GameObject, List<AKSwitchAtom>> gameobjectSwitchGrps = new Dictionary<GameObject, List<AKSwitchAtom>>();

        public AKTypesController()
        {
            AKEventAtom.onImplentment += PostEventHandler;
            AKSwitchAtom.onImplentment += SwitchStateHandler;
        }


        private void SwitchStateHandler(AKSwitchAtom atom, GameObject target)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {
            AKRESULT result = AkSoundEngine.SetSwitch(atom.config.Group, atom.currState, target);
            AudioUtil.AssertProcessResult(result, "set switch {1} {0}", atom.currState,target.name);
        }
<<<<<<< HEAD
        private void PostEventFinalHandler(AKEventAtom atom, GameObject target, bool firstPlayInObject)
        {
            //get switch 
=======
        private void PostEventHandler(AKEventAtom atom, GameObject target, bool firstPlayInObject)
        {
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            if (firstPlayInObject && atom.attachedGrps.Count > 0)
            {
                foreach (int grp in atom.attachedGrps)
                {
                    RegisterGetSwitch(target, grp);
                }
            }
            AkSoundEngine.PostEvent(atom.evtName, target);
        }
        /// <summary>
        /// switch获取
        /// </summary>
        /// <param name="target"></param>
        /// <param name="grpId"></param>
        /// <returns></returns>
        public AKSwitchAtom GetSwitch(GameObject target, int grpId)
        {
<<<<<<< HEAD
            HashSet<AKSwitchAtom> switchAtoms;
=======
            List<AKSwitchAtom> switchAtoms;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            if (gameobjectSwitchGrps.TryGetValue(target, out switchAtoms))
            {
                foreach (var atom in switchAtoms)
                {
                    if (atom.config.Id == grpId)
                        return atom;
                }
            }
            return null;
        }
        /// <summary>
        /// 新switch注册获取
        /// </summary>
        /// <param name="target"></param>
        /// <param name="grpId"></param>
        /// <param name="stateIndex"></param>
        /// <returns></returns>
        public AKSwitchAtom RegisterGetSwitch(GameObject target, int grpId, int stateIndex = -1)
        {
<<<<<<< HEAD
            AssertUtility.Assert(target != null);
            HashSet<AKSwitchAtom> switchAtoms;
=======
            CommonUtil.WeakAssert(target != null);
            List<AKSwitchAtom> switchAtoms;
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            AKSwitchAtom ret;
            if (gameobjectSwitchGrps.TryGetValue(target, out switchAtoms))
            {
                foreach (var atom in switchAtoms)
                {
                    if (atom.config.Id == grpId)
                        return atom;
                }
                ret = new AKSwitchAtom(grpId, stateIndex, target);
                switchAtoms.Add(ret);
            }
            else
            {
                ret = new AKSwitchAtom(grpId, stateIndex, target);
<<<<<<< HEAD
                switchAtoms = new HashSet<AKSwitchAtom>() { ret };
=======
                switchAtoms = new List<AKSwitchAtom>() { ret };
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
                gameobjectSwitchGrps.Add(target, switchAtoms);
            }
            return ret;
        }
        public AKEventAtom RegisterGetEvt(AudioEventItem config)
        {
            AKEventAtom evt;
            if (!events.TryGetValue(config.Id, out evt))
            {
                evt = new AKEventAtom(config);
                events.Add(config.Id, evt);
            }
            return evt;
        }


    }
}
