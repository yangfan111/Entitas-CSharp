
using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Shared.Audio
{
    public class AKBankAtomSet
    {
        private readonly Dictionary<string, AKBankAtom> bankAtomContenter = new Dictionary<string, AKBankAtom>();
        //完整加载的bank列表
        private readonly HashSet<AKBankAtom> loadedBanks = new HashSet<AKBankAtom>();
        //加载中的bank列表
        private readonly HashSet<AKBankAtom> loadingBanks = new HashSet<AKBankAtom>();
        //  private readonly HashSet<string> bankOnLoadIdList = new HashSet<string>();

        public AKBankAtomSet()
        {
<<<<<<< HEAD
            AKBankAtom.onLoadBefore += OnLoadPrepare;
            AKBankAtom.onLoadFinish += OnLoadResult;
        }

        public AKBankAtom Register(BankLoadRequestStruct requestData, bool loadIfSucess)
=======
            AKBankAtom.onLoadPrepare += OnLoadPrepare;
            AKBankAtom.onLoadFinish += OnLoadResult;
        }

        public AKBankAtom Register(BankLoadRequestData requestData, bool loadIfSucess)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {
            AKBankAtom atom;
            if (!bankAtomContenter.TryGetValue(requestData.bnkName, out atom))
            {
                atom = new AKBankAtom(requestData.bnkName, requestData.actionType, requestData.modelType);
                bankAtomContenter.Add(requestData.bnkName, atom);
                if (loadIfSucess)
                {
                    atom.Load(null, null);
                }
            }
            return atom;

        }
        public AKBankAtom Get(string bnk)
        {
            AKBankAtom atom;
            bankAtomContenter.TryGetValue(bnk, out atom);
            return atom;
        }
        public AKRESULT Vertify(AKBankAtom atom)
        {
            if (loadedBanks.Contains(atom))
                return AKRESULT.AK_BankAlreadyLoaded;
            if (loadingBanks.Contains(atom))
                return AKRESULT.AK_BankInLoadingQueue;
            return AKRESULT.AK_Success;
        }

        void OnLoadPrepare(AKBankAtom atom)
        {
            loadingBanks.Add(atom);
        }

<<<<<<< HEAD
        void OnLoadResult(BankLoadResponseStruct response)
=======
        void OnLoadResult(BankLoadResponseData response)
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        {
            loadingBanks.Remove(response.atom);
            if (response.loadResult.Sucess())
            {
                loadedBanks.Add(response.atom);
            }
            if (response.callback != null)
                response.callback(response);
            else
                AudioUtil.AssertProcessResult(response.loadResult, "load {0}", response.atom.BankName);
        }
    }


}
