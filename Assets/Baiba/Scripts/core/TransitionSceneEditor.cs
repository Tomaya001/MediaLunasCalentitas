using com.baiba.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace com.baiba.CustomEditors
{
    [CustomEditor(typeof(TransitionSceneScrip))]
    public class TransitionSceneEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var transition = target as TransitionSceneScrip;

            EditorGUILayout.LabelField("Scene Manager Custom", EditorStyles.boldLabel);
            transition.isPasive = EditorGUILayout.Toggle("Modo de Uso del SMC", transition.isPasive, new GUILayoutOption[0]);
            if (transition.isPasive)
            {
                transition.time = EditorGUILayout.FloatField("Tiempo de Espera", transition.time, new GUILayoutOption[0]);
                transition.scene = EditorGUILayout.TextField("Escena Destino", transition.scene, new GUILayoutOption[0]);
            }

        }
    }
}

