import { eventsService } from '../services/eventsService.js';
import { renderEvents } from '../components/eventCard.js';

async function loadEvents() {
    console.log('Loading events...');
    const events = await eventsService.getEvents();
    console.log('Events loaded:', events);
    
    if (events.length > 0) {
        renderEvents(events, 'eventsContainer');
    } else {
        console.log('No events found');
    }
}

// Inicializar cuando el DOM esté listo
document.addEventListener('DOMContentLoaded', loadEvents);

// Carga inmediata si el DOM ya está listo
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', loadEvents);
} else {
    loadEvents();
}