//AuthorName : 梁振东;
//CreateDate : 9/28/2019 9:00:20 AM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XluaFramework
{
    public class LuaInjectionList : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField]
        protected List<LuaInjection> m_injections;
        protected List<LuaInjection> Injections;
        
        private Dictionary<object, LuaInjection> dic = new Dictionary<object, LuaInjection>();
        public Dictionary<object, LuaInjection> DicData
        {
            get
            {
                return dic;
            }
            set
            {
                if(value != null)
                dic = value;
            }
        }
        public LuaInjection this[string key]{get{ return dic[key];}}

        public void OnAfterDeserialize()
        {
            dic.Clear();
            for(int i = 0; i < m_injections.Count; ++i)
            {
                if(dic.ContainsKey(m_injections[i].TypeName)) continue;
                dic.Add(m_injections[i].TypeName, m_injections[i]);
            }
        }
        public void OnBeforeSerialize()
        {

        }
    }
}
