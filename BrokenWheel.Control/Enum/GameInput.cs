using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BrokenWheel.Control.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameInput
    {
        Menu = default,
        Modifier,
        Action,
        Interact,

        Debug1,
        Debug2,
        Debug3,
        Debug4,
        Debug5,
        Debug6,
        Debug7,
        Debug8,
        Debug9,
        Debug10,
        Debug11,
        Debug12,

        Inventory,
        QuickSelect,
        Map,
        Calendar,
        Journal,
        Crafting,
        Rest,

        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        ToggleStance,
        ToggleSpeed,

        AttackPrimary,
        AttackSecondary,
        UseAbility,
        Reload,
        Throw,
        Kick,
        Bash,
    }
}
