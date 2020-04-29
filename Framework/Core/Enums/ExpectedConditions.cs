using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Enums
{
    public enum ExpectedConditionsEnum
    {
        AlertIsPresent,
        AlertState,
        ElementExists,
        ElementIsVisible,
        ElementSelectionStateToBe,
        ElementToBeClickable,
        ElementToBeSelected,
        FrameToBeAvailableAndSwitchToIt,
        InvisibilityOfElementLocated,
        InvisibilityOfElementWithText,
        PresenceOfAllElementsLocatedBy,
        StalenessOf,
        TextToBePresentInElement,
        TextToBePresentInElementLocated,
        TextToBePresentInElementValue,
        TitleContains,
        TitleIs,
        UrlContains,
        UrlMatches,
        UrlToBe,
        VisibilityOfAllElementsLocatedBy
    }
}
