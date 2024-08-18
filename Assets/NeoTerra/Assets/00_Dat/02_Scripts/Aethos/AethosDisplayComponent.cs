using Game.Object.Aethos;
using UnityEngine;

namespace Game.Object{
    public class ConstAnimKey{
        private const string IS_IDLE = "IsIdle";
        private const string IS_POWER_OFF = "IsPowerOff";
        private const string IS_WALKING = "IsWalking";
        private const string IS_TALKING = "IsTalking";
        private const string IS_PICKING_UP = "IsPickingUp";
        private const string IS_SCAN = "IsScanning";
        private const string IS_HOLDING = "IsHolding";
        private const string IS_HOLDING_WALK = "IsHoldingWalk";
        private const string IS_DROPPING_DOWN = "IsDroppingDown";

        private const string PICK_UP_ANIM = "PickUp";
        private const string HOLDING_ANIM = "Idle";
        private const string SCAN_ANIM = "Scanning";
        private const string TALKING_ANIM = "Talking";

        public static string GetKey(ActionEnum type){
            return type switch{
                ActionEnum.Idle => IS_IDLE,
                ActionEnum.PowerOff => IS_POWER_OFF,
                ActionEnum.Move => IS_WALKING,
                ActionEnum.Talking => IS_TALKING,
                ActionEnum.PickUp => IS_PICKING_UP,
                ActionEnum.Drop => IS_DROPPING_DOWN,
                ActionEnum.Holding => IS_HOLDING,
                ActionEnum.HoldingAndWalking => IS_HOLDING_WALK,
                ActionEnum.Scan => IS_SCAN,
                _ => throw new System.Exception("Invalid ActionEnum " + type.ToString()) 
            };
        }

        public static string GetStateName(ActionEnum type){
            return type switch{
                ActionEnum.PickUp => PICK_UP_ANIM,
                ActionEnum.Holding => HOLDING_ANIM,
                ActionEnum.Scan => SCAN_ANIM,
                ActionEnum.Talking => TALKING_ANIM,
                _ => throw new System.Exception("Invalid ActionEnum " + type.ToString()) 
            };
        }
    }

    public class AethosDisplayComponent : BaseObjectComponent{

        private Animator _animator;

        public AethosDisplayComponent(AethosController controller, Animator animator) : base(controller){
            _animator = animator;
        }

        public override void Init(){
        }

        public override void Update(){
        }

        public void SetValueParam(ActionEnum type, bool value){
            if (type == ActionEnum.None) return;
            _animator.SetBool(ConstAnimKey.GetKey(type), value);
        }

        public void SetAllValueParam(bool value){
            foreach (ActionEnum type in System.Enum.GetValues(typeof(ActionEnum))){
                if (type == ActionEnum.None || type == ActionEnum.SpecialTalking) continue;
                _animator.SetBool(ConstAnimKey.GetKey(type), value);
            }
        }

        public float GetLength(ActionEnum type){
            return _animator.GetCurrentAnimatorStateInfo(0).length;
        }

        public float GetLengthOfAnimation(ActionEnum type){
            foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips){
                if (clip.name == ConstAnimKey.GetStateName(type)){
                    if(clip.isLooping) return 4f;
                    return clip.length;
                }
            }
            throw new System.Exception("AnimationClip not found for " + type.ToString());
        }
    }
}