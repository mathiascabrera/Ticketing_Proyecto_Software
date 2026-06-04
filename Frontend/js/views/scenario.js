import { reservationService } from '../Services/reservationService.js';
import { SeatMap } from '../Components/seatMap.js';
import { Toast } from '../Components/toast.js';
import { authService } from '../Services/authService.js';

class ScenarioView {
    constructor() {
        this.eventId = this.getEventId();
        this.seatMap = null;
        this.toast = new Toast("toast");
        this.reservationId = null;
        this.reservedMode = false;
        this.timerInterval = null;

        this.init();
    }

    getEventId() {
        const params = new URLSearchParams(window.location.search);
        return params.get('eventId');
    }

    async init() {
        const token = authService.getToken();

        reservationService.setToken(token);

        await this.loadSeats();
        this.setupEventListeners();
    }

    async loadSeats() {
        try {
            const sectors = await reservationService.getSeats(this.eventId);
            console.log('Seats loaded:', sectors);

            this.seatMap = new SeatMap("map", (seat) => this.handleSeatClick(seat));
            this.seatMap.render(sectors);
        } catch (error) {
            this.toast.show("Error loading seats");
            console.error(error);
        }
    }

    setupEventListeners() {
        document.getElementById("btnReserve").addEventListener("click", () => this.reserve());
        document.getElementById("btnConfirm").addEventListener("click", () => this.confirm());
        document.getElementById("btnCancel").addEventListener("click", () => this.cancel());
        document.getElementById("arrow").addEventListener("click", () => this.goTo())
    }

    async handleSeatClick(seat) {
        if (this.reservedMode) {
            this.toast.show("First, cancel or pay for your reservation");
            return;
        }

        if (seat.status === 1) {
            this.toast.show("This seat is already reserved");
            return;
        }

        if (seat.status === 2) {
            this.toast.show("This seat has already been purchased");
            return;
        }

        this.seatMap.selectSeat(seat.id);
    }

    async reserve() {
        const selectedSeats = this.seatMap.getSelectedSeats();
        if (selectedSeats.length === 0) {
            this.toast.show("Select seats");
            return;
        }

        try {
            const data = await reservationService.createReservation(selectedSeats);
            console.log(data);
            this.reservationId = data.reservationId;
            this.reservedMode = true;
            this.startReservationTimer(data.expiresAt);

            this.updateUIState();
            this.toast.show("Reserve created");
            await this.loadSeats();
        } catch (error) {
            if (error.message === "CONFLICT") {
                this.toast.show("⚠️ Another user booked earlier. Updating...");
                this.seatMap.clearSelection();
                await this.loadSeats();
            } else {
                this.toast.show("Error when booking");
            }
        }
    }

    async confirm() {
        try {
            await reservationService.confirmReservation(this.reservationId);

            this.resetState();
            await this.loadSeats();
            setTimeout(() => {
                this.toast.show("Confirmed purchase");
                this.goTo();
            }, 2000);

        } catch (error) {
            this.toast.show("Error confirming purchase");
        }
    }

    goTo() {
        if (localStorage.getItem('role') === "Admin") {
            window.location.href = './eventsAdmin.html';
        } else {
            window.location.href = './events.html';
        }
    }

    cancel() {

        clearInterval(this.timerInterval);

        document.getElementById("reservationTimer").textContent = "";

        this.resetState();
        this.loadSeats();
    }

    updateUIState() {
        const reserveBtn = document.getElementById("btnReserve");
        const confirmBtn = document.getElementById("btnConfirm");
        const cancelBtn = document.getElementById("btnCancel");

        reserveBtn.disabled = true;
        reserveBtn.style.opacity = "0.4";
        confirmBtn.disabled = false;
        cancelBtn.style.display = "inline-block";

        this.seatMap.toggleReservedMode(true);
    }

    resetState() {
        this.reservationId = null;
        this.reservedMode = false;
        this.seatMap.clearSelection();

        const reserveBtn = document.getElementById("btnReserve");
        const confirmBtn = document.getElementById("btnConfirm");
        const cancelBtn = document.getElementById("btnCancel");

        reserveBtn.disabled = false;
        reserveBtn.style.opacity = "1";
        confirmBtn.disabled = true;
        cancelBtn.style.display = "none";

        this.seatMap.toggleReservedMode(false);
    }

    startReservationTimer(expiresAt) {

        if (this.timerInterval) {
            clearInterval(this.timerInterval);
        }

        const timerElement = document.getElementById("reservationTimer");

        this.timerInterval = setInterval(() => {

            const now = new Date();
            const expiration = new Date(expiresAt);

            const diff = expiration - now;

            if (diff <= 0) {
                clearInterval(this.timerInterval);
                timerElement.textContent = "Reservation expired";
                setTimeout(() => timerElement.style.opacity = 0, 2000);

                this.resetState();
                this.loadSeats();
                this.seatMap.clearSelection();
                this.goTo();
                return;
            }

            const minutes = Math.floor(diff / 1000 / 60);
            const seconds = Math.floor((diff / 1000) % 60);

            timerElement.textContent =
                `Time remaining: ${minutes}:${seconds.toString().padStart(2, '0')}`;

        }, 1000);
    }


}
document.addEventListener('DOMContentLoaded', () => {
    new ScenarioView();
});