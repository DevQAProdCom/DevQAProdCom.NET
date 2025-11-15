var uiElement = uiElementArgument;

// Create a new mouseup event
var event = new MouseEvent('mousedown', {
    bubbles: true,
    cancelable: true,
    view: window
});

uiElement.dispatchEvent(event);