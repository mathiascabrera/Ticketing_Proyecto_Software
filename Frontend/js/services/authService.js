class AuthService {
    async register(userData) {
        try {
            const response = await fetch('https://localhost:7269/api/Auth/register', {
                method: 'POST',
                headers: {
                    'accept': '*/*',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(userData)
            });
            console.log(response)

            const data = await response.json();

            if (!response.ok) {
                // Manejar errores de Identity Framework
                const errorMessage = data?.[0]?.description || 
                                   data?.message || 
                                   `Error ${response.status}`;
                return {
                    success: false,
                    error: errorMessage
                };
            }

            return {
                success: true,
                data: data
            };
        } catch (error) {
            console.error('Error de red:', error);
            return {
                success: false,
                error: 'Error de conexión. Verifica que el servidor esté corriendo.'
            };
        }
    }
}

export const authService = new AuthService();