namespace ET.Server
{

    [LSFCmdHandler(typeof (LSFTestCmd))]
    public class LSFTestCmdHandler: LSFCmdHandler
    {
        public override void Receive(LSFCmd cmd)
        {
            var testCmd = cmd as LSFTestCmd;
            Log.Info(testCmd.Value);
        }
    }
}