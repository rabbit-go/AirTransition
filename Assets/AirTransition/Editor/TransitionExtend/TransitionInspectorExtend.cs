#if UNITY_EDITOR

using UnityEditor;

#endif

using UnityEngine;

namespace AirTransition
{
    [CustomEditor(typeof(Transition))]
    public class TransitionInspectorExtend : Editor
    {
        public override void OnInspectorGUI()
        {
            Transition transition = target as Transition;
            switch (transition.transitionType)
            {
                case Transition.TransitionType.Fade:
                    transition.transitionType = (Transition.TransitionType)EditorGUILayout.EnumPopup("TransitionType", transition.transitionType);
                    transition.transitionColor = EditorGUILayout.ColorField("transitioncolor", transition.transitionColor);
                    break;

                case Transition.TransitionType.TransitionAlpha:
                    transition.transitionType = (Transition.TransitionType)EditorGUILayout.EnumPopup("TransitionType", transition.transitionType);
                    transition.optimiseSetting = EditorGUILayout.Toggle("Perfomance Optimise", transition.optimiseSetting);
                    transition.transitionAlphaMaterial = EditorGUILayout.ObjectField("Material", transition.transitionAlphaMaterial, typeof(Material), true) as Material;
                    transition.transitionColor = EditorGUILayout.ColorField("transitioncolor", transition.transitionColor);
                    transition.transitionTexture = EditorGUILayout.ObjectField("Texture", transition.transitionTexture, typeof(Texture), true) as Texture;
                    break;

                case Transition.TransitionType.TransitionCutOut:
                    transition.transitionType = (Transition.TransitionType)EditorGUILayout.EnumPopup("TransitionType", transition.transitionType);
                    transition.optimiseSetting = EditorGUILayout.Toggle("Perfomance Optimise", transition.optimiseSetting);
                    transition.transitionCutoutMaterial = EditorGUILayout.ObjectField("Material", transition.transitionCutoutMaterial, typeof(Material), true) as Material;
                    transition.transitionColor = EditorGUILayout.ColorField("transitioncolor", transition.transitionColor);
                    transition.transitionTexture = EditorGUILayout.ObjectField("Texture", transition.transitionTexture, typeof(Texture), true) as Texture;
                    break;

                default:
                    break;
            }
        }
    }
}