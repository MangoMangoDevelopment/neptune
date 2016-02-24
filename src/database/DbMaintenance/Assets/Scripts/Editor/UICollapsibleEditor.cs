using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UnityEditor.UI
{
	[CustomEditor(typeof(UICollapsible), true)]
	public class UICollapsibleEditor : Editor {
		
		public override void OnInspectorGUI()
		{
			this.serializedObject.Update();
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("m_MinHeight"));
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("m_Transition"));
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("m_TransitionDuration"));
			EditorGUILayout.PropertyField(this.serializedObject.FindProperty("m_CurrentState"));
			this.serializedObject.ApplyModifiedProperties();
			
			VerticalLayoutGroup vlg = (target as UICollapsible).transform.parent.gameObject.GetComponent<VerticalLayoutGroup>();
			
			if (vlg == null || !vlg.enabled)
			{
				EditorGUILayout.HelpBox("The parent transform does not have a Vertical Layout Group component. The UICollapsible will not function.", MessageType.Warning);
			}
		}
	}
}