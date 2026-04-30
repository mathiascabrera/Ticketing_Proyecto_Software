import { formatDate, formatCurrency, showNoResults } from './utils.js';

export function createEventCard(event) {
    const card = document.createElement('article');
    card.className = 'event-card';
    
    const sectorsHtml = event.sectors.map(sector => `
        <div class="sector-item">
            <span class="sector-name">${sector.name}</span>
            <span class="sector-price">${formatCurrency(sector.price)}</span>
        </div>
    `).join('');

    card.innerHTML = `
        <div class="event-header">
            <h3 class="event-title">${event.name || 'Evento sin nombre'}</h3>
            <div class="event-meta">
                <div class="event-id">ID: ${event.id}</div>
                <div class="event-date">
                    📅 ${formatDate(event.eventDate)}
                </div>
            </div>
        </div>
        <div class="event-content">
            <div class="sectors-section">
                <h4 class="sectors-title">Sectores Disponibles</h4>
                <div class="sectors-grid">
                    ${sectorsHtml}
                </div>
            </div>
            <button type="button" class="btn btn-success button-buy-ticket">Buy ticket</button>
        </div>
    `;
    
    return card;
}

export function renderEvents(events, container) {
    container.innerHTML = '';
    
    if (events.length === 0) {
        showNoResults(true);
        return;
    }
    
    showNoResults(false);
    
    events.forEach(event => {
        const card = createEventCard(event);
        container.appendChild(card);
    });
}