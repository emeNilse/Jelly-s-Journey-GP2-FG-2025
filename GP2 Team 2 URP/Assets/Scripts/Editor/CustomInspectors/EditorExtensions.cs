using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public  class EditorExtensions : EditorWindow
{
    
    public void DrawProperties(SerializedProperty property, bool drawChildren)
    {
        string lastPropPath = string.Empty;

        foreach(SerializedProperty p in property)
        {
            if(p.isArray && p.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUILayout.BeginHorizontal();
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                EditorGUILayout.EndHorizontal();

                if(p.isExpanded )
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(p, drawChildren );
                    EditorGUI.indentLevel--;
                }
            }
            else 
            {
                if(!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath))
                {
                    continue;
                }
                lastPropPath = p.propertyPath;
                EditorGUILayout.PropertyField(p, drawChildren);
            }
        }
    }
}
