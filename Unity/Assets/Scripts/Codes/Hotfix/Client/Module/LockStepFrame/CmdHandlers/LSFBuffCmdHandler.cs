// 由Creat LSFCmd Editor生成
namespace ET.Client
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
