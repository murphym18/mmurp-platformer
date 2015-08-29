using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateWorld : MonoBehaviour {
    public World.TileSet tiles;
    public int tileWidth = 3;
    public int tileHeight = 3;

    private delegate string ReadMapData();
    private ReadMapData mapReader;
    private List<Transform> createdTiles = new List<Transform>();

#if UNITY_WEBGL && !UNITY_EDITOR
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern string ReadMapDataFromDom();
#else
    public TextAsset testMapData;
    private string ReadTestMapData()
    {
        return testMapData.text;
    }
#endif

    void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        mapReader = ReadMapDataFromDom;
#else
        mapReader = ReadTestMapData;
#endif
    }

    // Use this for initialization
    void Start () {
        LoadMap();
	}

    void Refresh()
    {
        Application.LoadLevel("default_level");
    }

    void LoadMap()
    {
        Debug.Log("Creating World...");
        string data = mapReader();

        var lines = data.Split('\n');
        for (int row = 0; row < lines.Length; row++)
        {
            var line = lines[row].ToCharArray();
            for (int col = 0; col < line.Length; col++)
            {
                var c = line[col];
                var tile = tiles.GetPrefab(c);
                if (tile != null)
                {
                    var x = col * tileWidth;
                    var y = -row * tileHeight;
                    createdTiles.Add(Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity) as Transform);
                }
            }
        }
    }
}
