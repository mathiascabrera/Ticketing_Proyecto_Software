import { authService } from '../services/authService.js';
import { showNotification } from '../components/notifications.js';

document.addEventListener('DOMContentLoaded', function() {
    const form = document.querySelector('form');
    const emailInput = document.getElementById('InputEmailLoginClient');
    const passwordInput = document.getElementById('InputPasswordLoginClient');

    form.addEventListener('submit', async function(e) {
        e.preventDefault();
        
        // ✅ Datos EXACTOS como Swagger
        const loginData = {
            Email: emailInput.value.trim(),
            Password: passwordInput.value
        };

        console.log('📤 Enviando:', loginData);

        // Loading
        const btn = form.querySelector('button[type="submit"]');
        btn.disabled = true;
        btn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Iniciando...';

        // ✅ LLAMADA SIMPLE
        const result = await authService.login(loginData);
        console.log(result)

        btn.disabled = false;
        btn.innerHTML = 'Login';

        if (result.success) {
            console.log('✅ Login OK:', result.data.user);
            showNotification(`¡Bienvenido ${result.data.user.UserName}!`, 'success');
            
            // Redirigir
            setTimeout(() => {
                window.location.href = './events.html';
            }, 1500);
        } else {
            console.error('❌ Login falló:', result.error);
            showNotification(result.error, 'error');
            passwordInput.value = '';
        }
    });

    // Cancel
    document.querySelector('.btn-cancel').addEventListener('click', (e) => {
        e.preventDefault();
        window.location.href = './index.html';
    });
});