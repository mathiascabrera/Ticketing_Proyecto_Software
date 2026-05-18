import { authService } from '../services/authService.js';
import { showNotification } from '../components/notifications.js';

document.addEventListener('DOMContentLoaded', function() {
    const form = document.querySelector('form');
    const nameInput = document.getElementById('InputNameRegister');
    const emailInput = document.getElementById('InputEmailRegister');
    const passwordInput = document.getElementById('InputPasswordRegister');
    const confirmPasswordInput = document.getElementById('ConfirmInputPasswordRegister');

    setupRealTimeValidation();

    form.addEventListener('submit', async function(e) {
        e.preventDefault();
        
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

        const submitBtn = form.querySelector('button[type="submit"]');
        const originalText = submitBtn.innerHTML;
        submitBtn.innerHTML = '<span class="spinner-border spinner-border-sm me-2"></span>Registering...';
        submitBtn.disabled = true;

        try {
            const result = await authService.register(userData);
            
            if (result.success) {
                showNotification(result.data.message || 'Successful registration', 'success');
                
                setTimeout(() => {
                    window.location.href = './loginClient.html';
                }, 2000);
            } else {
                const errorMessage = result.error?.message || 
                                   result.error?.[0]?.description || 
                                   'Registration error';
                showNotification(errorMessage, 'error');
            }
        } catch (error) {
            showNotification('Connection error. Please try again.', 'error');
        } finally {
            submitBtn.innerHTML = originalText;
            submitBtn.disabled = false;
        }
    });

    const cancelBtn = document.querySelector('.btn-cancel');
    cancelBtn.addEventListener('click', function(e) {
        e.preventDefault();
        window.location.href = './index.html';
    });
});

function validateForm(userData, confirmPassword) {
    if (!userData.userName || userData.userName.length < 3) {
        return { isValid: false, message: 'The name must be at least 3 characters long' };
    }

    if (!userData.email || !isValidEmail(userData.email)) {
        return { isValid: false, message: 'Invalid email' };
    }

    if (!userData.password || userData.password.length < 6) {
        return { isValid: false, message: 'The password must be at least 6 characters long' };
    }

    if (userData.password !== confirmPassword) {
        return { isValid: false, message: 'The passwords do not match' };
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
                errorMessage = 'Minimum 3 characters';
            }
            break;
        case 'InputEmailRegister':
            if (!isValidEmail(value)) {
                isValid = false;
                errorMessage = 'Invalid email';
            }
            break;
        case 'InputPasswordRegister':
            if (value.length < 6) {
                isValid = false;
                errorMessage = 'Minimum 6 characters';
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