using System.Collections;
using UnityEngine;

namespace AirTransition
{
    internal interface ITransitiontable
    {
        void Begin(GameObject target);

        void Begin(GameObject target, Color color);

        void Process();

        void Transition(float time);

        void SetEndInTransition(IEnumerator transition);

        void SetOptimiseSetting(bool isEnable);
    }
}