// Fetch data from the backend (Node.js)
fetch('http://localhost:5000/execute-query', {
    method: 'POST',
    headers: {
        'Content-Type': 'application/json'
    },
    body: JSON.stringify({
        query: 'SELECT * FROM employee;' // SQL query
    })
    })
    .then(response => response.json())
    .then(data => {
    const tableHead = document.querySelector('#employee-table thead');
    const tableBody = document.querySelector('#employee-table tbody');
    
    tableBody.innerHTML = '';  // Clear any existing data
    tableHead.innerHTML = '';  // Clear any existing headers
    
    // Ensure there is data to process
    if (data.length > 0) {
        // Create and insert the table header
        const headerRow = document.createElement('tr');
        
        // Get the keys (column names) from the first record
        const columns = Object.keys(data[0]);
        
        columns.forEach(column => {
        const th = document.createElement('th');
        th.textContent = column;  // Set the header text to column name
        headerRow.appendChild(th);
        });
        
        // Append the header row to the thead
        tableHead.appendChild(headerRow);
        
        // Insert new rows for each record
        data.forEach(record => {
        const row = document.createElement('tr');
        Object.values(record).forEach(value => {
            const cell = document.createElement('td');
            cell.textContent = value; // Set the cell content
            row.appendChild(cell);
        });
        tableBody.appendChild(row);
        });
    } else {
        // In case there's no data
        const noDataRow = document.createElement('tr');
        const noDataCell = document.createElement('td');
        noDataCell.colSpan = columns.length;  // Span across all columns
        noDataCell.textContent = 'No data available';
        noDataRow.appendChild(noDataCell);
        tableBody.appendChild(noDataRow);
    }
    })
    .catch(error => {
    console.error('Error fetching data:', error);
    });