using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace com.baiba.core.leveleditor
{
    [CustomEditor(typeof(LevelBuilder))]
    public class LevelBuilderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            LevelBuilder builder = (LevelBuilder)target;
            base.OnInspectorGUI();
            if(GUILayout.Button("Guardar Escena"))
            {
                builder.SaveLevel();
            }
            
            if(GUILayout.Button("Cargar Escena"))
            {
                builder.LoadLevel();
            }
        }
    }
}
