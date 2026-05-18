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
            console.error('Network error:', error);
            return {
                success: false,
                error: 'Connection error. Verify that the server is running.'
            };
        }
    }
    async login(loginData) {
        return fetch('https://localhost:7269/api/Auth/login', {
            method: 'POST',
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: JSON.stringify(loginData)
        })
            .then(response => {
                console.log('Status:', response.status);
                return response.text(); 
            })
            .then(text => {
                console.log('Raw response:', text);
                let data;
                try {
                    data = JSON.parse(text);
                } catch (e) {
                    throw new Error(text || 'Invalid answer');
                }

                if (data.token && data.user) {
                    localStorage.setItem('authToken', data.token);
                    localStorage.setItem('currentUser', JSON.stringify(data.user));
                    return { success: true, data };
                } else {
                    throw new Error(data?.message || 'Invalid answer');
                }
            })
            .catch(error => {
                console.error('Login error:', error);
                return {
                    success: false,
                    error: error.message.includes('Usuario') ? error.message : 'Login error'
                };
            });
    }

    getToken() { return localStorage.getItem('authToken'); }
    getCurrentUser() {
        try {
            return JSON.parse(localStorage.getItem('currentUser') || '{}');
        } catch {
            return null;
        }
    }
    logout() {
        localStorage.removeItem('authToken');
        localStorage.removeItem('currentUser');
    }
}

export const authService = new AuthService();
