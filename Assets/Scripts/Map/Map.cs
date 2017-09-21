using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Map class holds all static information about the map.
/// This includes tile types, tile sets, height and width.
/// NOTE: The Map should not be responsible for handling any non-static data!
/// </summary>
public class Map : MonoBehaviour
{
    public int Height;
    public int Width;

    private int[][] map;

    public Map(int width, int height)
    {
        this.Width = width;
        this.Height = height;
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    //The generator will be responsible for generating the map
    public void Generate(MapGenerator mapGen)
    {
        mapGen.GenerateMap(this);
    }
}
