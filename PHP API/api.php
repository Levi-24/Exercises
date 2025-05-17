<?php
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: GET, POST, PUT, DELETE');
header('Access-Control-Allow-Headers: Content-Type');

$mysqli = new mysqli("localhost", "root", "", "brickhub");

if ($mysqli->connect_error) {
    die(json_encode(['error' => 'Connection failed: ' . $mysqli->connect_error]));
}

$method = $_SERVER['REQUEST_METHOD'];

$inputRaw = file_get_contents("php://input");
$input = json_decode($inputRaw, true) ?? [];

if ($input === null) {
    http_response_code(400);
    echo json_encode(['error' => 'Invalid JSON']);
    exit;
}

switch ($method) {
    case 'GET':
        if (isset($_GET['id'])) {
            $id = intval($_GET['id']);
            $result = $mysqli->query("SELECT * FROM genres WHERE id = $id");
            $data = $result->fetch_assoc();
            if ($data) {
                http_response_code(200);
                echo json_encode($data);
            } else {
                http_response_code(404);
                echo json_encode(['error' => 'Nem található']);
            }
        } else {
            $result = $mysqli->query("SELECT * FROM genres");
            http_response_code(200);
            echo json_encode($result->fetch_all(MYSQLI_ASSOC));
        }
        break;

    case 'POST':
        if (!isset($input['name'], $input['slug'])) {
            http_response_code(400);
            echo json_encode(['error' => 'Hiányzó kötelező mezők']);
            exit;
        }

        $name = $mysqli->real_escape_string($input['name']);
        $slug = $mysqli->real_escape_string($input['slug']);

        $stmt = $mysqli->prepare("INSERT INTO genres (name, slug) VALUES (?, ?)");
        $stmt->bind_param("ss", $name, $slug);

        //ss == string, string
        //i == integer
        //d == double
        //b == blob

        if ($stmt->execute()) {
            http_response_code(201);
            echo json_encode(['message' => "Sikeresen hozzáadva"]);
        } else {
            http_response_code(500);
            echo json_encode(['error' => 'Beszúrás sikertelen: ' . $mysqli->error]);
        }
        $stmt->close();
        break;

    case 'PUT':
        if (!isset($_GET['id'])) {
            http_response_code(400);
            echo json_encode(['error' => 'ID megadása szükséges']);
            exit;
        }
        $id = intval($_GET['id']);

        if (!isset($input['name'], $input['slug'])) {
            http_response_code(400);
            echo json_encode(['error' => 'Hiányzó kötelező mezők']);
            exit;
        }

        $name = $mysqli->real_escape_string($input['name']);
        $slug = $mysqli->real_escape_string($input['slug']);

        $stmt = $mysqli->prepare("UPDATE genres SET name = ?, slug = ? WHERE id = ?");
        $stmt->bind_param("ssi", $name, $slug, $id);

        if ($stmt->execute()) {
            if ($stmt->affected_rows > 0) {
                http_response_code(200);
                echo json_encode(['message' => "Sikeresen frissítve"]);
            } else {
                http_response_code(404);
                echo json_encode(['error' => 'Nem található vagy nincs változás']);
            }
        } else {
            http_response_code(500);
            echo json_encode(['error' => 'Frissítés sikertelen: ' . $mysqli->error]);
        }
        $stmt->close();
        break;


    case 'DELETE':
        if (!isset($_GET['id'])) {
            http_response_code(400);
            echo json_encode(['error' => 'ID megadása szükséges']);
            exit;
        }
        $id = intval($_GET['id']);

        // Törlés végrehajtása
        if ($mysqli->query("DELETE FROM genres WHERE id = $id")) {
            if ($mysqli->affected_rows > 0) {
                http_response_code(200);
                echo json_encode(['message' => "Sikeresen törölve"]);
            } else {
                http_response_code(404);
                echo json_encode(['error' => 'Nem található']);
            }
        } else {
            http_response_code(500);
            echo json_encode(['error' => 'Törlés sikertelen']);
        }
        break;

    default:
        http_response_code(405);
        echo json_encode(['error' => 'Hiba! Nem megfelelő metódus']);
        break;
}

$mysqli->close();

//GET       - http://localhost/api.php
//GET ID    - http://localhost/api.php?id=1
//POST      - http://localhost/api.php
            //use EchoAPI, POSTMAN, or CURL
            //{
            //  "name": "Racing",
            //  "slug": "racing"
            //}
//PUT       - http://localhost/api.php?id=1
            //use EchoAPI, POSTMAN, or CURL
            //{
            //  "name": "Updated",
            //  "slug": "updated"
            //}
//DELETE    - http://localhost/api.php?id=1