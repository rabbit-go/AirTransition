using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AirTransition
{
    public class WaitForSeconds
    {
        /// <summary>
        /// Process is done 処理が終了したか 
        /// </summary>
        public bool isDone { get { return settingTime < Time.time; } }
        private bool isdone;
        private float settingTime;

        public WaitForSeconds(float time)
        {
            settingTime = time + Time.time;
        }
    }
    public class WaitForSecondsRealtime
    {
        /// <summary>
        /// Process is done 処理が終了したか 
        /// </summary>
        public bool isDone { get { return settingTime < Time.realtimeSinceStartup; } }
        private bool isdone;
        private float settingTime;

        public WaitForSecondsRealtime(float time)
        {
            settingTime = time + Time.realtimeSinceStartup;
        }
    }
    public class Corutine
    {
        public static bool StartCorutine(IEnumerator corutine)
        {

            if (corutine.Current is WaitForSeconds)
            {
                var wait = corutine.Current as WaitForSeconds;
                return wait.isDone;
            }
            else if (corutine.Current is UnityEngine.WaitForSeconds)
            {
                Debug.LogError("This Function is not Suppoted Please use AirTransition.WaitForSeconds instead");
                return true;
            }
            else if (corutine.Current is UnityEngine.WaitForSecondsRealtime)
            {
                Debug.LogError("This Function is not Suppoted Please use AirTransition.WaitForSecondsRealtime instead");
                return true;
            }
            else if (corutine.Current is WaitForSecondsRealtime)
            {
                var wait = corutine.Current as WaitForSecondsRealtime;
                return wait.isDone;
            }
            else if(corutine.Current is AsyncOperation)
            {
                var async = corutine.Current as AsyncOperation;
                return async.isDone;
            }

            else
            {
                return true;
            }
        }
    }

}