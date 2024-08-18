using Game.Event;
using Game.Object.Aethos;
using TMPro;
using UnityEngine;

namespace Game.Object.Action{
    public class SpecialTalkingAction : TalkingAction{
        public SpecialTalkingAction(AethosController controller, ActionEnum type, TTSModule tTSModule, TMP_Text chatText) : base(controller, type, tTSModule, chatText){
        }

        public override bool Enter()
        {
            _chatText.transform.parent.gameObject.SetActive(true);
            GameEvent.OnTalkToPlayer += Talk;
            return true;
        }

        public override void Update(){
            if(_tTSModule.IsComplete){
                _controller.Resolve<AethosStateComponent>().ChangeState(_controller.Resolve<AethosStateComponent>().BeforeAcion);
                return;
            }
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            _chatText.transform.parent.gameObject.SetActive(false);
            Debug.Log("SpecialTalkingAction.Exit");
            GameEvent.OnTalkToPlayer = null;
            return true;
        }
    }
}