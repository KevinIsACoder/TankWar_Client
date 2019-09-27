using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AUTHOR : 梁振东
//DATE : 9/27/2019 6:05:22 PM
//DESC : ****
namespace lzdUnityEditor
{
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
