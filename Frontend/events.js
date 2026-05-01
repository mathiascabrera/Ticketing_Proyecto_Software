async function loadEvents() {

    const response = await fetch("https://localhost:7269/api/v1/events");

    if (!response.ok) {
        console.log("Error HTTP:", response.status);
        return;
    }

    const events = await response.json();

    const container = document.getElementById("eventsContainer");

    container.innerHTML = "";

    events.forEach(event => {

        const sectorsHtml = event.sectors.map(s => `
            <div>🎟 ${s.name} - $${s.price}</div>
        `).join("");

        container.innerHTML += `
            <div class="event-card" onclick="goToScenario(${event.id})">
                
                <img class="event-img" src="${event.imangenUrl}">
                
                <div class="event-body">
                    
                    <div class="event-title">${event.name}</div>

                    <div class="event-date">
                        📅 ${new Date(event.eventDate).toLocaleDateString()}
                    </div>

                    <div class="sectors">
                        ${sectorsHtml}
                    </div>

                </div>

            </div>
        `;
    });
}

function goToScenario(eventId) {
    window.location.href = `scenario.html?eventId=${eventId}`;
}

loadEvents();