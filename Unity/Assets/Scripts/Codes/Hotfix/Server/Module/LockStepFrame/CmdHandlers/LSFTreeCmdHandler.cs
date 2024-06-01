// 由Creat LSFCmd Editor生成
namespace ET.Server
{
    [LSFCmdHandler(typeof (LSFTreeCmd))]
    public class LSFTreeCmdHandler: LSFCmdHandler
    {
        public override void Receive(Unit unit, LSFCmd cmd)
        {
            var treeCmd = cmd as LSFTreeCmd;
        }
    }
}
