using BrokenWheel.Control.Enum;

namespace BrokenWheel.Control.Constants
{
    public static class RPGInputCategoryValue
    {
        public const RPGInput DEBUG_INPUT_START = RPGInput.Debug1;
        public const RPGInput DEBUG_INPUT_END = RPGInput.Debug12;
        public const RPGInput UI_INPUT_START = RPGInput.Cursor;

        public const int MISC = 1;
        public const int MENU = 100;
        public const int MOVEMENT = 200;
        public const int COMBAT = 300;
        public const int HOTBAR = 400;
        public const int DEBUG = 500;

        /// <summary>
        /// Any enum value equal to or above this is a UI action.
        /// </summary>
        public const int UI = 1000;
    }
}
