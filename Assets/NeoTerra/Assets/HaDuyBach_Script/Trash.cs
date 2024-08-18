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
    public int _point;

    public void OnDestroy()
    {
        FindFirstObjectByType<Game.GameManager.TrashSpawnManager>().RemoveTrash();
    }
}
