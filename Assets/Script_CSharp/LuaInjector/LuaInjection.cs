using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
//AUTHOR : 梁振东
//DATE : 9/27/2019 6:05:22 PM
//DESC : ****
namespace XluaFramework
{
    [System.Serializable]
    public class LuaInjection
    {
        [SerializeField]
        private string typeName = typeof(Object).FullName;
        public string TypeName
        {
            get
            {
                return typeName;
            }
        }
        [SerializeField]
        private string keyName;
        public string KeyName
        {
            get
            {
                return keyName;
            }
        }
        [SerializeField]
        private Object objectValue;
        public Object ObjectValue
        {
            get
            {
                return objectValue;
            }
            set
            {
                objectValue = value;
            }
        }
    }
}
