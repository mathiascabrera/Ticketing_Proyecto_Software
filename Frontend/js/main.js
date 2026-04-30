import eventsApi from './api.js';
import { debounce, showLoading, showNoResults } from './utils.js';
import { renderEvents } from './components.js';

let allEvents = [];
let filteredEvents = [];

const API_BASE_URL = 'http://localhost:5026';





const toastElList = document.querySelectorAll('.toast')
const toastList = [...toastElList].map(toastEl => new bootstrap.Toast(toastEl, option))

const toastTrigger = document.getElementById('liveToastBtn')
const toastLiveExample = document.getElementById('liveToast')

if (toastTrigger) {
  const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample)
  toastTrigger.addEventListener('click', () => {
    toastBootstrap.show()
  })
}














document.addEventListener('DOMContentLoaded', async () => {
    
    await loadEvents();
    
    // Event listeners
    document.getElementById('refreshBtn').addEventListener('click', loadEvents);
    
    const searchInput = document.getElementById('searchInput');
    searchInput.addEventListener('input', debounce(handleSearch, 300));
});

async function loadEvents() {
    try {
        showLoading(true);
        allEvents = await eventsApi.getAllEvents(API_BASE_URL);
        filteredEvents = await [...allEvents];
        renderEvents(filteredEvents, document.getElementById('eventsContainer'));
    } catch (error) {
        console.error('Error cargando eventos:', error);
        alert('Error al cargar los eventos. Verifica la conexión y la URL de la API.');
        showNoResults(true);
    } finally {
        showLoading(false);
    }
}

function handleSearch() {
    const query = document.getElementById('searchInput').value.toLowerCase().trim();
    
    if (!query) {
        filteredEvents = [...allEvents];
    } else {
        filteredEvents = allEvents.filter(event => 
            event.name?.toLowerCase().includes(query) ||
            event.sectors.some(sector => 
                sector.name.toLowerCase().includes(query)
            )
        );
    }
    
    renderEvents(filteredEvents, document.getElementById('eventsContainer'));
}