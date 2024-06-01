// 由Creat LSFCmd Editor生成
namespace ET.Server
{
    [LSFCmdHandler(typeof (LSFB3CollisionCmd))]
    public class LSFB3CollisionCmdHandler: LSFCmdHandler
    {
        public override void Receive(Unit unit, LSFCmd cmd)
        {
            var b3CollisionCmd = cmd as LSFB3CollisionCmd;
        }
    }
}
