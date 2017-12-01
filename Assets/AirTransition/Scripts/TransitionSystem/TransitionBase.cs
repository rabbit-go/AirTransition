using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AirTransition
{
    public class TransitionBase : ITransitiontable
    {
        protected IEnumerator FadeCorutine, transition = null;
        protected Image wallpaper;
        protected SpriteRenderer renderer;
        private float FadeTime;
        private bool optimiseSetting;

        // Use this for initialization
        public void Begin(GameObject target)
        {
            Begin(target, Color.black);
        }

        virtual public void Begin(GameObject target, Color transitionColor)
        {
            if (target == null)
            {
                throw new System.ArgumentNullException();
            }
            if (optimiseSetting)
            {
                var camera = target.GetComponent<Camera>();
                if (camera == null) camera = target.AddComponent<Camera>();
                camera.orthographic = true;
                camera.orthographicSize = 1;
                camera.nearClipPlane = 0.009f;
                camera.farClipPlane = 0.01f;
                camera.clearFlags = CameraClearFlags.Nothing;
                camera.depth = 255;

                GameObject spritegameobject = new GameObject("Sprite");
                spritegameobject.transform.parent = target.transform;
                spritegameobject.transform.position = camera.transform.position + new Vector3(0, 0, 0.01f);
                spritegameobject.transform.rotation = camera.transform.rotation;

                if (renderer == null) renderer = spritegameobject.AddComponent<SpriteRenderer>();
                renderer.sprite = Sprite.Create(new Texture2D(Screen.width, Screen.height), new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));
                renderer.color = transitionColor;
                renderer.sortingOrder = 255;

                renderer.enabled = false;
            }
            else
            {
                var camera = target.GetComponent<Camera>();
                if (camera == null) camera = target.AddComponent<Camera>();

                var chanvas = target.GetComponent<Canvas>();
                if (chanvas == null) chanvas = target.AddComponent<Canvas>();

                var chanvasscaler = target.GetComponent<CanvasScaler>();
                if (chanvasscaler == null) chanvasscaler = target.AddComponent<CanvasScaler>();

                chanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                chanvas.sortingOrder = 255;

                wallpaper = target.GetComponent<Image>() ?? target.AddComponent<Image>();
                wallpaper.sprite = Sprite.Create(new Texture2D(Screen.width, Screen.height), new Rect(0, 0, Screen.width, Screen.height), new Vector2(0.5f, 0.5f));
                wallpaper.color = transitionColor;
                wallpaper.raycastTarget = false;
                wallpaper.enabled = false;
            }
        }

        // Update is called once per frame
        public void Process()
        {
            if (FadeCorutine != null && !FadeCorutine.MoveNext())
            {
                if (endFadeIn && !endFadeOut)
                {
                    SetEnableRenderer(true);
                    OutTransition(FadeTime);
                }
                else
                {
                    SetEnableRenderer(false);
                    FadeCorutine = null;
                }
            }
        }

        public void SetEndInTransition(IEnumerator endTransition)
        {
            if (endTransition == null)
            {
                throw new System.ArgumentNullException();
            }
            transition = endTransition;
        }

        protected virtual IEnumerator TransitionProcess(float time, bool isFadeIn)
        {
            throw new System.NotSupportedException();
        }

        protected bool endFadeIn, endFadeOut;

        protected virtual IEnumerator InTransition(float time)
        {
            yield return null;
        }

        protected virtual IEnumerator TransitionOut(float time)
        {
            throw new System.NotSupportedException();
        }

        public void Transition(float time)
        {
            if (time < 0)
            {
                throw new System.ArgumentException();
            }
            FadeTime = time;
            if (transition == null)
            {
                throw new System.NullReferenceException();
            }
            FadeCorutine = InTransition(time);
        }

        protected void OutTransition(float time)
        {
            if (time < 0)
            {
                throw new System.ArgumentException();
            }
            FadeCorutine = TransitionOut(time);
        }

        protected void Setmaterial(Material mat)
        {
            if (optimiseSetting)
            {
                renderer.material = mat;
            }
            else
            {
                wallpaper.material = mat;
            }
        }

        protected void SetmaterialFloat(string name, float value)
        {
            if (optimiseSetting)
            {
                renderer.gameObject.GetComponent<SpriteRenderer>().material.SetFloat(name, value);
            }
            else
            {
                wallpaper.gameObject.GetComponent<Image>().material.SetFloat(name, value);
            }
        }

        protected void SetEnableRenderer(bool isEnable)
        {
            if (optimiseSetting)
            {
                if(renderer.enabled != isEnable)
                {
                    renderer.enabled = isEnable;
                }
            }
            else
            {
                if (wallpaper.enabled != isEnable)
                {
                    wallpaper.enabled = isEnable;
                }
            }
        }

        public void SetOptimiseSetting(bool isEnable)
        {
            optimiseSetting = isEnable;
        }
    }
}