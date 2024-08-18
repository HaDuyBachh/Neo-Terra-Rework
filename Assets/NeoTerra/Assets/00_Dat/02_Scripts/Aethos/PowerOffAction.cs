using Game.Manager;
using Game.Object.Aethos;
using UnityEngine;

namespace Game.Object.Action{
    public class PowerOffAction : BaseObjectAction{
        public PowerOffAction(AethosController controller) : base(controller, ActionEnum.PowerOff){
        }

        public override bool Enter()
        {
            _controller.Resolve<AethosDisplayComponent>().SetAllValueParam(false);
            return base.Enter();
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            if(actionAfter != ActionEnum.Idle && actionAfter != ActionEnum.SpecialTalking) return false;
            return base.Exit(actionAfter);
        }

        public override void OnPlayerInteract()
        {
            base.OnPlayerInteract();
            Debug.Log("PowerOffAction.OnPlayerInteract");

            var vfx = ParticleManager.Instance.GetVFX(ParticleManager.VFXType.Star);
            vfx.transform.position = _controller.transform.position + Vector3.up * 1f;
            vfx.transform.localScale = Vector3.one * 0.1f;
            _controller.Resolve<AethosActionLogicComponent>().TalkToPlayer("Kael ... système... gravement endommagé!", false);
        }
    }
}