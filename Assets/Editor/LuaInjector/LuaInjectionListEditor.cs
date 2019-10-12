//AuthorName : 梁振东;
//CreateDate : 9/27/2019 11:02:12 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using XluaFramework;
namespace lzdUnityEditor
{
    [CustomEditor(typeof(LuaInjectionList))]
    public class LuaInjectionListEditor : Editor
    {
		private ReorderableList reorderableList; //可拖曳列表
		private SerializedProperty m_injectProperty;
		protected virtual void OnEnable()
		{
			m_injectProperty = serializedObject.FindProperty("m_injections");
			reorderableList = new ReorderableList(serializedObject, m_injectProperty, true, true, true, true)
			{
				drawHeaderCallback = DrawHeaderCallBack,
				drawElementCallback = DrawElementList
			};
		}
		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			reorderableList.DoLayoutList();
			serializedObject.ApplyModifiedProperties();
		}
		void DrawHeaderCallBack(Rect rect)
		{
			rect.y += 1;
			float offset = 30;
			rect.x += offset;
		    float width = (rect.width - offset) / 2;
            EditorGUI.LabelField(rect, "Type");
			rect.x += rect.width - (offset + width);
			EditorGUI.LabelField(rect, "Value");
		}
		void DrawElementList(Rect rect, int index, bool isActive, bool isFocuse)
		{
            rect.y += 1;
			SerializedProperty itemData = m_injectProperty.GetArrayElementAtIndex(index);
			rect.height = EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(rect, itemData);
		}
    }
}
