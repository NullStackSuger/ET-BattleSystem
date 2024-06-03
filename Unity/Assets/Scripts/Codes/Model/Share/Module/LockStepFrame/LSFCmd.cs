using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFCmd
    {
        [ProtoMember(1)]
        public uint Frame;
        [ProtoMember(2)]
        public long UnitId;
    }
    
    // 流程
    // Client.LSFComponentHandler 收集数据
    // Client.LSFComponent 在帧末发送LSFCmd
    // Server 收到客户端消息
    // Server.LSFCmdHandler 调用Component处理消息 
    // Server.LSFComponentHandler 因为Component数值改动, 收集数据
    // Server.LSFComponent 在帧末发送LSFCmd
    // Client 收到服务端消息
    // Client.LSFComponent 判断消息(本地非本地, 预测成功预测失败等)
    // 非本地玩家 Client.LSFCmdHandler 调用Component处理消息 
    // 本地玩家且预测成功 进入下一次Tick
    // 本地玩家且预测失败 Client.LSFComponentHandler 调用RollBack, 并在下一帧预测多帧 直到追回客户端应有进度
}