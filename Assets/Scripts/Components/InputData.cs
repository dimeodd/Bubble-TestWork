using UnityEngine;

namespace EcsStructs
{
    public struct InputData
    {
        public SubInputData curr;
        public SubInputData temp;
    }

    public struct SubInputData
    {
        public Vector2 pos;

        public bool IsPressed;
        public bool IsInsideFireZone;
    }
}