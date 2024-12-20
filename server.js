const express = require ('express');
const {Client} =require('pg')
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express();
app.use(cors());
app.use(bodyParser.json());

const con = new Client({
    host: "localhost",
    user: "postgres",
    port: 5432,
    password: "1234",
    database: "DataBasesProject"
})

con.connect().then(()=> console.log("connected"));

// var sql = "INSERT INTO employee(\"personlID\",name,surname,startdate,manager)VALUES (2,'ahmed','nagy','now','true');"

// con.query(sql,(err,res)=>{
//     if (!err){
//         console.log("adding is complated");
//     }else{
//         console.log(err.message)
//     }
// })

// con.query('Select * from "employee"',(err,res)=>{
//     if (!err){
//         console.log(res.rows)

//         // Call the function to generate the table
//         generateTable(res.rows);

//     }else{
//         console.log(err.message)
//     }
    
// })

// API endpoint to receive SQL query from frontend
app.post('/execute-query', async (req, res) => {
    const { query } = req.body;  // Get the SQL query from the frontend
  
    try {
      // Execute the SQL query
      const result = await con.query(query);
  
      // Send the results back to the frontend
      res.json(result.rows);
    } catch (error) {
      console.error('Error executing query', error.stack);
      res.status(500).send('Error executing the query');
    }
  });

// Close PostgreSQL client connection when the server shuts down
process.on('SIGINT', () => {
    console.log('Closing PostgreSQL connection');
    con.end();
    process.exit();
  });
  
  // Start the server
  const port = 5000;
  app.listen(port, () => {
    console.log(`Server running on http://localhost:${port}`);
  });

// con.end;

//     // Function to generate the table dynamically
//     function generateTable(data) {
//         const table = document.getElementById("employee-table");
//         const thead = table.querySelector("thead tr");
//         const tbody = table.querySelector("tbody");

//         // Clear any existing rows
//         tbody.innerHTML = '';

//         // Add table headers dynamically based on the first row keys (property names)
//         const headers = Object.keys(data[0]);
//         headers.forEach(header => {
//             const th = document.createElement("th");
//             th.innerText = header.charAt(0).toUpperCase() + header.slice(1); // Capitalize first letter
//             thead.appendChild(th);
//         });

//         // Add table rows for each record in res.rows
//         data.forEach(row => {
//             const tr = document.createElement("tr");
//             headers.forEach(header => {
//                 const td = document.createElement("td");
//                 td.innerText = row[header];
//                 tr.appendChild(td);
//             });
//             tbody.appendChild(tr);
//         });
    

// }







// document.addEventListener('DOMContentLoaded', () => {
//     const apiUrl = 'https://api.example.com/data'; // Replace with your API endpoint
//     const dataContainer = document.getElementById('data-container');

//     fetch(apiUrl)
//         .then(response => response.json())
//         .then(data => {
//             data.forEach(item => {
//                 const div = document.createElement('div');
//                 div.className = 'data-item';
//                 div.textContent = JSON.stringify(item); // Adjust based on your data structure
//                 dataContainer.appendChild(div);
//             });
//         })
//         .catch(error => {
//             console.error('Error fetching data:', error);
//             dataContainer.innerHTML = 'Failed to load data.';
//         });
// });