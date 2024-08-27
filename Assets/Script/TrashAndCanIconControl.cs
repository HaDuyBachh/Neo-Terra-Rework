using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashAndCanIconControl : MonoBehaviour
{
    public Trash.Type Type { get { return type; } }
    [SerializeField]
    private Trash.Type type;
}
