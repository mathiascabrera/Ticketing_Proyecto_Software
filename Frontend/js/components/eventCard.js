export function createEventCard(event) {
    return `
        <div class="event-card" data-event-id="${event.id}">
            <img class="event-img" src="${event.url1}" alt="${event.name}">
            <div class="event-body">
                <div class="event-title">${event.name}</div>
                <div class="event-date">
                    📅 ${new Date(event.eventDate).toLocaleDateString()}
                </div>
            </div>
        </div>
    `;
}

export function renderEvents(events, containerId) {
    const container = document.getElementById(containerId);
    if (!container) {
        console.error(`Container with id "${containerId}" not found`);
        return;
    }

    container.innerHTML = "";
    events.forEach(event => {
        container.innerHTML += createEventCard(event);
    });

    // Agregar event listeners después de renderizar
    addEventListeners();
}

function addEventListeners() {
    const eventCards = document.querySelectorAll('.event-card');
    eventCards.forEach(card => {
        card.addEventListener('click', function() {
            const eventId = this.dataset.eventId;
            goToScenario(eventId);
        });
    });
}

function goToScenario(eventId) {
    window.location.href = `./scenario.html?eventId=${eventId}`;
}