using System;
using MyTools.Event;
using TMPro;
using UnityEngine;

namespace TimeCounter {
    public class TimeCounter : MonoBehaviour{
        [Header("Time Counter")]
        public float time;
        public DefaultEvent onTimeOut;

        [Header("Time Display")]
        public TMP_Text timeDisplay; 

        private void OnEnable(){
            timeDisplay.text = time.ToString();
        }

        private void Update(){
            time -= Time.deltaTime;
            timeDisplay.text = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
            if (time <= 0){
                onTimeOut?.Raise();
            }
        }
    }
}