//AuthorName : 梁振东;
//CreateDate : 9/28/2019 2:39:41 PM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XluaFramework;
using System;
using System.Reflection;
using System.Linq;
namespace lzdUnityEditor
{
    [CustomPropertyDrawer(typeof(LuaInjection))]
    public class LuaInjectionPropertyDrawer : PropertyDrawer
    {
        LuaInjectionPropertyDrawer()
        {
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(IsSupportedAssembly)
            .ToList();
            typeList.AddRange
            (
                from assembly in assemblies
                from type in assembly.GetExportedTypes()
                where IsSupportedType(type)
                select type
            );
        }
        bool IsSupportedAssembly(Assembly type)
        {
            string typeName = type.GetName().Name;
            if (typeName.StartsWith("UnityEditor") || typeName.StartsWith("Editor")) return false;
            if (typeName.StartsWith("Unity") || typeName.StartsWith("Assembly-CSharp")) return true;
            return false;
        }
        bool IsSupportedType(Type type)
        {
			return type.IsSubclassOf(typeof(UnityEngine.Object));
        }
        private List<Type> typeList = new List<Type>
        {
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(string),
            typeof(Color),
            typeof(Vector2),
            typeof(Vector3)
        };

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
			EditorGUI.BeginProperty(rect, label, property);
			float width = rect.width / 3;
			float space = 5;
			rect.width = width - space;
			rect.height = EditorGUIUtility.singleLineHeight;
			DrawTypeButton(rect, property);
			rect.x += width;
		    DrawKeyButton(rect, property);
			rect.x += width;
			DrawValueButton(rect, property);
			EditorGUI.EndProperty();
			property.serializedObject.ApplyModifiedProperties(); //更新
        }
		void DrawTypeButton(Rect rect, SerializedProperty property)
		{
			float typeBtnWidth = 30;
		    SerializedProperty typeName = property.FindPropertyRelative("typeName");
			if(GUI.Button(new Rect(rect.x, rect.y, typeBtnWidth, rect.height), "Type", EditorStyles.miniButton))
			{
				DrawTypeMenu(delegate(object result)
				{
					typeName.stringValue = result.GetType().FullName;
					property.serializedObject.ApplyModifiedProperties();
				});
			}
			rect.x += typeBtnWidth;
			rect.width -= typeBtnWidth;
			EditorGUI.DelayedTextField(rect, typeName);
		}
		void DrawTypeMenu(GenericMenu.MenuFunction2 callback)
		{
			GenericMenu genericMenu = new GenericMenu();
            for(int i = 0; i < typeList.Count; ++i)
			{
				string spaceName = typeList[i].Namespace;
				string typeName = typeList[i].FullName;
				string menuContent;
				if(string.IsNullOrEmpty(spaceName))
				{
					menuContent = "NoSpace/" + typeName.Substring(0, 1) + "/" + typeName.Replace(".", "/"); 
				}
				else
				{
                    menuContent = spaceName + "/" + typeName.Substring(0, 1) + "/" + typeName.Replace(".", "/");
				}
				genericMenu.AddItem(new GUIContent(menuContent), true, callback, GUIContent.none);
			}
			genericMenu.ShowAsContext();
		}
		void DrawKeyButton(Rect rect, SerializedProperty property)
		{
            SerializedProperty keyName = property.FindPropertyRelative("keyName");
		    EditorGUI.PropertyField(rect, keyName);
		}
		void DrawValueButton(Rect rect, SerializedProperty property)
		{
			SerializedProperty valueObjectRefence = property.FindPropertyRelative("objectValue");
			
		}
    }
} 
