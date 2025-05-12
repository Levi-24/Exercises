<?php
if ($_SERVER['REQUEST_METHOD'] !== 'GET') {
    http_response_code(405);
    echo json_encode(["error" => "Érvénytelen kérési metódus. Csak GET engedélyezett."]);
    exit;
}

require_once 'connect.php';

try {
    $sql = "SELECT * FROM csokik";
    $result = $conn->query($sql);

    if (!$result) {
        throw new Exception("Lekérdezési hiba: " . $conn->error);
    }

    $csokik = [];

    while ($row = $result->fetch_assoc()) {
        $csokik[] = $row;
    }

    // JSON fájlba írás
    $jsonData = json_encode($csokik, JSON_UNESCAPED_UNICODE | JSON_PRETTY_PRINT);

    if (file_put_contents('csokik.json', $jsonData) === false) {
        throw new Exception("Nem sikerült a csokik.json fájlba írni.");
    }

    // Kimenet
    header('Content-Type: application/json; charset=utf-8');
    echo $jsonData;

} catch (Exception $e) {
    http_response_code(500);
    echo json_encode(["error" => "Szerverhiba: " . $e->getMessage()]);
    exit;
}

$conn->close();
?>
