using System.Collections.Generic;
using Akali.Common;
using UnityEngine;

namespace Akali.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PoolList", menuName = App.AppName + "/Pools/PoolList")]
    public class PoolListScriptableObject : ScriptableObject
    {
        public List<PoolScriptableObject> list;
    }
}