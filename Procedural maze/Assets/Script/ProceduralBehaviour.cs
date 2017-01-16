using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ProceduralBehaviour : MonoBehaviour {

    // The "maze" empty game object
    public GameObject m_Maze;

    // The corridor prefab
    public Object m_CorridorPrefab;

    // The crossroad prefab
    public Object m_CrossroadPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            ManageCorridor();
        }

	    // When the Character pass a certain point of a corridor manage the corridors
        
	}

    // Manage corridor creation.
    void ManageCorridor ()
    {
        // Can randomly create a crossroad.
        // Crossroads have a 1/5 chance to appear
        if (Random.Range(1, 6) == 5)
        {
            AddCrossRoad();
        }
        // Create only corridor.
        else
        {
            AddRandomCorridor();
        }

        // Remove unseen corridors.
        RemoveCorridor();
    }


    // Add a new corridor to the maze in a random direction if it does not cross the limit of navigation space
    void AddRandomCorridor ()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in m_Maze.transform)
        {
            children.Add(child.gameObject);
        }

        // Create instance of corridor
        GameObject corridor = Instantiate(m_CorridorPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // The position and orientation at which the corridor will be placed
        Vector3 endPosition = new Vector3();
        float endYOrientation = 0;
        switch (Random.Range(1, 5))
        {
            case 1:
                endYOrientation = 0;
                break;
            case 2:
                endYOrientation = 90;
                break;
            case 3:
                endYOrientation = 180;
                break;
            case 4:
                endYOrientation = 270;
                break;
        }

        // Corridor is placed at the end of the last corridor
        GameObject lastPlane = children[children.Count - 1];
        if (lastPlane.transform.rotation.eulerAngles.y == 90)
        {
            if (endYOrientation == 270 || endYOrientation == 90)
            {
                endYOrientation = 90;
                corridor.transform.position = new Vector3(lastPlane.transform.position.x + 10f, lastPlane.transform.position.y, lastPlane.transform.position.z);
            }
            else
            {
                if (endYOrientation == 0)
                {
                    corridor.transform.position = new Vector3(lastPlane.transform.position.x + 6.5f, lastPlane.transform.position.y, lastPlane.transform.position.z + 3.5f);
                }
                else if (endYOrientation == 180)
                {
                    corridor.transform.position = new Vector3(lastPlane.transform.position.x + 6.5f, lastPlane.transform.position.y, lastPlane.transform.position.z - 3.5f);
                }
            }
        }
        else if (lastPlane.transform.rotation.eulerAngles.y == 180)
        {
            if (endYOrientation == 0 || endYOrientation == 180)
            {
                endYOrientation = 180;
                corridor.transform.position = new Vector3(lastPlane.transform.position.x, lastPlane.transform.position.y, lastPlane.transform.position.z - 10f);
            }
            else
            {
                if (endYOrientation == 90)
                {
                    corridor.transform.position = new Vector3(lastPlane.transform.position.x + 3.5f, lastPlane.transform.position.y, lastPlane.transform.position.z - 6.5f);
                }
                else if (endYOrientation == 270)
                {
                    corridor.transform.position = new Vector3(lastPlane.transform.position.x - 3.5f, lastPlane.transform.position.y, lastPlane.transform.position.z - 6.5f);
                }
            }
        }
        else if (lastPlane.transform.rotation.eulerAngles.y == 0)
        {
            if (endYOrientation == 180 || endYOrientation == 0)
            {
                endYOrientation = 0;
                corridor.transform.position = new Vector3(lastPlane.transform.position.x, lastPlane.transform.position.y, lastPlane.transform.position.z + 10f);
            }
            else
            {
                if (endYOrientation == 90)
                {
                    corridor.transform.position = new Vector3(lastPlane.transform.position.x + 3.5f, lastPlane.transform.position.y, lastPlane.transform.position.z + 6.5f);
                }
                else if (endYOrientation == 270)
                {
                    corridor.transform.position = new Vector3(lastPlane.transform.position.x - 3.5f, lastPlane.transform.position.y, lastPlane.transform.position.z + 6.5f);
                }
            }
        }
        else if (lastPlane.transform.rotation.eulerAngles.y == 270)
        {
            if (endYOrientation == 90 || endYOrientation == 270)
            {
                endYOrientation = 270;
                corridor.transform.position = new Vector3(lastPlane.transform.position.x - 10f, lastPlane.transform.position.y, lastPlane.transform.position.z);
            }
            else
            {
                if (endYOrientation == 0)
                {
                    corridor.transform.position = new Vector3(lastPlane.transform.position.x - 6.5f, lastPlane.transform.position.y, lastPlane.transform.position.z + 3.5f);
                }
                else if (endYOrientation == 180)
                {
                    corridor.transform.position = new Vector3(lastPlane.transform.position.x - 6.5f, lastPlane.transform.position.y, lastPlane.transform.position.z - 3.5f);
                }
            }
            
        }
        corridor.transform.rotation = Quaternion.Euler(0f, endYOrientation, 0f);
        corridor.transform.SetParent(m_Maze.transform);
    }

    // Remove past corridor.
    void RemoveCorridor ()
    {
        // Remove old corridor
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in m_Maze.transform)
        {
            children.Add(child.gameObject);
        }
        while (children.Count > 7)
        {
            Destroy(children[0]);
            children.RemoveAt(0);
        }
    }

    // Add a new crossroad for the corridors.
    void AddCrossRoad ()
    {
        
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in m_Maze.transform)
        {
            children.Add(child.gameObject);
        }

        // Create instance of corridor
        GameObject crossroad = Instantiate(m_CrossroadPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        
        // Corridor is placed at the end of the last corridor
        GameObject lastPlane = children[children.Count - 1];
        if (lastPlane.transform.rotation.eulerAngles.y == 90)
        {
            crossroad.transform.position = new Vector3(lastPlane.transform.position.x + 6.5f, lastPlane.transform.position.y, lastPlane.transform.position.z);
        }
        else if (lastPlane.transform.rotation.eulerAngles.y == 180)
        {
            crossroad.transform.position = new Vector3(lastPlane.transform.position.x, lastPlane.transform.position.y, lastPlane.transform.position.z - 6.5f);
        }
        else if (lastPlane.transform.rotation.eulerAngles.y == 0)
        {
            crossroad.transform.position = new Vector3(lastPlane.transform.position.x, lastPlane.transform.position.y, lastPlane.transform.position.z + 6.5f);
        }
        else if (lastPlane.transform.rotation.eulerAngles.y == 270)
        {
            crossroad.transform.position = new Vector3(lastPlane.transform.position.x - 6.5f, lastPlane.transform.position.y, lastPlane.transform.position.z);
        }
        crossroad.transform.SetParent(m_Maze.transform);

        GameObject corridor1 = Instantiate(m_CorridorPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        GameObject corridor2 = Instantiate(m_CorridorPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        // Adding a crossroad adds two corridors.
        switch ((int)lastPlane.transform.rotation.eulerAngles.y)
        {
            case 0:
                corridor1.transform.position = new Vector3(crossroad.transform.position.x - 6.5f, crossroad.transform.position.y, crossroad.transform.position.z);
                corridor1.transform.rotation = Quaternion.Euler(0, 270, 0);
                corridor2.transform.position = new Vector3(crossroad.transform.position.x + 6.5f, crossroad.transform.position.y, crossroad.transform.position.z);
                corridor2.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;

            case 90:
                corridor1.transform.position = new Vector3(crossroad.transform.position.x, crossroad.transform.position.y, crossroad.transform.position.z - 6.5f);
                corridor1.transform.rotation = Quaternion.Euler(0, 180, 0);
                corridor2.transform.position = new Vector3(crossroad.transform.position.x, crossroad.transform.position.y, crossroad.transform.position.z + 6.5f);
                corridor2.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;

            case 180:
                corridor1.transform.position = new Vector3(crossroad.transform.position.x + 6.5f, crossroad.transform.position.y, crossroad.transform.position.z);
                corridor1.transform.rotation = Quaternion.Euler(0, 90, 0);
                corridor2.transform.position = new Vector3(crossroad.transform.position.x - 6.5f, crossroad.transform.position.y, crossroad.transform.position.z);
                corridor2.transform.rotation = Quaternion.Euler(0, 270, 0);
                break;

            case 270:
                corridor1.transform.position = new Vector3(crossroad.transform.position.x, crossroad.transform.position.y, crossroad.transform.position.z + 6.5f);
                corridor1.transform.rotation = Quaternion.Euler(0, 0, 0);
                corridor2.transform.position = new Vector3(crossroad.transform.position.x, crossroad.transform.position.y, crossroad.transform.position.z - 6.5f);
                corridor2.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
        }
        corridor1.transform.SetParent(m_Maze.transform);
        corridor2.transform.SetParent(m_Maze.transform);

    }

}
