// 由Creat LSFCmd Editor生成
namespace ET.Server
{
    [LSFCmdHandler(typeof (LSFBuffCmd))]
    public class LSFBuffCmdHandler: LSFCmdHandler
    {
        public override void Receive(Unit unit, LSFCmd cmd)
        {
            var buffCmd = cmd as LSFBuffCmd;
        }
    }
}
