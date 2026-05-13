import { authService } from '../services/authService.js';
import { showNotification } from '../components/notifications.js';

document.addEventListener('DOMContentLoaded', function() {
    const form = document.querySelector('form');
    const nameInput = document.getElementById('InputNameRegister');
    const emailInput = document.getElementById('InputEmailRegister');
    const passwordInput = document.getElementById('InputPasswordRegister');
    const confirmPasswordInput = document.getElementById('ConfirmInputPasswordRegister');

    // Validación en tiempo real
    setupRealTimeValidation();

    form.addEventListener('submit', async function(e) {
        e.preventDefault();
        
        // Obtener y validar datos
        const userData = {
            userName: nameInput.value.trim(),
            email: emailInput.value.trim(),
            password: passwordInput.value
        };

        const validationResult = validateForm(userData, confirmPasswordInput.value);
        if (!validationResult.isValid) {
            showNotification(validationResult.message, 'error');
            return;
        }

        // Mostrar loading
        const submitBtn = form.querySelector('button[type="submit"]');
        const originalText = submitBtn.innerHTML;
        submitBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Registrando...';
        submitBtn.disabled = true;

        try {
            const result = await authService.register(userData);
            console.log(result)
            
            if (result.success) {
                showNotification(result.data.message || 'Registro exitoso', 'success');
                
                // Redirigir a login después de 2 segundos
                setTimeout(() => {
                    window.location.href = './loginClient.html';
                }, 2000);
            } else {
                const errorMessage = result.error?.message || 
                                   result.error?.[0]?.description || 
                                   'Error en el registro';
                showNotification(errorMessage, 'error');
            }
        } catch (error) {
            showNotification('Error de conexión. Inténtalo de nuevo.', 'error');
        } finally {
            // Restaurar botón
            submitBtn.innerHTML = originalText;
            submitBtn.disabled = false;
        }
    });

    // Cancelar - redirigir a login
    const cancelBtn = document.querySelector('.btn-cancel');
    cancelBtn.addEventListener('click', function(e) {
        e.preventDefault();
        window.location.href = './index.html';
    });
});

function validateForm(userData, confirmPassword) {
    if (!userData.userName || userData.userName.length < 3) {
        return { isValid: false, message: 'El nombre debe tener al menos 3 caracteres' };
    }

    if (!userData.email || !isValidEmail(userData.email)) {
        return { isValid: false, message: 'Email inválido' };
    }

    if (!userData.password || userData.password.length < 6) {
        return { isValid: false, message: 'La contraseña debe tener al menos 6 caracteres' };
    }

    if (userData.password !== confirmPassword) {
        return { isValid: false, message: 'Las contraseñas no coinciden' };
    }

    return { isValid: true };
}

function isValidEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

function setupRealTimeValidation() {
    const inputs = ['InputNameRegister', 'InputEmailRegister', 'InputPasswordRegister', 'ConfirmInputPasswordRegister'];
    
    inputs.forEach(inputId => {
        const input = document.getElementById(inputId);
        if (input) {
            input.addEventListener('blur', function() {
                validateField(input);
            });
            input.addEventListener('input', function() {
                removeFieldError(input);
            });
        }
    });
}

function validateField(input) {
    const value = input.value.trim();
    let isValid = true;
    let errorMessage = '';

    switch(input.id) {
        case 'InputNameRegister':
            if (value.length < 3) {
                isValid = false;
                errorMessage = 'Mínimo 3 caracteres';
            }
            break;
        case 'InputEmailRegister':
            if (!isValidEmail(value)) {
                isValid = false;
                errorMessage = 'Email inválido';
            }
            break;
        case 'InputPasswordRegister':
            if (value.length < 6) {
                isValid = false;
                errorMessage = 'Mínimo 6 caracteres';
            }
            break;
    }

    if (!isValid) {
        showFieldError(input, errorMessage);
    } else {
        removeFieldError(input);
    }
}

function showFieldError(input, message) {
    let errorDiv = input.parentElement.querySelector('.invalid-feedback');
    if (!errorDiv) {
        errorDiv = document.createElement('div');
        errorDiv.className = 'invalid-feedback d-block';
        input.classList.add('is-invalid');
        input.parentElement.appendChild(errorDiv);
    }
    errorDiv.textContent = message;
}

function removeFieldError(input) {
    input.classList.remove('is-invalid');
    const errorDiv = input.parentElement.querySelector('.invalid-feedback');
    if (errorDiv) {
        errorDiv.remove();
    }
}