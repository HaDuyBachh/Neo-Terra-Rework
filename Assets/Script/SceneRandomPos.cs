using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRandomPos : MonoBehaviour
{
    public Transform Car;
    public List<Transform> randomPos;
    private bool Change = false;
    private int beforeK = -1;
    private int K;

    // Update is called once per frame
    void Update()
    {
        if (!Change || Input.GetKeyDown(KeyCode.T))
        {
            Car.gameObject.SetActive(false);
            do
            {
                K = Random.Range(0, randomPos.Count);
            }
            while (K == beforeK);
            beforeK = K;

            Debug.LogError("Đã chuyển hướng  " + K);
            var randTras = randomPos[K];
            Car.position = randTras.position;
            Change = true;
            Car.gameObject.SetActive(true);
        }
    }
}
