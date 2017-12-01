using System.Collections;
using UnityEngine;

namespace AirTransition
{
    public class Transition_alpha : TransitionBase
    {

        private float FadeTime;

        private Material wallpaperMat;

        public Transition_alpha(Material materia)
        {
            wallpaperMat = materia;
        }

        override public void Begin(GameObject target, Color transitionColor)
        {
            base.Begin(target, transitionColor);
            Setmaterial(wallpaperMat);
        }

        // Update is called once per frame
        private float Transitiontime;

        override protected IEnumerator TransitionProcess(float time, bool isFadeIn)
        {

            float firsttime = Time.realtimeSinceStartup, nowtime = 0;
            while (time > nowtime)
            {
                nowtime = Time.realtimeSinceStartup - firsttime;
                Transitiontime = isFadeIn ? nowtime / time : 1 - (nowtime / time);
                SetmaterialFloat("_Range", Transitiontime);
                yield return null;
            }

        }

        override protected IEnumerator InTransition(float time)
        {
            IEnumerator enumerator = TransitionProcess(time, true);
            SetEnableRenderer(true);
            while (enumerator.MoveNext())
            {
                yield return null;
            }

            
            while (transition.MoveNext())
            {
                
                while (!AirTransition.Corutine.StartCorutine(transition))
                {
                    yield return null;
                    SetEnableRenderer(false);//Todo:綺麗な実装を考える、このままだとn秒後にロードができなくなる場合がある。
                }
            }
            endFadeIn = true;
        }

        override protected IEnumerator TransitionOut(float time)
        {
            IEnumerator enumerator = TransitionProcess(time, false);
            while (enumerator.MoveNext())
            {
                yield return null;
            }
            endFadeOut = true;
        }
    }
}