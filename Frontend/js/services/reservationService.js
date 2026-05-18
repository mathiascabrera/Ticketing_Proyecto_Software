const API_BASE = "https://localhost:7269/api";

class ReservationService {
    constructor() {
        this.token = "";
    }

    setToken(token) {
        this.token = token;
    }

async getSeats(eventId) {

    try {
        const response = await fetch(`${API_BASE}/v1/${eventId}/seats`, {
            headers: {
                "Authorization": `Bearer ${this.token}`
            }
        });
        if (!response.ok) {
            throw new Error(`HTTP ${response.status}`);
        }
        return await response.json();
    } catch (error) {
        console.error("Error fetching seats:", error);
        return [];
    }
}

    async createReservation(seatIds) {
        try {
            const response = await fetch(`${API_BASE}/Reservations`, {
                method: "POST",
                headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${this.token}`
                },
                body: JSON.stringify({ seatsIds: seatIds })
            });

            if (!response.ok) {
                if (response.status === 409) {
                    throw new Error("CONFLICT");
                }
                throw new Error(`HTTP ${response.status}`);
            }

            return await response.json();
        } catch (error) {
            console.error("Error creating reservation:", error);
            throw error;
        }
    }

    async confirmReservation(reservationId) {
        try {
            const response = await fetch(`${API_BASE}/Reservations/confirm`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${this.token}`
                },
                body: JSON.stringify({ reservationId })
            });

            if (!response.ok) {
                throw new Error(`HTTP ${response.status}`);
            }

            return await response.json();
        } catch (error) {
            console.error("Error confirming reservation:", error);
            throw error;
        }
    }
}

export const reservationService = new ReservationService();