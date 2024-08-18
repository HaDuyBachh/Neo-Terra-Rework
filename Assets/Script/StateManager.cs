// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class StateManager : MonoBehaviour
// {
//     private static StateManager instance;
//     public static StateManager Instance
//     {
//         get
//         {
//             if (instance == null)
//             {
//                 instance = FindObjectOfType<StateManager>();
//                 if (instance == null)
//                 {
//                     GameObject singletonObject = new GameObject();
//                     instance = singletonObject.AddComponent<StateManager>();
//                     singletonObject.name = typeof(StateManager).ToString() + " (Singleton)";
//                     DontDestroyOnLoad(singletonObject);
//                 }
//             }
//             return instance;
//         }
//     }
//
//     public enum CableState
//     {
//         Default,
//         CableCutted,
//         CableStripped,
//         CableUnited
//     }
//
//
//     // Add state variables and methods here
//     private CableState currentState;
//     private CableState[] cableStates = { CableState.CableCutted, CableState.CableStripped, CableState.CableUnited };
//
//     // Awake is called when the script instance is being loaded
//     private void Awake()
//     {
//         if (instance == null)
//         {
//             instance = this;
//             DontDestroyOnLoad(this.gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//         currentState = CableState.Default;
//     }
//     public void SetCurrentState(CableState newState)
//     {
//         currentState = newState;
//         Debug.Log("Current state: " + currentState);
//     }
//
//     public CableState GetCurrentState()
//     {
//         return currentState;
//     }
//
//     public CableState[] GetCableStates()
//     {
//         return cableStates;
//     }
// }
