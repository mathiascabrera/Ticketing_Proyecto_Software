const grid = document.getElementById("grid");
const dimensions = grid.getBoundingClientRect();
const cell = 15;
const rowElement = document.getElementById("rowtext");
const colElement = document.getElementById("columntext");
rowElement.textContent=`Rows (max ${Math.floor(dimensions.height / cell)}).`;
colElement.textContent=`Columns (max ${Math.floor(dimensions.width / cell)}).`;

const toast = document.getElementById("toast");
const eventData = {
    name: "",
    date: "",
    place: "",
    description: "",
    state: "",
    url1: "",
    url2: "",
    sectors: []
};
const colors = ["#2563eb", "#16a34a", "#dc2626", "#9333ea", "#ea580c"];

flatpickr("#eventDate", {
    enableTime: true,
    dateFormat: "Y-m-d H:i",
    time_24hr: true,
    disableMobile: true
});

function goBack(){
    window.location.href = './homeAdmin.html';
}

function showToast(msg) {
    toast.innerText = msg;
    toast.style.opacity = 1;
    toast.style.background= "#9a2525cc";
    setTimeout(() => toast.style.opacity = 0, 2000);
}

function showToastGreen(msg) {
    toast.innerText = msg;
    toast.style.opacity = 1;
    toast.style.background= '#38bf0bcc';
    setTimeout(() => {
        toast.style.opacity = 0;
        goBack();
    }, 2000);
}

function isColliding(a, b) {
    return !(
        a.gridX + a.cols <= b.gridX ||
        a.gridX >= b.gridX + b.cols ||
        a.gridY + a.rows <= b.gridY ||
        a.gridY >= b.gridY + b.rows
    );
}

function addSector() {
    const name = document.getElementById("sectorName").value.trim();
    const rows = +document.getElementById("rows").value;
    const cols = +document.getElementById("cols").value;
    const price = document.getElementById("price").value;

    if (!name) {
        showToast("Attention: The sector must have a name.");
        return;
    }

    if (price === "" || isNaN(price) || +price <= 0) {
        showToast("Attention: The price must be greater than 0.");
        return;
    }

    const exists = eventData.sectors.some(s =>
        s.name.toLowerCase() === name.toLowerCase()
    );

    if (exists) {
        showToast("Attention: There is already a sector with that name.");
        return;
    }

    if (cols*cell > dimensions.width || rows*cell > dimensions.height) {
        showToast(`Allowed rows: ${Math.floor(dimensions.height / cell)}. Allowed columns: ${Math.floor(dimensions.width / cell)}.`);
        return;
    }

    const sector = {
        id: Date.now(),
        name,
        rows,
        cols,
        price: +price,
        gridX: 5,
        gridY: 5
    };

    eventData.sectors.push(sector);
    renderSector(sector);
    update();

    document.getElementById("sectorName").value = "";
    document.getElementById("rows").value = 5;
    document.getElementById("cols").value = 10;
    document.getElementById("price").value = "";
}

function renderSector(sector) {

    const div = document.createElement("div");
    div.className = "sector";

    div.style.width = sector.cols * cell + "px";
    div.style.height = sector.rows * cell + "px";

    div.style.left = sector.gridX * cell + "px";
    div.style.top = sector.gridY * cell + "px";

    div.style.background = colors[Math.random() * colors.length | 0];

    const handle = document.createElement("div");
    handle.className = "sector-handle";
    div.appendChild(handle);

    const label = document.createElement("div");
    label.className = "sector-label";
    label.innerText = `${sector.name} $${sector.price}`;
    div.appendChild(label);

    for (let i = 0; i < sector.rows; i++) {
        for (let j = 0; j < sector.cols; j++) {
            const seat = document.createElement("div");
            seat.className = "seat";
            seat.onclick = (e) => {
                e.stopPropagation();
                seat.classList.toggle("selected");
            };
            div.appendChild(seat);
        }
    }

    div.addEventListener("contextmenu", (e) => {
        e.preventDefault();
        eventData.sectors = eventData.sectors.filter(s => s.id !== sector.id);
        div.remove();
        update();
    });
    enableDrag(div, handle, sector);
    grid.appendChild(div);
}

function enableDrag(el, handle, sector) {
    let dragging = false;
    let startX = 0, startY = 0;
    let originX = 0, originY = 0;

    handle.addEventListener("pointerdown", (e) => {
        dragging = true;
        startX = e.clientX;
        startY = e.clientY;
        originX = sector.gridX;
        originY = sector.gridY;
        handle.setPointerCapture(e.pointerId);
    });

    handle.addEventListener("pointermove", (e) => {
        if (!dragging) return;

        const dx = e.clientX - startX;
        const dy = e.clientY - startY;

        let newX = originX + Math.round(dx / cell);
        let newY = originY + Math.round(dy / cell);

        newX = Math.max(0, Math.min(60 - sector.cols, newX));
        newY = Math.max(0, Math.min(60 - sector.rows, newY));

        el.style.left = newX * cell + "px";
        el.style.top = newY * cell + "px";
    });

    handle.addEventListener("pointerup", () => {
        dragging = false;

        const proposed = {
            ...sector,
            gridX: Math.round(parseInt(el.style.left) / cell),
            gridY: Math.round(parseInt(el.style.top) / cell)
        };

        let collision = false;

        for (const s of eventData.sectors) {
            if (s.id === sector.id) continue;
            if (isColliding(proposed, s)) {
                collision = true;
                break;
            }
        }

        if (collision) {
            el.style.left = sector.gridX * cell + "px";
            el.style.top = sector.gridY * cell + "px";
        } else {
            sector.gridX = proposed.gridX;
            sector.gridY = proposed.gridY;
        }
        update();
    });
}

function update() {
    eventData.name = document.getElementById("eventName").value;
    eventData.date = document.getElementById("eventDate").value;
    eventData.place = document.getElementById("eventPlace").value;
    eventData.description = document.getElementById("eventDescription").value;
    eventData.state = document.getElementById("eventState").value;
    eventData.url1 = document.getElementById("url1").value;
    eventData.url2 = document.getElementById("url2").value;
}

async function save() {
    const name = document.getElementById("eventName").value.trim();
    const date = document.getElementById("eventDate").value;
    const place = document.getElementById("eventPlace").value.trim();

    if (!name) {
        showToast("Attention: The event name is required.");
        return;
    }

    if (!date) {
        showToast("Attention: The event date is required.");
        return;
    }

    if (!place) {
        showToast("Attention: The event venue is mandatory.");
        return;
    }

    const payload = {
        ...eventData,
        date: new Date(date).toISOString()
    };

    try {

    const token = localStorage.getItem("authToken");

    const response = await fetch("https://localhost:7269/api/v1/", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify(payload)
    });

    if (!response.ok) {
        const errorText = await response.text();
        throw new Error(errorText);
    }

    showToastGreen("Event saved successfully 🎉");

    } catch (err) {
        console.error(err);
        showToast("Error saving event.");
    }

}
document.getElementById("eventName").addEventListener("input", update);
document.getElementById("eventDate").addEventListener("input", update);
document.getElementById("eventPlace").addEventListener("input", update);
document.getElementById("eventDescription").addEventListener("input", update);
document.getElementById("eventState").addEventListener("input", update);
document.getElementById("url1").addEventListener("input", update);
document.getElementById("url2").addEventListener("input", update);