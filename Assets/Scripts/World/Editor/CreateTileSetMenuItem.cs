using UnityEngine;
using System.Collections;
using UnityEditor;
namespace World
{
    public class CreateTileSetMenuItem
    {
        [MenuItem("Assets/Create/TileSet")]
        public static void CreateTileSet()
        {
            TileSet tileSet = ScriptableObject.CreateInstance<TileSet>();
            AssetDatabase.CreateAsset(tileSet, "Assets/New TileSet.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = tileSet;
        }
    }

}
