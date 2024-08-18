using System.Threading.Tasks;
using Game.Event;
using Game.Manager;
using Game.Object.Aethos;
using UnityEngine;

namespace Game.Object.Action{
    public class ScanAction : BaseObjectAction{
        private GeminiRecyclingModule _scanModule;
        private bool _isScanning = false;
        private bool _isStartScanning = false;
        private string _scanResult = "";
        public ScanAction(AethosController controller, ActionEnum type, GeminiRecyclingModule scanModule) : base(controller, type){
            _scanModule = scanModule;
        }

        public override bool Enter()
        {
            _isStartScanning = false;
            _isScanning = false;
            GameEvent.OnScanObject += Scan;
            return base.Enter();
        }

        public override void Update(){
            if(_isStartScanning){
                if(!_isScanning){
                    _controller.Resolve<AethosStateComponent>().ChangeState(ActionEnum.Move);
                    GameEvent.OnPlayerMove?.Invoke(Camera.main.transform.position);
                    GameEvent.OnReachTarget += () => {
                        _controller.Resolve<AethosStateComponent>().ChangeState(ActionEnum.Talking);
                        GameEvent.OnTalkToPlayer?.Invoke(_scanResult);
                    };
                    return;
                }
            }
        }

        public override bool Exit(ActionEnum actionAfter)
        {
            return base.Exit(actionAfter);
        }

        public async void Scan(Trash trash, bool isLearning = false){
            _isStartScanning = true;
            _isScanning = true;
            string tmp = await _scanModule.StartScan(trash.trashImage, isLearning);
            _scanResult = tmp;
            Debug.Log(tmp);
            _isScanning = false;
            //Do something with the scan
            //_controller.TargetScanner
        }
    }
}