using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleIconControl : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> trashAndCanIcon;
    [SerializeField]
    private List<Sprite> textures;
    [SerializeField]
    private Trash.Type type;
    [SerializeField]
    private CapsuleControl capsule;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("trashAndCanIcon"))
        {
            trashAndCanIcon.Add(other.gameObject);
            type = trashAndCanIcon[0].GetComponent<TrashAndCanIconControl>().Type;
            transform.GetComponent<Image>().sprite = textures[(int)type];
            capsule.SetUpCapsuleType(type);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("trashAndCanIcon"))
        {
            trashAndCanIcon.Remove(other.gameObject);
            if (trashAndCanIcon.Count > 0)
                type = trashAndCanIcon[0].GetComponent<TrashAndCanIconControl>().Type;
            else
                type = 0;

            transform.GetComponent<Image>().sprite = textures[(int)type];
            capsule.SetUpCapsuleType(type);
        }
    }
}
