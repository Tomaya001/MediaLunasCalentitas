using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.baiba.core
{
    public class AssetLoader
    {
        public static T GetAsset<T>(string id) where T: Object
        {
            return Resources.Load<T>(id);
        }
    }
}

