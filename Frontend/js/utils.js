// Utilidades globales
export const formatDate = (dateString) => {
    if (!dateString) return 'Fecha no disponible';
    const date = new Date(dateString);
    return date.toLocaleDateString('es-ES', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        weekday: 'long'
    });
};

export const formatCurrency = (amount) => {
    return new Intl.NumberFormat('es-ES', {
        style: 'currency',
        currency: 'ARS'
    }).format(amount);
};

export const debounce = (func, wait) => {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
};

export const showLoading = (show = true) => {
    /* document.getElementById('loading').classList.toggle('hidden', !show); */
    document.getElementById('eventsContainer').classList.toggle('hidden', show);
};

export const showNoResults = (show = true) => {
    document.getElementById('noResults').style.display = show ? 'block' : 'none';
};