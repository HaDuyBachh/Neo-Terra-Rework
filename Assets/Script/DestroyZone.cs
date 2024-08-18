using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    public Transform StandPlace;
    private void OnTriggerEnter(Collider other)
    {
        if ( other.transform.parent!=null && other.transform.parent.CompareTag("trash"))
        {
            StartCoroutine(DestroyAndSpawnNewtrash(other.transform.parent.gameObject));
        }   
        else
        if (transform.CompareTag("trash"))
        {
            StartCoroutine(DestroyAndSpawnNewtrash(other.transform.gameObject));
        }    
        else
        if (other.transform.CompareTag("player"))
        {
            other.transform.position = StandPlace.position;
        }    
    }

    private IEnumerator DestroyAndSpawnNewtrash(GameObject obj)
    {
        obj.tag = "Untagged";
        yield return new WaitForSeconds(0.5f);
        Destroy(obj);
        FindFirstObjectByType<Game.GameManager.TrashSpawnManager>().SpawnTrashLine();
    }    
}
