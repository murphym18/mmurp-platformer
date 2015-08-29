using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace World
{

    public class TileSet : ScriptableObject, ISerializationCallbackReceiver
    {
        [System.Serializable]
        public struct Tile
        {
            public char Key;
            public string description;
            public Transform prefab;
        }

        public List<Tile> tiles;

        private Dictionary<char, Transform> prefabs = new Dictionary<char, Transform>();
        private Dictionary<char, string> descriptions = new Dictionary<char, string>();

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            foreach(Tile t in tiles)
            {
                if (t.Key != '0')
                {
                    prefabs[t.Key] = t.prefab;
                    descriptions[t.Key] = t.description;
                }
                
            }

        }

        public string GetDescription(char c)
        {
            return descriptions[c];
        }

        public Transform GetPrefab(char c)
        {
            Transform value;
            if (prefabs.TryGetValue(c, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }
    }
}

