using System;
using ProtoBuf.Meta;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(OperaComponent))]
    public static class OperaComponentSystem
    {
        [ObjectSystem]
        public class OperaComponentAwakeSystem : AwakeSystem<OperaComponent>
        {
            protected override void Awake(OperaComponent self)
            {
                self.mapMask = LayerMask.GetMask("Map");
            }
        }

        [ObjectSystem]
        public class OperaComponentUpdateSystem : UpdateSystem<OperaComponent>
        {
            protected override void Update(OperaComponent self)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
                    {
                        C2M_PathfindingResult c2MPathfindingResult = new C2M_PathfindingResult();
                        c2MPathfindingResult.Position = hit.point;
                        self.ClientScene().GetComponent<SessionComponent>().Session.Send(c2MPathfindingResult);
                    }
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    CodeLoader.Instance.LoadHotfix();
                    EventSystem.Instance.Load();
                    Log.Debug("hot reload success!");
                }
            
                if (Input.GetKeyDown(KeyCode.T))
                {
                    C2M_TransferMap c2MTransferMap = new C2M_TransferMap();
                    self.ClientScene().GetComponent<SessionComponent>().Session.Call(c2MTransferMap).Coroutine();
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    C2M_NormalAtk c2MNormalAtk = new();
                    c2MNormalAtk.CastConfigId = 0001;
                    self.ClientScene().GetComponent<SessionComponent>().Session.Call(c2MNormalAtk).Coroutine();
                    C2M_FrameCmd c2MFrameCmd = new();
                    c2MFrameCmd.Cmd = new LSFTestCmd() { Value = "C2S" };
                    RuntimeTypeModel.Default.Add(typeof (LSFCmd), true);
                    RuntimeTypeModel.Default.Add(typeof (LSFTestCmd), true);
                    RuntimeTypeModel.Default[typeof (LSFCmd)].AddSubType(100, typeof(LSFTestCmd));
                    self.ClientScene().GetComponent<SessionComponent>().Session.Send(c2MFrameCmd);
                    Log.Warning("End Send");
                }
            }
        }
    }
}