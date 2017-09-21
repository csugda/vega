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
    public int Height { get; set; }
    public int Width { get; set; }

    private int[][] map;

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
        //TODO: ADD GENERATION MECHANIC
    }
}
