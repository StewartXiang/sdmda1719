using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner {

    public GameObject SpawnPrefab;
    public int initCount = 10;
    private LinkedList<GameObject> SpawnList = new LinkedList<GameObject>();
    //private LinkedList<GameObject> UnSpawnList = new LinkedList<GameObject>();
    //private LinkedListNode<GameObject> currentSpawn = null;
    public Spawner()
    {

    }
    public Spawner(GameObject prefab):this(prefab,10)
    {
        
    }
    private GameObject realSpawn()
    {
        GameObject temp = MonoBehaviour.Instantiate<GameObject>(SpawnPrefab);
        temp.SetActive(false);
        SpawnList.AddLast(temp);
        return temp;
    }
    public Spawner(GameObject prefab,int count)
    {
        SpawnPrefab = prefab;
        initCount = count;
        for (int i = 0; i < initCount; i++)
        {
            realSpawn();
        }
    }

    public void Spawn()
    {
        Spawn(0, 0);
    }

    public GameObject Spawn(float x, float y)
    {
        if (!HasSpare())
        {
            realSpawn();
        }

        LinkedListNode<GameObject> temp = SpawnList.Last;
        SpawnList.RemoveLast();
        SpawnList.AddFirst(temp);
        temp.Value.SetActive(true);
        temp.Value.transform.position = new Vector3(x, y);
        return temp.Value;

        /*if (UnSpawnList.Count == 0)
        {
            realSpawn();
        }
        if (currentSpawn == null)
        {
            currentSpawn = SpawnList.Last;
        }
        else if (currentSpawn.Next == null)
        {
            if (SpawnList.First.Value.activeSelf == true)
            {
                realSpawn();
                currentSpawn = SpawnList.Last;
            }
        }
        */
    }

    public bool HasSpare()
    {
        return SpawnList.Last.Value.activeSelf == false;
    }
    public void Destroy(GameObject target)
    {
        target.SetActive(false);
        SpawnList.Remove(target);
        SpawnList.AddLast(target);
    }

    public GameObject DestroyOne()
    {
        GameObject temp = SpawnList.First.Value;
        if (temp.activeSelf != false)
        {
            temp.SetActive(false);
            SpawnList.RemoveFirst();
            SpawnList.AddLast(temp);
        }
        return temp;
    }
}
