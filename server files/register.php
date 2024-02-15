<?php
$servername = "localhost";
$username = "username"; // your MySQL username
$password = "password"; // your MySQL password
$dbname = "games";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Prepare SQL statement with placeholders
$sql = "INSERT INTO users (username, password) VALUES (?, ?)";

// Prepare the statement
$stmt = $conn->prepare($sql);

// Bind parameters and execute the statement
$stmt->bind_param("ss", $username, $hashed_password);

// Set parameters and execute
$username = $_POST['username'];
$password = $_POST['password'];
$hashed_password = password_hash($password, PASSWORD_DEFAULT);

// Execute the statement
if ($stmt->execute()) {
    echo "New record created successfully";
} else {
    echo "Error: " . $sql . "<br>" . $conn->error;
}

// Close statement and connection
$stmt->close();
$conn->close();
?>
