using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChangeController : MonoBehaviour
{
    private IEnumerator IEChangeSceneAfter(int id, float time)
    {
        yield return new WaitForSeconds(time);
        ChangeScene(id);
    }    
    public void ChangeScene(int id)
    {
        SceneManager.LoadScene(id);
    }    
    public void ChangeSceneAfterMinus(int id)
    {
        StartCoroutine(IEChangeSceneAfter(id, 3.0f));
    }    
}
