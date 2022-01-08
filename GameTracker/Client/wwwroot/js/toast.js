function showToast(dotNetObjectRef, toastEl) {
    const bsToast = new bootstrap.Toast(toastEl);

    bsToast.show();

    const hideHandler = () => {
        dotNetObjectRef.invokeMethodAsync("HideToast");
        toastEl.removeEventListener('hidden.bs.toast', hideHandler);
    }

    toastEl.addEventListener('hidden.bs.toast', hideHandler);

    return hideHandler;
}