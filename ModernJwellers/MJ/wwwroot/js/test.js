
const jewelries = [
    { id: 1, name: 'Golden Pearl Necklace', image: 'https://images.unsplash.com/photo-1599643478518-a784e5dc4c8f', polish: 'Gold', type: 'Necklace', price: 12999, quantity: 25, threshold: 5 },
    { id: 2, name: 'Silver Diamond Ring', image: 'https://images.unsplash.com/photo-1602173574767-37ac01994b2a', polish: 'Silver', type: 'Ring', price: 8999, quantity: 12, threshold: 3 },
    { id: 3, name: 'Platinum Wedding Band', image: 'https://images.unsplash.com/photo-1605100804763-247f67b3557e', polish: 'Platinum', type: 'Ring', price: 24999, quantity: 8, threshold: 2 },
    { id: 4, name: 'Rose Gold Bracelet', image: 'https://images.unsplash.com/photo-1611591437281-4608be71d985', polish: 'Rose Gold', type: 'Bracelet', price: 15999, quantity: 18, threshold: 5 },
    { id: 5, name: 'Rhodium Plated Hoop Earrings', image: 'https://images.unsplash.com/photo-1611591437162-28fd2b539b8f', polish: 'Rhodium', type: 'Earrings', price: 5999, quantity: 4, threshold: 5 },
    { id: 6, name: 'Gold Plated Anklet', image: 'https://images.unsplash.com/photo-1605348530920-bb46767dfd80', polish: 'Gold', type: 'Anklet', price: 7999, quantity: 15, threshold: 5 },
    { id: 7, name: 'Silver Chain Necklace', image: 'https://images.unsplash.com/photo-1602173574767-37ac01994b2a', polish: 'Silver', type: 'Necklace', price: 11999, quantity: 2, threshold: 5 },
    { id: 8, name: 'Diamond Stud Earrings', image: 'https://images.unsplash.com/photo-1599643478518-a784e5dc4c8f', polish: 'Gold', type: 'Earrings', price: 21999, quantity: 7, threshold: 3 },
    { id: 9, name: 'Platinum Pendant', image: 'https://images.unsplash.com/photo-1605100804763-247f67b3557e', polish: 'Platinum', type: 'Pendant', price: 17999, quantity: 10, threshold: 4 },
    { id: 10, name: 'Rose Gold Nose Pin', image: 'https://images.unsplash.com/photo-1611591437281-4608be71d985', polish: 'Rose Gold', type: 'Nose Pin', price: 3999, quantity: 20, threshold: 10 },
    { id: 11, name: 'Gold Mangalsutra', image: 'https://images.unsplash.com/photo-1608042314453-ae338b9622df', polish: 'Gold', type: 'Mangalsutra', price: 25999, quantity: 6, threshold: 3 },
    { id: 12, name: 'Silver Toe Ring', image: 'https://images.unsplash.com/photo-1608042314453-ae338b9622df', polish: 'Silver', type: 'Ring', price: 2999, quantity: 0, threshold: 5 },
    { id: 13, name: 'Rhodium Bangles Set', image: 'https://images.unsplash.com/photo-1602173574767-37ac01994b2a', polish: 'Rhodium', type: 'Bangles', price: 14999, quantity: 3, threshold: 5 },
    { id: 14, name: 'Platinum Earcuffs', image: 'https://images.unsplash.com/photo-1605100804763-247f67b3557e', polish: 'Platinum', type: 'Earrings', price: 12999, quantity: 9, threshold: 4 },
    { id: 15, name: 'Gold Traditional Choker', image: 'https://images.unsplash.com/photo-1608042314453-ae338b9622df', polish: 'Gold', type: 'Necklace', price: 18999, quantity: 5, threshold: 3 },
    { id: 16, name: 'Silver Anklet Set', image: 'https://images.unsplash.com/photo-1602173574767-37ac01994b2a', polish: 'Silver', type: 'Anklet', price: 9999, quantity: 3, threshold: 5 },
    { id: 17, name: 'Rose Gold Brooch', image: 'https://images.unsplash.com/photo-1611591437281-4608be71d985', polish: 'Rose Gold', type: 'Brooch', price: 8999, quantity: 12, threshold: 5 },
    { id: 18, name: 'Rhodium Plated Hairpin', image: 'https://images.unsplash.com/photo-1611591437162-28fd2b539b8f', polish: 'Rhodium', type: 'Hair Accessory', price: 4999, quantity: 8, threshold: 5 },
    { id: 19, name: 'Gold Waistband', image: 'https://images.unsplash.com/photo-1608042314453-ae338b9622df', polish: 'Gold', type: 'Waistband', price: 15999, quantity: 4, threshold: 3 },
    { id: 20, name: 'Platinum Armlet', image: 'https://images.unsplash.com/photo-1605100804763-247f67b3557e', polish: 'Platinum', type: 'Armlet', price: 21999, quantity: 6, threshold: 2 },
    { id: 21, name: 'Silver Hair Chain', image: 'https://images.unsplash.com/photo-1602173574767-37ac01994b2a', polish: 'Silver', type: 'Hair Accessory', price: 6999, quantity: 10, threshold: 5 },
    { id: 22, name: 'Gold Temple Pendant', image: 'https://images.unsplash.com/photo-1608042314453-ae338b9622df', polish: 'Gold', type: 'Pendant', price: 16999, quantity: 7, threshold: 3 },
    { id: 23, name: 'Rose Gold Toe Ring', image: 'https://images.unsplash.com/photo-1611591437281-4608be71d985', polish: 'Rose Gold', type: 'Ring', price: 2999, quantity: 2, threshold: 5 },
    { id: 24, name: 'Diamond Nose Stud', image: 'https://images.unsplash.com/photo-1599643478518-a784e5dc4c8f', polish: 'Gold', type: 'Nose Pin', price: 8999, quantity: 15, threshold: 5 },
    { id: 25, name: 'Silver Maang Tikka', image: 'https://images.unsplash.com/photo-1602173574767-37ac01994b2a', polish: 'Silver', type: 'Head Jewelry', price: 11999, quantity: 3, threshold: 2 },
    { id: 26, name: 'Platinum Cufflinks', image: 'https://images.unsplash.com/photo-1605100804763-247f67b3557e', polish: 'Platinum', type: 'Cufflinks', price: 14999, quantity: 8, threshold: 3 },
    { id: 27, name: 'Gold Arm Band', image: 'https://images.unsplash.com/photo-1608042314453-ae338b9622df', polish: 'Gold', type: 'Armlet', price: 21999, quantity: 1, threshold: 2 },
    { id: 28, name: 'Silver Payal Set', image: 'https://images.unsplash.com/photo-1602173574767-37ac01994b2a', polish: 'Silver', type: 'Anklet', price: 14999, quantity: 6, threshold: 3 },
    { id: 29, name: 'Rose Gold Finger Ring', image: 'https://images.unsplash.com/photo-1611591437281-4608be71d985', polish: 'Rose Gold', type: 'Ring', price: 4499, quantity: 12, threshold: 5 },
    { id: 30, name: 'Gold Pendant Set', image: 'https://images.unsplash.com/photo-1608042314453-ae338b9622df', polish: 'Gold', type: 'Necklace', price: 24999, quantity: 5, threshold: 2 }
];

// Current state of filters
let currentState = {
    searchQuery: '',
    minPrice: 0,
    maxPrice: 50000,
    polishTypes: [],
    lowStockOnly: false,
    currentPage: 1,
    itemsPerPage: 10
};

// DOM elements
const inventoryTableBody = document.getElementById('inventoryTableBody');
const searchInput = document.getElementById('searchInput');
const priceFilterBtn = document.getElementById('priceFilterBtn');
const polishFilterBtn = document.getElementById('polishFilterBtn');
const stockFilterBtn = document.getElementById('stockFilterBtn');
const resetFiltersBtn = document.getElementById('resetFiltersBtn');
const priceFilterDropdown = document.getElementById('priceFilterDropdown');
const polishFilterDropdown = document.getElementById('polishFilterDropdown');
const priceRange = document.getElementById('priceRange');
const minPrice = document.getElementById('minPrice');
const maxPrice = document.getElementById('maxPrice');
const minPriceValue = document.getElementById('minPriceValue');
const maxPriceValue = document.getElementById('maxPriceValue');
const itemsPerPage = document.getElementById('itemsPerPage');
const prevPageBtn = document.getElementById('prevPageBtn');
const nextPageBtn = document.getElementById('nextPageBtn');
const paginationNumbers = document.getElementById('paginationNumbers');
const startItem = document.getElementById('startItem');
const endItem = document.getElementById('endItem');
const totalItems = document.getElementById('totalItems');

// Polish type checkboxes
const goldPolish = document.getElementById('goldPolish');
const silverPolish = document.getElementById('silverPolish');
const rhodiumPolish = document.getElementById('rhodiumPolish');
const platPolish = document.getElementById('platPolish');
const rosePolish = document.getElementById('rosePolish');
const otherPolish = document.getElementById('otherPolish');

// Initialize the page
document.addEventListener('DOMContentLoaded', function() {
    renderInventory();
    setupEventListeners();
});

// Set up event listeners
function setupEventListeners() {
    // Search input
    searchInput.addEventListener('input', function(e) {
        currentState.searchQuery = e.target.value.toLowerCase();
        currentState.currentPage = 1;
        renderInventory();
    });

    // Price Filter Button
    priceFilterBtn.addEventListener('click', function() {
        priceFilterDropdown.classList.toggle('open');
        polishFilterDropdown.classList.remove('open');
    });

    // Polish Filter Button
    polishFilterBtn.addEventListener('click', function() {
        polishFilterDropdown.classList.toggle('open');
        priceFilterDropdown.classList.remove('open');
    });

    // Stock Filter Button
    stockFilterBtn.addEventListener('click', function() {
        currentState.lowStockOnly = !currentState.lowStockOnly;
        this.classList.toggle('bg-yellow-100');
        this.classList.toggle('text-yellow-800');
        currentState.currentPage = 1;
        renderInventory();
    });

    // Reset Filters Button
    resetFiltersBtn.addEventListener('click', function() {
        resetFilters();
    });

    // Price Range Slider
    priceRange.addEventListener('input', function() {
        maxPrice.value = this.value;
        currentState.maxPrice = parseInt(this.value);
        maxPriceValue.textContent = formatNumber(this.value);
        currentState.currentPage = 1;
        renderInventory();
    });

    // Min Price Input
    minPrice.addEventListener('change', function() {
        if (parseInt(this.value) < 0) this.value = 0;
        if (parseInt(this.value) > parseInt(maxPrice.value)) this.value = maxPrice.value;
        currentState.minPrice = parseInt(this.value);
        minPriceValue.textContent = formatNumber(this.value);
        currentState.currentPage = 1;
        renderInventory();
    });

    // Max Price Input
    maxPrice.addEventListener('change', function() {
        if (parseInt(this.value) < parseInt(minPrice.value)) this.value = minPrice.value;
        if (parseInt(this.value) > 50000) this.value = 50000;
        currentState.maxPrice = parseInt(this.value);
        priceRange.value = this.value;
        maxPriceValue.textContent = formatNumber(this.value);
        currentState.currentPage = 1;
        renderInventory();
    });

    // Polish Type Checkboxes
    goldPolish.addEventListener('change', updatePolishFilters);
    silverPolish.addEventListener('change', updatePolishFilters);
    rhodiumPolish.addEventListener('change', updatePolishFilters);
    platPolish.addEventListener('change', updatePolishFilters);
    rosePolish.addEventListener('change', updatePolishFilters);
    otherPolish.addEventListener('change', updatePolishFilters);

    // Items per page dropdown
    itemsPerPage.addEventListener('change', function() {
        currentState.itemsPerPage = parseInt(this.value);
        currentState.currentPage = 1;
        renderInventory();
    });

    // Previous page button
    prevPageBtn.addEventListener('click', function(e) {
        e.preventDefault();
        if (currentState.currentPage > 1) {
            currentState.currentPage--;
            renderInventory();
        }
    });

    // Next page button
    nextPageBtn.addEventListener('click', function(e) {
        e.preventDefault();
        const filteredItems = filterItems();
        const totalPages = Math.ceil(filteredItems.length / currentState.itemsPerPage);
        if (currentState.currentPage < totalPages) {
            currentState.currentPage++;
            renderInventory();
        }
    });
}

// Update polish filters based on checkbox state
function updatePolishFilters() {
    currentState.polishTypes = [];
    if (goldPolish.checked) currentState.polishTypes.push('Gold');
    if (silverPolish.checked) currentState.polishTypes.push('Silver');
    if (rhodiumPolish.checked) currentState.polishTypes.push('Rhodium');
    if (platPolish.checked) currentState.polishTypes.push('Platinum');
    if (rosePolish.checked) currentState.polishTypes.push('Rose Gold');
    if (otherPolish.checked) currentState.polishTypes.push('Other');
    
    currentState.currentPage = 1;
    renderInventory();
}

// Reset all filters to default
function resetFilters() {
    // Reset search
    searchInput.value = '';
    currentState.searchQuery = '';
    
    // Reset price filter
    priceRange.value = 50000;
    maxPrice.value = 50000;
    minPrice.value = 0;
    currentState.minPrice = 0;
    currentState.maxPrice = 50000;
    minPriceValue.textContent = '0';
    maxPriceValue.textContent = '50000';
    
    // Reset polish filter
    goldPolish.checked = false;
    silverPolish.checked = false;
    rhodiumPolish.checked = false;
    platPolish.checked = false;
    rosePolish.checked = false;
    otherPolish.checked = false;
    currentState.polishTypes = [];
    
    // Reset low stock filter
    currentState.lowStockOnly = false;
    stockFilterBtn.classList.remove('bg-yellow-100', 'text-yellow-800');
    
    // Close dropdowns
    priceFilterDropdown.classList.remove('open');
    polishFilterDropdown.classList.remove('open');
    
    // Reset pagination
    currentState.currentPage = 1;
    itemsPerPage.value = 10;
    currentState.itemsPerPage = 10;
    
    renderInventory();
}

// Filter items based on current state
function filterItems() {
    return jewelries.filter(item => {
        // Search filter
        const matchesSearch = 
            currentState.searchQuery === '' || 
            item.name.toLowerCase().includes(currentState.searchQuery) || 
            item.type.toLowerCase().includes(currentState.searchQuery);
        
        // Price filter
        const matchesPrice = 
            item.price >= currentState.minPrice && 
            item.price <= currentState.maxPrice;
        
        // Polish type filter
        const matchesPolish = 
            currentState.polishTypes.length === 0 || 
            currentState.polishTypes.includes(item.polish);
        
        // Low stock filter
        const matchesStock = 
            !currentState.lowStockOnly || 
            item.quantity <= item.threshold;
        
        return matchesSearch && matchesPrice && matchesPolish && matchesStock;
    });
}

// Render the inventory table
function renderInventory() {
    const filteredItems = filterItems();
    const startIndex = (currentState.currentPage - 1) * currentState.itemsPerPage;
    const endIndex = startIndex + currentState.itemsPerPage;
    const paginatedItems = filteredItems.slice(startIndex, endIndex);
    
    // Clear table body
    inventoryTableBody.innerHTML = '';
    
    // Add items to table
    paginatedItems.forEach(item => {
        const status = getStatus(item.quantity, item.threshold);
        
        const row = document.createElement('tr');
        row.className = 'hover:bg-gray-50';
        row.innerHTML = `
            <td class="px-6 py-4 whitespace-nowrap">
                <input type="checkbox" class="h-4 w-4 text-blue-600 focus:ring-blue-500 border-gray-300 rounded">
            </td>
            <td class="px-6 py-4 whitespace-nowrap">
                <div class="flex items-center">
                    <div class="flex-shrink-0 h-10 w-10">
                        <img class="h-10 w-10 rounded-md object-cover" src="${item.image}" alt="${item.name}">
                    </div>
                    <div class="ml-4">
                        <div class="text-sm font-medium text-gray-900">${item.name}</div>
                        <div class="text-sm text-gray-500">${item.type}</div>
                    </div>
                </div>
            </td>
            <td class="px-6 py-4 whitespace-nowrap">
                <div class="text-sm text-gray-900 capitalize">${item.polish}</div>
            </td>
            <td class="px-6 py-4 whitespace-nowrap">
                <div class="text-sm text-gray-900">₹${formatNumber(item.price)}</div>
            </td>
            <td class="px-6 py-4 whitespace-nowrap">
                <div class="text-sm text-gray-900">${item.quantity}</div>
            </td>
            <td class="px-6 py-4 whitespace-nowrap">
                ${status}
            </td>
            <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                <div class="flex space-x-2">
                    <button class="text-blue-600 hover:text-blue-900">
                        <i class="fas fa-edit"></i>
                    </button>
                    <button class="text-red-600 hover:text-red-900">
                        <i class="fas fa-trash"></i>
                    </button>
                </div>
            </td>
        `;
        
        inventoryTableBody.appendChild(row);
    });
    
    // Update pagination
    updatePagination(filteredItems.length);
}

// Get status badge based on quantity
function getStatus(quantity, threshold) {
    if (quantity === 0) {
        return '<span class="px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-red-100 text-red-800">Out of Stock</span>';
    } else if (quantity <= threshold) {
        return '<span class="px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-yellow-100 text-yellow-800">Low Stock</span>';
    } else {
        return '<span class="px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-green-100 text-green-800">In Stock</span>';
    }
}

// Update pagination controls
function updatePagination(totalItemsCount) {
    const totalPages = Math.ceil(totalItemsCount / currentState.itemsPerPage);
    
    // Update page info
    startItem.textContent = ((currentState.currentPage - 1) * currentState.itemsPerPage) + 1;
    endItem.textContent = Math.min(currentState.currentPage * currentState.itemsPerPage, totalItemsCount);
    totalItems.textContent = totalItemsCount;
    
    // Enable/disable navigation buttons
    prevPageBtn.classList.toggle('opacity-50', currentState.currentPage === 1);
    prevPageBtn.classList.toggle('cursor-not-allowed', currentState.currentPage === 1);
    nextPageBtn.classList.toggle('opacity-50', currentState.currentPage === totalPages);
    nextPageBtn.classList.toggle('cursor-not-allowed', currentState.currentPage === totalPages);
    
    // Clear existing pagination numbers
    paginationNumbers.innerHTML = '';
    
    // Create pagination numbers
    const maxVisiblePages = 5;
    let startPage, endPage;
    
    if (totalPages <= maxVisiblePages) {
        // All pages visible
        startPage = 1;
        endPage = totalPages;
    } else {
        // Calculate start and end pages
        const maxPagesBeforeCurrent = Math.floor(maxVisiblePages / 2);
        const maxPagesAfterCurrent = Math.ceil(maxVisiblePages / 2) - 1;
        
        if (currentState.currentPage <= maxPagesBeforeCurrent) {
            // Near the beginning
            startPage = 1;
            endPage = maxVisiblePages;
        } else if (currentState.currentPage + maxPagesAfterCurrent >= totalPages) {
            // Near the end
            startPage = totalPages - maxVisiblePages + 1;
            endPage = totalPages;
        } else {
            // Somewhere in the middle
            startPage = currentState.currentPage - maxPagesBeforeCurrent;
            endPage = currentState.currentPage + maxPagesAfterCurrent;
        }
    }
    
    // Always show first page
    if (startPage > 1) {
        addPageNumber(1);
        if (startPage > 2) {
            addEllipsis();
        }
    }
    
    // Add page numbers
    for (let i = startPage; i <= endPage; i++) {
        addPageNumber(i);
    }
    
    // Always show last page
    if (endPage < totalPages) {
        if (endPage < totalPages - 1) {
            addEllipsis();
        }
        addPageNumber(totalPages);
    }
}

// Add a page number to pagination
function addPageNumber(pageNumber) {
    const pageBtn = document.createElement('a');
    pageBtn.href = '#';
    pageBtn.className = `relative inline-flex items-center px-4 py-2 border border-gray-300 bg-white text-sm font-medium ${currentState.currentPage === pageNumber ? 'text-blue-600 bg-blue-50' : 'text-gray-500 hover:bg-gray-50'}`;
    pageBtn.textContent = pageNumber;
    
    pageBtn.addEventListener('click', function(e) {
        e.preventDefault();
        currentState.currentPage = pageNumber;
        renderInventory();
    });
    
    paginationNumbers.appendChild(pageBtn);
}

// Add ellipsis to pagination
function addEllipsis() {
    const ellipsis = document.createElement('span');
    ellipsis.className = 'relative inline-flex items-center px-4 py-2 border border-gray-300 bg-white text-sm font-medium text-gray-700';
    ellipsis.innerHTML = '...';
    paginationNumbers.appendChild(ellipsis);
}

// Format number with commas
function formatNumber(num) {
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
