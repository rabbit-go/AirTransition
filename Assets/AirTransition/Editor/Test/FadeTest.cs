using AirTransition;
using NUnit.Framework;
using System.Collections;
using UnityEngine;

[TestFixture]
public class FadeTest
{
    [Test]
    public void BiginTestNoamal()
    {
        Fade instance = new Fade();
        GameObject obj = new GameObject();
        instance.Begin(obj);
    }

    [Test]
    [TestCase(null)]
    public void BiginTestNull(GameObject obj)
    {
        Fade instance = new Fade();
        Assert.That(() => instance.Begin(obj), Throws.ArgumentNullException);
    }

    [Test]
    public void ProcessTestNoamal()
    {
        Fade instance = new Fade();
        GameObject obj = new GameObject();
        instance.Begin(obj);
        instance.SetEndInTransition(TestCase());
        instance.Process();
    }

    [Test]
    [TestCase(1.0f)]
    public void TransitionTestNoamal(float time)
    {
        Fade instance = new Fade();
        GameObject obj = new GameObject();
        instance.Begin(obj);
        instance.SetEndInTransition(TestCase());
        instance.Transition(time);
        instance.Process();
    }

    [Test]
    [TestCase(-1.0f)]
    public void TransitionTestInvalid(float time)
    {
        Fade instance = new Fade();
        GameObject obj = new GameObject();
        instance.Begin(obj);
        instance.SetEndInTransition(TestCase());
        Assert.That(() => instance.Transition(time), Throws.ArgumentException);
    }

    [Test]
    [TestCase(1.0f)]
    public void TransitionTestNullReference(float time)
    {
        Fade instance = new Fade();
        GameObject obj = new GameObject();
        instance.Begin(obj);
        //instance.SetEndInTransition(TestCase());
        Assert.That(() => { instance.Transition(time); }, Throws.TypeOf<System.NullReferenceException>());
    }

    [TestCase(1.0f)]
    public void TransitionTestNullReference2(float time)
    {
        Fade instance = new Fade();
        GameObject obj = new GameObject();
        instance.Begin(obj);
        //instance.SetEndInTransition(null);
        Assert.That(() => { instance.Transition(time); }, Throws.TypeOf<System.NullReferenceException>());
    }

    [Test]
    public void TransitionTestNoamal()
    {
        Fade instance = new Fade();
        GameObject obj = new GameObject();
        var camera = obj.AddComponent<Camera>();
        instance.SetEndInTransition(TestCase());
    }

    [Test]
    public void TransitionTestNoamal2()
    {
        Fade instance = new Fade();
        GameObject obj = new GameObject();
        instance.SetEndInTransition(TestCase());
        instance.Begin(obj);
        instance.Transition(1.0f);
        instance.Process();
    }

    private IEnumerator TestCase()
    {
        yield return null;
    }
}