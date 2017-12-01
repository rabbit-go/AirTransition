using System.Collections;
using UnityEngine;
using AirTransition;
public class SendScene : MonoBehaviour
{
    // Use this for initialization
    public void Button()
    {
        Transition.Instance.LoadScene("Test2");
    }
    public void Button1()
    {
        Transition.Instance.LoadScene("Test2","NowLoading", 3.0f);
    }
    public void Button2()
    {
        Transition.Instance.LoadScene(TransitionProcess("Test2"), 3.0f);
    }
    private IEnumerator TransitionProcess(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        yield return new AirTransition.WaitForSeconds(3.0f);
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
    }
}