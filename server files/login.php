<?php
ini_set('session.gc_maxlifetime', 50);


session_start();

$servername = "localhost";
$username = "your user name"; // your MySQL username
$password = "password"; // your MySQL password
$dbname = "games";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$username = $_POST['username'];
$password = $_POST['password'];

// Prepare SQL statement with placeholders
$sql = "SELECT * FROM users WHERE username = ?";

// Prepare the statement
$stmt = $conn->prepare($sql);

// Bind parameters and execute the statement
$stmt->bind_param("s", $username);

// Execute the statement
$stmt->execute();

// Get result
$result = $stmt->get_result();

// Check if a user with the given username exists
if ($result->num_rows > 0) {
    // Fetch the user record
    $row = $result->fetch_assoc();

    // Verify the password
    if (password_verify($password, $row['password'])) {
        // Update last_activity field
        $currentTime = date("Y-m-d H:i:s");
        $userId = $row['id']; // Assuming 'id' is the primary key of your 'users' table

        // Prepare SQL statement to update last_activity field
        $updateSql = "UPDATE users SET last_activity = ? WHERE id = ?";

        // Prepare the update statement
        $updateStmt = $conn->prepare($updateSql);

        // Bind parameters and execute the update statement
        $updateStmt->bind_param("si", $currentTime, $userId);
        $updateStmt->execute();

        // Close the update statement
        $updateStmt->close();

        // Store user information in session
        $_SESSION['username'] = $username;
        $_SESSION['user_id'] = $row['id']; // Assuming 'id' is the primary key of your 'users' table
        echo "Login successful";
    } else {
        echo "Invalid password";
    }
} else {
    echo "User not found";
}

// Close statement and connection
$stmt->close();
$conn->close();
?>