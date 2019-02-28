using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.AssetManager;

namespace Core.SceneManagement
{
    public interface ILevelManager : IDisposable
    {
        event Action<Scene, LoadSceneMode> SceneLoaded;
        event Action<Scene> SceneUnloaded;
        event Action<UnityObject> GoLoaded;
<<<<<<< HEAD
        event Action<UnityObject> AfterGoLoaded;
        event Action<UnityObject> BeforeGoUnloaded;
=======
>>>>>>> dd9404d40605492fb1a883a3bed4cad8c5b8b20a
        event Action<UnityObject> GoUnloaded;

        OriginStatus UpdateOrigin(Vector3 pos);
        void GoLoadedWrapper(string source, UnityObject go);

        void GetRequests(List<AssetInfo> sceneRequests, List<AssetInfo> goRequests);
        int NotFinishedRequests { get; }
    }
}