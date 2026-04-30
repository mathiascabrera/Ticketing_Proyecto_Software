class EventsApi {
    async getAllEvents(apiUrl) {
        try {
            const res = await fetch(`${apiUrl}/api/v1/events`);
            const jres = await res.json();
            if (!res.ok) {
                throw new Error(`Error ${response.status}: ${response.statusText}`);
            }
            return jres;
        } catch (error) {
            console.error('Error consumiendooooo API de eventos:', error);
        }
    }
}
export default new EventsApi();