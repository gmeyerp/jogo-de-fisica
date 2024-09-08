using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerList
{
    public Item<GameObject> first;
    public Item<GameObject> last;
    public int size { get; private set; }

    public GameObject Spawn()
    {
        return first.enemy;
    }


}
 public class Item<GameObject>
{
    public GameObject enemy;
    public Item<GameObject> next;

    public Item(GameObject enemy)
    {
        this.enemy = enemy;
    }
}