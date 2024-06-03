// 由Creat LSFCmd Editor生成
namespace ET.Server
{
    [LSFCmdHandler(typeof (LSFTestCmd))]
    public class LSFTestCmdHandler: LSFCmdHandler
    {
        public override void Receive(Unit unit, LSFCmd cmd)
        {
            var testCmd = cmd as LSFTestCmd;
        }
    }
}
