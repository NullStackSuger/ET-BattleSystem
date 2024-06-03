// 由Creat LSFCmd Editor生成
namespace ET.Client
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
