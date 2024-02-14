using BrokenWheel.Control.Constants;
using BrokenWheel.Control.Enum;

namespace BrokenWheel.Control.Extensions
{
    public static class RPGInputExtensions
    {
        public static bool IsUIInput(this RPGInput input)
        {
            return input >= RPGInputCategoryValue.UI_INPUT_START;
        }
    }
}
