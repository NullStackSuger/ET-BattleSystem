using System;
using ProtoBuf.Meta;
using UnityEngine;

namespace ET.Client
{
    [FriendOf(typeof(OperaComponent))]
    [FriendOfAttribute(typeof(ET.Client.GameRoomComponent))]
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
        [FriendOf(typeof(GameRoomComponent))]
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
                    /*C2M_UseCast c2MUseCast = new();
                    c2MUseCast.CastConfigId = 0001;
                    self.ClientScene().GetComponent<SessionComponent>().Session.Call(c2MUseCast).Coroutine();*/

                    self.CreatCast(0001).Coroutine();
                }
            }
        }

        private static async ETTask CreatCast(this OperaComponent self, int castConfig)
        {
            GameRoomComponent room = Root.Instance.Scene.GetComponent<GameRoomComponent>();

            //UnitComponent unitComponent = self.ClientScene().GetComponent<CurrentScenesComponent>().Scene.GetComponent<UnitComponent>();

            Unit castUnit = room.MainPlayer.GetComponent<CastComponent>().Creat(castConfig);

            C2M_FrameCmdReq c2MFrameCmd = new();
            c2MFrameCmd.Cmd = new LSFCastCmd()
            {
                Frame = room.Frame,
                UnitId = castUnit.Id,
            };
            M2C_FrameCmdRes res = await self.ClientScene().GetComponent<SessionComponent>().Session.Call(c2MFrameCmd) as M2C_FrameCmdRes;
            room.TargetAhead = room.Frame - res.Frame;
        }
    }
}