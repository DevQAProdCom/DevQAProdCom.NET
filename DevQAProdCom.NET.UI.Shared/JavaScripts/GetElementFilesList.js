var element = uiElementArgument;
var files = Array.from(element.files || []).map(f => f.name);
return files;