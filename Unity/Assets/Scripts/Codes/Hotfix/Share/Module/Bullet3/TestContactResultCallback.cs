using BulletSharp;

namespace ET
{
    [ContactResultCallback]
    public class TestContactResultCallback : ContactResultCallback
    {
        public override float AddSingleResult(ManifoldPoint cp, 
        CollisionObjectWrapper colObj0Wrap, int partId0, int index0, 
        CollisionObjectWrapper colObj1Wrap, int partId1, int index1)
        {
            return 0.0f;
        }
    }
}