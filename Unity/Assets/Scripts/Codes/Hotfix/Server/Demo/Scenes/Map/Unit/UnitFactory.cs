using System;
using Unity.Mathematics;

namespace ET.Server
{
    public static class UnitFactory
    {
        public static Unit Create(Scene scene, long id, UnitType unitType)
        {
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            switch (unitType)
            {
                case UnitType.Player:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                    unit.AddComponent<MoveComponent>();
                    unit.Position = new float3(-10, 0, -10);
			
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
                    numericComponent.Set(NumericType.AOI, 15000); // 视野15米
                    
                    unitComponent.Add(unit);
                    // 加入aoi
                    unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
                    
                    // Test
                    unit.AddComponent<ActionComponent>();
                    unit.AddComponent<CastComponent>();
                    unit.AddComponent<BuffComponent>();
                    
                    return unit;
                }
                default:
                    throw new Exception($"not such unit type: {unitType}");
            }
        }

        public static Unit CreatCast(Scene scene, int castConfigId, float3 position, quaternion rotation)
        {
            // Error: unitComponent == null
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