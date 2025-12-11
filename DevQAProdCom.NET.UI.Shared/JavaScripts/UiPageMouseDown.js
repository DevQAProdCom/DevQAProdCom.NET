// Create a new mousedown event
var event = new MouseEvent('mousedown', {
    bubbles: true,
    cancelable: true,
    view: window
});

document.dispatchEvent(event);