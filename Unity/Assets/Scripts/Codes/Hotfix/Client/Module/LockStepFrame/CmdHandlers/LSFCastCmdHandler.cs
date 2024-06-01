// 由Creat LSFCmd Editor生成
namespace ET.Client
{
    [LSFCmdHandler(typeof (LSFCastCmd))]
    public class LSFCastCmdHandler: LSFCmdHandler
    {
        public override void Receive(Unit unit, LSFCmd cmd)
        {
            var castCmd = cmd as LSFCastCmd;
        }
    }
}
