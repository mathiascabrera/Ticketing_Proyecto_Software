console.log("🚀 APP INICIADA");

// ------------------ ELEMENTOS ------------------
const container = document.getElementById("seats-container");
const reserveBtn = document.getElementById("reserveBtn");
const payBtn = document.getElementById("payBtn");

const eventId = 1;

// ------------------ ESTADO ------------------
let seats = [];
let selectedSeat = null;
let currentReservationId = null;
let lockSelection = false; // 🔥 BLOQUEO GLOBAL

// ------------------ INIT ------------------
init();

async function init() {
  payBtn.disabled = true;
  await loadSeats();
}

// ------------------ LOAD SEATS ------------------
async function loadSeats() {
  try {
    const res = await fetch(`https://localhost:7269/api/v1/${eventId}/seats`);
    const data = await res.json();

    data.sort((a, b) => {
      if (a.rowIdentifier === b.rowIdentifier) {
        return a.seatNumber - b.seatNumber;
      }
      return a.rowIdentifier.localeCompare(b.rowIdentifier);
    });

    seats = data;
    renderSeats();

  } catch (err) {
    console.error("❌ Error cargando:", err);
  }
}

// ------------------ STATUS ------------------
function getStatusClass(status) {
  switch (status) {
    case 0: return "available";
    case 1: return "reserved";
    case 2: return "sold";
    default: return "available";
  }
}

// ------------------ RENDER ------------------
function renderSeats() {
  container.innerHTML = "";

  renderSector("VIP", seats.filter(s => s.sector === 1));
  renderSector("GENERAL", seats.filter(s => s.sector === 2));
}

// ------------------ SECTOR ------------------
function renderSector(titleText, data) {

  const title = document.createElement("h3");
  title.innerText = "🎟 " + titleText;
  container.appendChild(title);

  let currentRow = null;
  let rowDiv = null;

  data.forEach(s => {

    if (s.rowIdentifier !== currentRow) {
      currentRow = s.rowIdentifier;

      rowDiv = document.createElement("div");
      rowDiv.className = "seat-row";

      container.appendChild(rowDiv);
    }

    const seat = document.createElement("div");
    seat.className = "seat " + getStatusClass(s.status);
    seat.innerText = `${s.rowIdentifier}${s.seatNumber}`;

    // 🔥 BLOQUEO TOTAL SI YA HAY RESERVA ACTIVA
    seat.addEventListener("click", () => {

      if (lockSelection) {
        showToast("⚠️ Ya tenés una reserva activa realiza la compra primero", "warning");
        return;
      }

      if (s.status === 1 || s.status === 2) {
        showToast("❌ Asiento no disponible", "error");
        return;
      }

      if (selectedSeat) {
        selectedSeat.element.classList.remove("selected");
      }

      selectedSeat = {
        id: s.id,
        element: seat
      };

      seat.classList.add("selected");
    });

    rowDiv.appendChild(seat);
  });
}

// ------------------ RESERVAR ------------------
async function reservar() {

  if (!selectedSeat) {
    showToast("⚠️ Seleccione un asiento", "error");
    return;
  }

  try {
    showToast("⏳ Procesando reserva...", "info");

    const res = await fetch(
      `https://localhost:7269/api/Reservations?seatId=${selectedSeat.id}`,
      { method: "POST" }
    );

    if (!res.ok) throw new Error();

    const data = await res.json();

    currentReservationId = data.reservationId;

    // 🔥 BLOQUEO DESPUÉS DE RESERVAR
    lockSelection = true;

    payBtn.disabled = false;

    showToast("✅ Reserva creada", "success");

    selectedSeat = null;
    loadSeats();

  } catch (error) {
    console.error(error);
    showToast("❌ Error en la reserva", "error");
  }
}

// ------------------ PAGAR ------------------
payBtn.addEventListener("click", async () => {

  if (!currentReservationId) return;

  try {
    showToast("💳 Confirmando pago...", "info");

    const res = await fetch(
      "https://localhost:7269/api/Reservations/confirm",
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          reservationId: currentReservationId,
          userId: 1
        })
      }
    );

    const data = await res.json();

    if (!res.ok || data.status !== "Paid") {
      showToast("❌ Pago no confirmado", "error");
      return;
    }

    showToast("💳 Compra exitosa", "success");

    // 🔥 RESET COMPLETO
    currentReservationId = null;
    lockSelection = false;
    payBtn.disabled = true;

    loadSeats();

  } catch (error) {
    console.error(error);
    showToast("❌ Error en el pago", "error");
  }
});

// ------------------ EVENTOS ------------------
reserveBtn.addEventListener("click", reservar);

// ------------------ TOAST ------------------
function showToast(message, type = "info") {

  let toast = document.getElementById("toast");

  if (!toast) {
    toast = document.createElement("div");
    toast.id = "toast";
    document.body.appendChild(toast);
  }

  toast.innerText = message;

  toast.style.position = "fixed";
  toast.style.top = "25px";
  toast.style.left = "50%";
  toast.style.transform = "translateX(-50%)";
  toast.style.padding = "18px 28px";
  toast.style.borderRadius = "8px";
  toast.style.color = "white";
  toast.style.zIndex = "9999";
  toast.style.fontSize = "18px"; 

  if (type === "success") toast.style.background = "green";
  else if (type === "error") toast.style.background = "red";
  else toast.style.background = "black";

  toast.style.opacity = "1";

  setTimeout(() => {
    toast.style.opacity = "0";
  }, 1500);
}