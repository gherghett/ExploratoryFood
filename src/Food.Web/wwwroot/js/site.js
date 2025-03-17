// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function formatSwedishNumber(value) {
    return value.toLocaleString("sv-SE", {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });
}