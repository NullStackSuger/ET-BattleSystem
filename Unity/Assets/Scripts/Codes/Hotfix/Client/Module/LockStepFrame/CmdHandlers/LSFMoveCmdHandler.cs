// 由Creat LSFCmd Editor生成
namespace ET.Client
{
    [LSFCmdHandler(typeof (LSFMoveCmd))]
    public class LSFMoveCmdHandler: LSFCmdHandler
    {
        public override void Receive(Unit unit, LSFCmd cmd)
        {
            var moveCmd = cmd as LSFMoveCmd;
            Log.Warning($"Move Cmd Client");
            //MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            /*unit.Position = moveCmd.Position;
            unit.Rotation = moveCmd.Rotation;*/
        }
    }
}
