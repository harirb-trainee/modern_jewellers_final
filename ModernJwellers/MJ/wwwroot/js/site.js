// DOM Elements
const loginForm = document.getElementById('loginForm');
const forgotPassword = document.getElementById('forgotPassword');
const togglePassword = document.getElementById('togglePassword');
const passwordInput = document.getElementById('password');

// Toggle password visibility
togglePassword.addEventListener('click', function () {
    const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
    passwordInput.setAttribute('type', type);
    this.querySelector('i').classList.toggle('fa-eye-slash');
});

function loadTab(tab) {
    let url = "";
    switch (tab) {
        case "polish":
            url = "/Inventory/LoadPolishList";
            break;
        case "category":
            url = "/Inventory/LoadCategoryList";
            break;
        case "subcategory":
            url = "/Inventory/LoadSubCategoryList";
            break;
        case "attribute":
            url = "/Inventory/LoadAttributeList";
            break;
        case "material":
            url = "/Inventory/LoadMaterialList";
            break;
        case "inventory":
            url = "/Inventory/LoadItemList";
            break;
    }
    if (url !== "") {
        $("#" + tab + "Content").html(`<div class='text-center py-10 text-gray-500'>Loading...</div>`);
        $.get(url, function (data) {
            $("#" + tab + "Content").html(data);
        }).fail(function (xhr, status, error) {
            console.error("Error loading tab:", error);
        });
    }
}
