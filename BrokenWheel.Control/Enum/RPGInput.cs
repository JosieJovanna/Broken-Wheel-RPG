﻿using BrokenWheel.Control.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BrokenWheel.Control.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RPGInput : int
    {
        Custom = default,

        #region Main Input
        Menu = RPGInputCategoryValue.MISC,
        Modifier,
        Action,
        Interact,

        Inventory = RPGInputCategoryValue.MENU,
        QuickSelect,
        Map,
        Calendar,
        Journal,
        Crafting,
        Rest,

        Look = RPGInputCategoryValue.MOVEMENT,
        MoveAnalog,
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        ToggleStance,
        ToggleSpeed,

        AttackPrimary = RPGInputCategoryValue.COMBAT,
        AttackSecondary,
        UseAbility,
        Reload,
        Throw,
        Kick,
        Bash,

        HotbarToggle = RPGInputCategoryValue.HOTBAR,
        HotbarNext,
        HotbarPrev,
        Hotbar1,
        Hotbar2,
        Hotbar3,
        Hotbar4,
        Hotbar5,
        Hotbar6,
        Hotbar7,
        Hotbar8,
        Hotbar9,
        Hotbar10,

        Debug1 = RPGInputCategoryValue.DEBUG,
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
        #endregion

        #region User Interface Input
        UIAccept = RPGInputCategoryValue.UI,
        UISelect,
        UICancel,
        UIFocusNext,
        UIFocusPrev,
        UILeft,
        UIRight,
        UIUp,
        UIDown,
        UIPageUp,
        UIPageDown,
        UIHome,
        UIEnd,
        UICut,
        UICopy,
        UIPaste,
        UIUndo,
        UIRedo,
        UITextCompletionQuery,
        UITextCompletionAccept,
        UITextCompletionReplace,
        UITextNewline,
        UITextNewlineBlank,
        UITextNewlineAbove,
        UITextIndent,
        UITextDedent,
        UITextBackspace,
        UITextBackspaceWord,
        UITextBackspaceAllToLeft,
        UITextDelete,
        UITextDeleteWord,
        UITextDeleteAllToRight,
        UITextCaretLeft,
        UITextCaretWordLeft,
        UITextCaretRight,
        UITextCaretWordRight,
        UITextCaretUp,
        UITextCaretDown,
        UITextCaretLineStart,
        UITextCaretLineEnd,
        UITextCaretPageUp,
        UITextCaretPageDown,
        UITextCaretDocumentStart,
        UITextCaretDocumentEnd,
        UITextCaretAddBelow,
        UITextCaretAddAbove,
        UITextScrollUp,
        UITextScrollDown,
        UITextSelectAll,
        UITextSelectWordUnderCaret,
        UITextAddSelectionForNextOccurrence,
        UITextClearCaretsAndSelection,
        UITextToggleInsertMode,
        UIMenu,
        UITextSubmit,
        UIGraphDuplicate,
        UIGraphDelete,
        UIFiledialogUpOneLevel,
        UIFiledialogRefresh,
        UIFiledialogShowHidden,
        UISwapInputDirection,
        #endregion
    }
}
