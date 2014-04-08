using UnityEngine;
using System.Collections;
using UnityEditor;

public class GameManager : MonoBehaviour {

    public const float TILE_SIZE = 128.0f / 100.0f;

    private int _width = 25;
    private int _height = 16;

	// Use this for initialization
	void Start () {
        GameObject bushes = GameObject.Find("Bushes");
        Object bushPrefab = Resources.Load("Prefabs/Bush");
        Object playerPrefab = Resources.Load("Prefabs/Player");

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (Random.Range(0, 100) < 13)
                    break;

                GameObject gameObject = GameObject.Instantiate(bushPrefab) as GameObject;
                gameObject.transform.localPosition = new Vector3(x * TILE_SIZE, y * TILE_SIZE);
                gameObject.transform.parent = bushes.transform;
            }
        }

        GameObject player = GameObject.Instantiate(playerPrefab) as GameObject;
        player.transform.localPosition = new Vector3(10 * TILE_SIZE, 10 * TILE_SIZE );

        // position camera in the middle
        Camera.main.transform.localPosition = new Vector3(((_width * TILE_SIZE) / 2), (_height * TILE_SIZE) / 2, Camera.main.transform.localPosition.z); 
	}

    // Update is called once per frame
    void Update()
    {
	
	}
}
