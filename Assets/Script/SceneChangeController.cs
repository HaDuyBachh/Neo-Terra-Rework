using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangeController : MonoBehaviour
{
    public void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }    
}
