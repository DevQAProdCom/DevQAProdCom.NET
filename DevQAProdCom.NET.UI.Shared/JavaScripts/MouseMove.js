var x = xArgument;
var y = yArgument;

// Create a new mouseup event
var event = new MouseEvent('mousemove', {
    bubbles: true,
    cancelable: true,
    view: window,
    clientX: x,
    clientY: y
});

document.dispatchEvent(event);