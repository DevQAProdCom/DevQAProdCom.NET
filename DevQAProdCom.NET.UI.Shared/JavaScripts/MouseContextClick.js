var uiElement = uiElementArgument;

const contextMenuEvent = new MouseEvent('contextmenu',
    {
        bubbles: true,
        cancelable: true,
        view: window,
        button: 2 // 0: left, 1: middle, 2: right
    });
uiElement.dispatchEvent(contextMenuEvent)