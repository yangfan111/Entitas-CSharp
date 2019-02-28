using UnityEngine;
using System.Collections.Generic;
using System;
using XmlConfig;


namespace App.Shared.Audio
{


    public class AKAudioDispatcher
    {
        public AudioRegulator Regulator { get; private set; }
        //    public AKAudioEngineDriver Driver { get; private set; }
        private AudioBankLoader bankLoader;

        private readonly AKTypesController typesController = new AKTypesController();
        public AKAudioDispatcher(AudioBankLoader loader)
        {
            typesController = new AKTypesController();
            bankLoader = loader;
        }
       
        public AKAudioDispatcher()
        {

            //OnConfigAssetLoadFinish+= config load finish
        }


        public void PrepareEvent(int eventId, GameObject target)
        {
            //typesController.VarySwitchState()
        }
        /// <summary>
        /// 播放音频
        /// </summary>
        /// <param name="econfig"></param>
        /// <param name="target"></param>
        public void PostEvent(AudioEventItem econfig, GameObject target)
        {
            bankLoader.LoadAtom(econfig.BankRef, LoadResultHandler, target, econfig);
           
        }
        public void SetSwitch(GameObject target, AudioGrp_ShotModelIndex shotModelGrpIndex)
        {
<<<<<<< HEAD
           AKSwitchAtom switchAtom =  typesController.RegisterGetSwitch(target, (int)AudioGrp_ShotModelIndex.Id, (int)shotModelGrpIndex);
           switchAtom.SetSwitch((int)shotModelGrpIndex, target);
        }
    
        private void LoadResultHandler(BankLoadResponseStruct response)
=======
            typesController.RegisterGetSwitch(target, (int)AudioGrp_ShotModelIndex.Id, (int)shotModelGrpIndex);
        }


        private void LoadResultHandler(BankLoadResponseData response)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {

            if (!response.loadResult.Sucess())
            {
                AudioUtil.ELog("result:{0} bank name:{1}", response.loadResult, response.bnkName);
                return;
            }
<<<<<<< HEAD
            //get bank post -> loadresult->bank.register
            if (response.loadResult == AKRESULT.AK_Success)
=======
            if(response.loadResult == AKRESULT.AK_Success)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            {
                response.atom.Register(response.target);

            }
<<<<<<< HEAD
            //get evt
            AKEventAtom evtAtom = typesController.RegisterGetEvt((AudioEventItem)response.userData);
            //get bank
=======
            AKEventAtom evtAtom = typesController.RegisterGetEvt((AudioEventItem)response.userData);
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
            evtAtom.PostEvent(response.target);
        }
        

        public void PrepareEvent(int eventId)
        {

        }

        public void PrepareBank(string bankName)
        {

        }
        void OnAsyncBnkLoadSucess(string bankName)
        {

        }
        void OnAsyncBnkALoadRefUpdate(string bankName)
        {

        }
        void OnAsyncBnkALoadFail(string bankName)
        {

        }
        //private static Dictionary<AudioTriggerEventType, string> triggerTypeBehaviors;
        //public static Dictionary<AudioTriggerEventType, string> TriggerTypeBehaviors
        //{
        //    get
        //    {
        //        if (triggerTypeBehaviors == null)
        //        {
        //            triggerTypeBehaviors = new Dictionary<AudioTriggerEventType, string>();
        //            Type et = typeof(AudioTriggerEventType);
        //            Array enumArr = System.Enum.GetValues(et);
        //            foreach (AudioTriggerEventType etype in enumArr)
        //            {
        //                string strName = Enum.GetName(et, etype);
        //                triggerTypeBehaviors.Add(etype, "On" + strName);
        //            }
        //        }
        //        return triggerTypeBehaviors;
        //    }
        //}
    }
}
