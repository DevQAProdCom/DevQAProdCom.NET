var items = {}, ls = window.localStorage;
for (var i = 0; i < ls.length; i++) {
    var key = ls.key(i);
    items[key] = ls.getItem(key);
}
return items;