using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AirTransition
{
    public class Transition : SingletonMonoBehaviour<Transition>
    {
        public enum TransitionType
        {
            Fade,
            TransitionAlpha,
            TransitionCutOut
        }

        [SerializeField]
        public TransitionType transitionType = TransitionType.Fade;

        [SerializeField]
        public Material transitionAlphaMaterial, transitionCutoutMaterial;

        [SerializeField]
        public bool optimiseSetting;

        [SerializeField]
        public Texture transitionTexture;

        [SerializeField]
        public Color transitionColor;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (transitionTable != null)
            {
                transitionTable.Process();
            }
        }

        private ITransitiontable transitionTable = null;
        /// <summary>
        /// シーンをロードします
        /// </summary>
        /// <param name="sceneName">シーンの名前</param>
        /// <param name="transSec">シーンを遷移するのにかける時間</param>
        public void LoadScene(string sceneName, float transSec = 2.0f)
        {
            LoadScene(TransitionProcess(sceneName), transSec);
        }
        /// <summary>
        /// シーンをNowLoadingなどを出しながら、ロードします
        /// </summary>
        /// <param name="sceneName">シーンの名前</param>
        /// <param name="LoadingSceneName">NowLoadingなどのローディング中に出したいシーンの名前</param>
        /// <param name="transSec">シーンを遷移するのにかける時間</param>
        public void LoadScene(string sceneName,string LoadingSceneName, float transSec = 2.0f)
        {
            LoadScene(TransitionProcessWithNowLoading(sceneName, LoadingSceneName), transSec);
        }
        /// <summary>
        /// シーンをロードします、このオーバーロードではIEnumaratorを使ってロード処理を設定することが可能です
        /// </summary>
        /// <param name="loadingProcess">Corutineで実行したい内容を入力(注:yieldで返却できるのはネームスペースAirTransitionにある WaitForSeconds,WaitForSecondsRealtimeと本来のAsyncOperation,nullのみです)</param>
        /// <param name="transSec"></param>
        public void LoadScene(IEnumerator loadingProcess, float transSec = 2.0f)
        {
            switch (transitionType)
            {
                case TransitionType.Fade:
                    transitionTable = new Fade();
                    break;

                case TransitionType.TransitionAlpha:
                    if (transitionAlphaMaterial == null)
                    {
                        Shader shader = Shader.Find("UI/TransitionAlpha");
                        transitionAlphaMaterial = new Material(shader);
                    }
                    if(transitionTexture == null)
                    {
                        throw new System.NullReferenceException();
                    }
                    transitionAlphaMaterial.SetTexture("_MaskTex", transitionTexture);
                    transitionTable = new Transition_alpha(transitionAlphaMaterial);
                    break;

                case TransitionType.TransitionCutOut:
                    if (transitionCutoutMaterial == null)
                    {
                        Shader shader = Shader.Find("UI/TransitionCutOut");
                        transitionCutoutMaterial = new Material(shader);
                    }
                    if (transitionTexture == null)
                    {
                        throw new System.NullReferenceException();
                    }
                    transitionCutoutMaterial.SetTexture("_MaskTex", transitionTexture);
                    transitionTable = new Transition_alpha(transitionCutoutMaterial);
                    break;

                default:
                    throw new System.ArgumentOutOfRangeException("Invalid TransitionType value");
            }
            transitionTable.SetOptimiseSetting(optimiseSetting);
            transitionTable.Begin(gameObject, transitionColor);
            transitionTable.SetEndInTransition(loadingProcess);
            transitionTable.Transition(transSec);
        }

        private IEnumerator TransitionProcess(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            yield return null;
        }
        private IEnumerator TransitionProcessWithNowLoading(string sceneName,string LoadingSceneName)
        {
            SceneManager.LoadScene(LoadingSceneName);
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}