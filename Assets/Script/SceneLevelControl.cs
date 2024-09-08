using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
public class SceneLevelControl : MonoBehaviour
{
    [SerializeField]
    private GameData generalData;
    public int sceneID;
    public List<GameObject> levels;

    public void Start()
    { 
        OffAllLevel();
    }

    public void OffAllLevel()
    {
        foreach (var level in levels)
        {
            level.GetComponent<ILevelControl>().UnUsing();
        }
    }    

    public void UsingLevelId(int id)
    {
        OffAllLevel();
        levels[id].GetComponent<ILevelControl>().Using();
    }    

    public void UsingLevel()
    {
        //UsingLevelId(FindAnyObjectByType<GeneralControl>().GeneralLevel);
        UsingLevelId(generalData.levelWillPlay);
    }    
}
