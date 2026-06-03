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

document.addEventListener('DOMContentLoaded', loadEvents);

if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', loadEvents);
} else {
    loadEvents();
}

function getRoleFromToken() {
    const token = localStorage.getItem("token");

    if (!token) return null;

    const payload = JSON.parse(atob(token.split(".")[1]));

    return payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
}

document.addEventListener("DOMContentLoaded", () => {
   // alert(" events.js está ejecutándose");
    const role = getRoleFromToken();
    const container = document.getElementById("eventsContainer");

    console.log("ROLE:", role);

    if (role && role.toLowerCase() === "admin") {

        const btn = document.createElement("button");
        btn.className = "btn btn-primary mt-3";
        btn.innerText = "⬅ Volver al admin";

        btn.onclick = () => {
            window.location.href = "./homeAdmin.html";
        };

        container.appendChild(btn);
    }
});