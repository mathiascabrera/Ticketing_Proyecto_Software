async function loadEvents() {

    const response = await fetch("https://localhost:7269/api/v1/events");

    

    if (!response.ok) {
        console.log("Error HTTP:", response.status);
        return;
    }


    const events = await response.json();

    console.log(events)

    const container = document.getElementById("eventsContainer");

    container.innerHTML = "";

    events.forEach(event => {

        container.innerHTML += `
            <div class="event-card" onclick="goToScenario(${event.id})">
                
                <img class="event-img" src="${event.url1}">
                
                <div class="event-body">
                    
                    <div class="event-title">${event.name}</div>

                    <div class="event-date">
                        📅 ${new Date(event.eventDate).toLocaleDateString()}
                    </div>

                </div>

            </div>
        `;
    });
}

function goToScenario(eventId) {
    window.location.href = `./scenario.html?eventId=${eventId}`;
}

loadEvents();