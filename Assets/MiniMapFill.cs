using Assets.Scripts.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapFill : MonoBehaviour {
    public GameObject Floor, Wall, empty, playerMark, row;
    public Map Map;
    public GameObject player;
	// Use this for initialization
	void Start () {        
        Map.onMapChanged.AddListener(ReGenMiniMap);
        GenMinimap();
    }
    private void OnEnable()
    {
        PlayerPos();
    }
    private void OnDisable()
    {
        
    }
    private void ReGenMiniMap()
    {
        foreach (Transform t in this.transform)
            Destroy(t.gameObject);
        GenMinimap();
    }
    public GameObject mapBox;
    private void PlayerPos()
    {
        Vector3 plpos = Map.MapPositionFromWorld(player.transform.position);

        Vector3 newPos = new Vector3((((int)plpos.z) * 10), -(((int)plpos.x) * 10), 0);
        playerMark.GetComponent<RectTransform>().anchoredPosition = newPos;

        Vector3 boxpos = mapBox.GetComponent<RectTransform>().anchoredPosition;
        boxpos.y = -(new Vector2(player.transform.position.x, player.transform.position.z).magnitude);
        mapBox.GetComponent<RectTransform>().anchoredPosition = boxpos;

    }
    private void GenMinimap()
    {
        if (Map == null)
            Map = GameObject.Find("Map").GetComponent<Map>();
        Debug.Log("genMiniMap");
        GameObject CurRow;
        Vector3 playerpos = Map.MapPositionFromWorld(player.transform.position);
        for (int x = 0; x < Map.MapParams.MapBounds.x; ++x)
        {
            CurRow = Instantiate(row, this.transform);
            CurRow.name = x + "";
            for (int z = 0; z < Map.MapParams.MapBounds.z; ++z)
            {
                GameObject Tile;
               
                if (Map.SectorMap[x, z] == 0)
                    Tile = Instantiate(empty, CurRow.transform);
                else if (Map.TileTypeMap[x, z] == TileType.Floor)
                    Tile = Instantiate(Floor, CurRow.transform);
                else Tile = Instantiate(Wall, CurRow.transform);
                Tile.name = "" + z;
            }
        }
    }
}
