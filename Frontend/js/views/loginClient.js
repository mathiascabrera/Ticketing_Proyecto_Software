import { authService } from '../services/authService.js';
import { showNotification } from '../components/notifications.js';

document.addEventListener('DOMContentLoaded', function() {
    const form = document.querySelector('form');
    const emailInput = document.getElementById('InputEmailLoginClient');
    const passwordInput = document.getElementById('InputPasswordLoginClient');

    form.addEventListener('submit', async function(e) {
        e.preventDefault();
        
        const loginData = {
            Email: emailInput.value.trim(),
            Password: passwordInput.value
        };

        console.log('📤 Sending:', loginData);

        const btn = form.querySelector('button[type="submit"]');
        btn.disabled = true;
        btn.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Getting started...';

        const result = await authService.login(loginData);
        console.log(result)

        btn.disabled = false;
        btn.innerHTML = 'Login';

        if (result.success) {
            console.log('✅ Login OK:', result.data.user);
            showNotification(`Welcome ${result.data.user.UserName}!`, 'success');
            setTimeout(() => {
                const token = result.data.token;
                const payload = JSON.parse(atob(token.split('.')[1]));
                const role = payload[
                    "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                ];
                localStorage.setItem("role", role);
                localStorage.setItem("token", token);
                if (role === "Admin") {
                    window.location.href = './homeAdmin.html';
                } else {
                    window.location.href = './events.html';
                }
            }, 1500);
        } else {
            console.error('❌ Login fail:', result.error);
            showNotification(result.error, 'error');
            passwordInput.value = '';
        }
    });

    document.querySelector('.btn-cancel').addEventListener('click', (e) => {
        e.preventDefault();
        window.location.href = './index.html';
    });
});