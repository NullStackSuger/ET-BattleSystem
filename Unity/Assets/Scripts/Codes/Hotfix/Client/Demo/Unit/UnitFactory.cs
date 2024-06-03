using Unity.Mathematics;

namespace ET.Client
{
    public static class UnitFactory
    {
        public static Unit Create(Scene currentScene, UnitInfo unitInfo)
        {
	        UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
	        Unit unit = unitComponent.AddChildWithId<Unit, int>(unitInfo.UnitId, unitInfo.ConfigId);
	        unitComponent.Add(unit);
	        
	        unit.Position = unitInfo.Position;
	        unit.Forward = unitInfo.Forward;
	        
	        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();

			foreach (var kv in unitInfo.KV)
			{
				numericComponent.Set(kv.Key, kv.Value);
			}
	        
	        unit.AddComponent<MoveComponent>();
	        if (unitInfo.MoveInfo != null)
	        {
		        if (unitInfo.MoveInfo.Points.Count > 0)
				{
					unitInfo.MoveInfo.Points[0] = unit.Position;
					unit.MoveToAsync(unitInfo.MoveInfo.Points).Coroutine();
				}
	        }

	        unit.AddComponent<ObjectWait>();

	        unit.AddComponent<XunLuoPathComponent>();

	        unit.AddComponent<CastComponent>();
	        unit.AddComponent<BuffComponent>();
	        unit.AddComponent<ActionComponent>();
	        
	        EventSystem.Instance.Publish(unit.DomainScene(), new EventType.AfterUnitCreate() {Unit = unit});
	        
            return unit;
        }
        
        public static Unit CreatCast(Scene scene, int castConfigId, float3 position, quaternion rotation)
        {
	        UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
	        // 这里暂时先添1001, 以后再修改配置表
	        Unit unit = unitComponent.AddChild<Unit, int>(1001);
	        unit.Position = position;
	        unit.Rotation = rotation;
            
	        unit.AddComponent<MoveComponent>();
	        //unit.AddComponent<PathfindingComponent, string>(Root.Instance.Scene.Name);
	        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
	        numericComponent.Set(NumericType.Speed, 6f);
	        numericComponent.Set(NumericType.AOI, 15000);
	        unit.AddComponent<Cast, int>(castConfigId);
	        // Add AOI Entity
	        //unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
	        // Add RangeComponent(Type selected by selectedType)
            
	        return unit;
        }
    }
}
