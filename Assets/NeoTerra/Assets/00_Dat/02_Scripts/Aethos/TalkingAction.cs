using Game.Event;
using Game.Object.Aethos;
using TMPro;
using UnityEngine;

namespace Game.Object.Action
{
    public class TalkingAction : BaseObjectAction
    {
        private float _time = 0;
        protected TTSModule _tTSModule;
        protected TMP_Text _chatText;

        protected bool _isStartTalking = false;

        public TalkingAction(AethosController controller, ActionEnum type, TTSModule tTSModule, TMP_Text chatText) : base(controller, type)
        {
            _tTSModule = tTSModule;
            _chatText = chatText;
        }

        public override bool Enter()
        {
            Debug.Log("TalkingAction.Enter");
            GameEvent.OnTalkToPlayer += Talk;
            _isStartTalking = false;
            _chatText.transform.parent.gameObject.SetActive(true);
            //_time = _controller.Resolve<AethosDisplayComponent>().GetLengthOfAnimation(_actionType);
            return base.Enter();
        }

        public override void Update()
        {
            if(_isStartTalking){
                if(_tTSModule.IsComplete){
                    _controller.Resolve<AethosStateComponent>().ChangeState(ActionEnum.Idle);
                    return;
                }
            }
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            Debug.Log("TalkingAction.Exit");
            GameEvent.OnTalkToPlayer = null;
            _chatText.transform.parent.gameObject.SetActive(false);
            return base.Exit(actionAfter);
        }

        public void Talk(string text)
        {
            _chatText.text = text;
            _isStartTalking = true;
            _tTSModule.SpeakText(text);
        }
    }
}