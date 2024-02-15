<?php
$servername = "localhost";
$username = "usernamehere"; // your MySQL username
$password = "password"; // your MySQL password
$dbname = "games";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Define an array to store online users
$online_users = array();

// Check if user's last activity was within last 5 minutes (adjust as needed)
$timestamp_threshold = time() - (5 * 60);

// Prepare SQL statement to select online users
$sql = "SELECT username FROM users WHERE last_activity > ?";

// Prepare the statement
$stmt = $conn->prepare($sql);

// Bind parameters and execute the statement
$stmt->bind_param("i", $timestamp_threshold);

// Execute the statement
$stmt->execute();

// Get result
$result = $stmt->get_result();

// Fetch online users and store them in the array
while ($row = $result->fetch_assoc()) {
    $online_users[] = $row['username'];
}

// Close statement and connection
$stmt->close();
$conn->close();

// Output online users in JSON format
echo json_encode($online_users);
?>
