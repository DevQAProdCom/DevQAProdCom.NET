var uiElement = uiElementArgument;
var attributeName = attributeNameArgument;
var attributeValue = attributeValueArgument;
uiElement.setAttribute(attributeName, attributeValue)
uiElement.dispatchEvent(new Event('change'));