using System.Collections;
using UnityEngine;

namespace AirTransition
{
    /// <summary>
    /// Fadeの中身ですTransitionBaseより動作内容だけ書き換え
    /// </summary>
    public class Fade : TransitionBase
    {
        private Color fadealpha;

        override protected IEnumerator TransitionProcess(float time, bool isFadeIn)
        {
            
            fadealpha = wallpaper.color;
            float firsttime = Time.realtimeSinceStartup, nowtime = 0;//realtimeで行うことによりtimescaleをいじった時も対応
            while (time > nowtime)
            {
                nowtime = Time.realtimeSinceStartup - firsttime;
                fadealpha.a = isFadeIn ? nowtime / time : 1 - (nowtime / time);
                yield return null;
            }
           
        }

        override protected IEnumerator InTransition(float time)
        {
            IEnumerator enumerator = TransitionProcess(time, true);
            while (enumerator.MoveNext())
            {
                wallpaper.color = fadealpha;
                yield return null;
            }
            while (transition.MoveNext())
            {
                while (!AirTransition.Corutine.StartCorutine(transition))
                {
                    yield return null;
                }
                SetEnableRenderer(false);//Todo:綺麗な実装を考える、このままだとロードができない場合がある。
            }
            endFadeIn = true;
        }

        override protected IEnumerator TransitionOut(float time)
        {
            IEnumerator enumerator = TransitionProcess(time, false);
            while (enumerator.MoveNext())
            {
                wallpaper.color = fadealpha;
                yield return null;
            }
            endFadeOut = true;
        }
    }
}