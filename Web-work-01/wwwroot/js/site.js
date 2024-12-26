// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function editEmployee(department) {
    // 將資料帶入表單的輸入框
    var selectElement = document.getElementById("department");
    selectElement.value = department;
    // 顯示彈出視窗
    document.getElementById('editModal').style.display = 'block';
}

// 關閉彈出視窗
function closeModal() {
    document.getElementById('editModal').style.display = 'none';
}
