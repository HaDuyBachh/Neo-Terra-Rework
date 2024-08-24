using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconUIControl : MonoBehaviour
{
    public bool isTouched = false;
    public Transform hand;
    public GameObject anchorObject;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") && anchorObject.GetComponent<AnchorControl>().usingObject == anchorObject)
        {
            hand = other.transform;
            isTouched = true;
            anchorObject.GetComponent<AnchorControl>().usingObject = this.gameObject;
            anchorObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") && anchorObject.GetComponent<AnchorControl>().usingObject == this.gameObject)
        {
            isTouched = false;
            anchorObject.GetComponent<AnchorControl>().usingObject = anchorObject;
            anchorObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void Update()
    {
        if (isTouched)
        {
            anchorObject.transform.position = 
                new Vector3(Mathf.Clamp(hand.transform.position.x, transform.position.x - 0.002f, transform.position.x + 0.002f), 
                                        hand.transform.position.y, hand.transform.position.z);

            var target = new Vector3(transform.position.x, anchorObject.transform.position.y, anchorObject.transform.position.z);

            transform.position = Vector3.Lerp(transform.position, target, 2 * Time.deltaTime);
        }
    }
}
