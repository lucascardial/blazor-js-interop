let windowEventListeners = {};

export function AddWindowListener(instanceReference) {
    let eventListener = () => NotifyWindowEvent(instanceReference);
    
    window.addEventListener("resize", eventListener);
    windowEventListeners[instanceReference] = eventListener;

    NotifyWindowEvent(instanceReference);
}


export function RemoveWindowListener(instanceReference) {
    window.removeEventListener("resize", windowEventListeners[instanceReference]);
}

export function NotifyWindowEvent(instanceReference) {
    instanceReference.invokeMethodAsync("UpdateWindowState", window.innerWidth, window.innerHeight);
}
