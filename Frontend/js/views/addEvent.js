const grid = document.getElementById("grid");
const json = document.getElementById("json");
const toast = document.getElementById("toast");
const cell = 15;
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

function showToast(msg) {
    toast.innerText = msg;
    toast.style.opacity = 1;
    setTimeout(() => toast.style.opacity = 0, 2000);
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
        showToast("El sector debe tener un nombre");
        return;
    }

    if (price === "" || isNaN(price) || +price <= 0) {
        showToast("El precio debe ser mayor a 0");
        return;
    }

    const exists = eventData.sectors.some(s =>
        s.name.toLowerCase() === name.toLowerCase()
    );

    if (exists) {
        showToast("Ya existe un sector con ese nombre");
        return;
    }

    if (rows > 60 || cols > 60) {
        showToast("Máximo permitido: 60x60");
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
    json.innerText = JSON.stringify(eventData, null, 2);
}

async function save() {
    const name = document.getElementById("eventName").value.trim();
    const date = document.getElementById("eventDate").value;
    const place = document.getElementById("eventPlace").value.trim();

    if (!name) {
        showToast("El nombre del evento es obligatorio");
        return;
    }

    if (!date) {
        showToast("La fecha del evento es obligatoria");
        return;
    }

    if (!place) {
        showToast("El lugar del evento es obligatorio");
        return;
    }

    const payload = {
        ...eventData,
        date: new Date(date).toISOString()
    };

    try {
        const response = await fetch("https://localhost:7269/api/v1", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(payload)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(errorText);
        }

        const result = await response.json().catch(() => null);

        showToast("Evento guardado correctamente 🎉");
        console.log("RESPUESTA API:", result);

    } catch (err) {
        console.error(err);
        showToast("Error al guardar el evento");
    }
}
document.getElementById("eventName").addEventListener("input", update);
document.getElementById("eventDate").addEventListener("input", update);
document.getElementById("eventPlace").addEventListener("input", update);
document.getElementById("eventDescription").addEventListener("input", update);
document.getElementById("eventState").addEventListener("input", update);
document.getElementById("url1").addEventListener("input", update);
document.getElementById("url2").addEventListener("input", update);