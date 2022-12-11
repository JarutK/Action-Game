using UnityEngine;

namespace Service.SystemBase
{
    public readonly struct SystemInput
    {
        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";

        public static float HorizontalAxis
        {
            get => Input.GetAxis(HORIZONTAL_AXIS);
        }

        public static float HorizontalAxisRaw
        {
            get => Input.GetAxisRaw(HORIZONTAL_AXIS);
        }

        public static float VerticalAxis
        {
            get => Input.GetAxis(VERTICAL_AXIS);
        }

        public static float VerticalAxisRaw
        {
            get => Input.GetAxisRaw(VERTICAL_AXIS);
        }
    }
}