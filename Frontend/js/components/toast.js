export class Toast {
    constructor(toastId) {
        this.toastElement = document.getElementById(toastId);
    }

    show(message) {
        if (!this.toastElement) return;
        
        this.toastElement.innerText = message;
        this.toastElement.classList.add("show");
        
        setTimeout(() => {
            this.toastElement.classList.remove("show");
        }, 2500);
    }
}