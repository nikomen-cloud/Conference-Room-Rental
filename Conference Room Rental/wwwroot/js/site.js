// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Funkcja wywoływana po kliknięciu przycisku
function toggleTheme() {
    const currentTheme = document.documentElement.getAttribute('data-theme');
    const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
    
    document.documentElement.setAttribute('data-theme', newTheme);
    localStorage.setItem('theme', newTheme);
    updateButtonIcon(newTheme);
}

// Funkcja zmieniająca ikonę (słońce/księżyc)
function updateButtonIcon(theme) {
    const btn = document.getElementById('theme-toggle-btn');
    if (btn) {
        btn.innerText = theme === 'dark' ? '☀️' : '🌙';
    }
}

// Sprawdzenie ustawień przy załadowaniu
(function() {
    const savedTheme = localStorage.getItem('theme') || 'light';
    document.documentElement.setAttribute('data-theme', savedTheme);
    document.addEventListener("DOMContentLoaded", function() {
        updateButtonIcon(savedTheme);
    });
})();