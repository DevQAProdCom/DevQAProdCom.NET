function isElementInViewport(element) {
    const rect = element.getBoundingClientRect();
    const viewportWidth = window.innerWidth || document.documentElement.clientWidth;
    const viewportHeight = window.innerHeight || document.documentElement.clientHeight;

    return (
        rect.top >= 0 &&
            rect.left >= 0 &&
            rect.bottom <= viewportHeight &&
            rect.right <= viewportWidth
    );
}

const myElement = uiElementArgument;
const isVisible = isElementInViewport(myElement);
console.log('Is element visible in viewport:', isVisible);
return isVisible;