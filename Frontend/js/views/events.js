import { eventsService } from '../services/eventsService.js';
import { renderEvents } from '../components/eventCard.js';

async function loadEvents(pageNumber=1) {
    console.log('Loading events...');
    const eventsResponse = await eventsService.getEvents(pageNumber);
    const events = eventsResponse.items;
    const totalPages = eventsResponse.totalPages;
    const page = eventsResponse.page;
    console.log('ACA:', page);
    
    if (events.length > 0) {
        renderEvents(events, 'eventsContainer');
        if(totalPages > 1){
            renderPagination(totalPages, page);
        }
    } else {
        console.log('No events found');
    }
}

function renderPagination(totalPages, currentPage) {
    const pagination = document.getElementById("pagination");
    pagination.innerHTML = "";
    const title = document.createElement("h1");
    title.textContent = "View other events:";
    pagination.appendChild(title);

    const div = document.createElement("div");

    for (let i = 1; i <= totalPages; i++) {
        const btn = document.createElement("button");
        btn.textContent = i;
        btn.classList.add("page-number");

        if (i === currentPage) {
            btn.classList.add("current-page");
        }

        btn.addEventListener("click", () => {
            loadEvents(i);
        });

        div.appendChild(btn);
    }
    pagination.appendChild(div);
}

document.addEventListener('DOMContentLoaded', loadEvents());

if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', loadEvents());
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