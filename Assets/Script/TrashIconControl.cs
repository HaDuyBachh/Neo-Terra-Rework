using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashIconControl : MonoBehaviour
{
    public Trash.Type Type { get { return _type; } }
    [SerializeField]
    private Trash.Type _type;
}
