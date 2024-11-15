using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public enum Type
    {
        none,
        plastic,
        paper,
        metal,
        glass,
        organic,
        special
    }

    [SerializeField] public Texture2D trashImage;
    [SerializeField]
    public Type _type;
    public int _point = 0;
    public bool canThrow = true;

    public void OnDestroy()
    {
        if (canThrow)
        {
            canThrow = false;
            FindFirstObjectByType<Game.GameManager.TrashSpawnManager>().RemoveTrashCanThrowCount();
        }
    }
    public void OnDisable()
    {
        if (canThrow)
        {
            canThrow = false;
            FindFirstObjectByType<Game.GameManager.TrashSpawnManager>().RemoveTrashCanThrowCount();
        };
    }
}
