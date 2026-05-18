import { authService } from './authService.js';

class EventsService {

    constructor() {
        this.baseUrl = "https://localhost:7269/api/v1/events";
    }

    async getEvents() {

        try {

            const token = authService.getToken();

            const response = await fetch(this.baseUrl, {
                headers: {
                    "Authorization": `Bearer ${token}`
                }
            });

            if (!response.ok) {
                console.log("Error HTTP:", response.status);
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            return await response.json();

        } catch (error) {

            console.error("Error fetching events:", error);
            return [];
        }
    }
}

export const eventsService = new EventsService();