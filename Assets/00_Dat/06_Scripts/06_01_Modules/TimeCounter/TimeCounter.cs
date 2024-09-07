using System;
using MyTools.Event;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace TimeCounter
{
    public class TimeCounter : MonoBehaviour
    {
        [Header("Time Counter")]
        public float time;
        public DefaultEvent onTimeOut;
        public UnityEvent simpleEvent;

        [Header("Time Display")]
        public TMP_Text timeDisplay;

        private float _defaulTime;

        void Awake(){
            _defaulTime = time;
        }

        private void OnEnable()
        {
            time = _defaulTime;
            timeDisplay.text = time.ToString();
        }

        private void Update()
        {
            time -= Time.deltaTime;
            timeDisplay.text = TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
            if (time <= 0)
            {
                onTimeOut?.Raise();
                simpleEvent?.Invoke();
            }
        }
    }
}