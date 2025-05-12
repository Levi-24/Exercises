<?php
header("Content-Type: text/html; charset=UTF-8");

define('DB_HOST', 'localhost');
define('DB_USER', 'root');
define('DB_PASS', '');
define('DB_NAME', 'csokik');

$conn = new mysqli(DB_HOST, DB_USER, DB_PASS, DB_NAME);

// Kapcsolat ellenőrzés
if ($conn->connect_error) {
    http_response_code(500);
    die("Kapcsolódási hiba: " . $conn->connect_error);
}

$conn->set_charset("utf8");

/*
Sikeres kódok
    200 OK – Kérés sikeresen teljesült.
    201 Created – Új erőforrás sikeresen létrejött (pl. POST után).
    204 No Content – Sikeres, de nincs visszaküldött tartalom.

Kliensoldali hibák
    400 Bad Request – Hibás szintaxis vagy érvénytelen kérés.
    401 Unauthorized – Hitelesítés szükséges.
    403 Forbidden – Jogosultság megtagadva.
    404 Not Found – Az erőforrás nem található.
    405 Method Not Allowed – Nem engedélyezett HTTP metódus (pl. GET helyett POST).
    422 Unprocessable Entity – Érvényes kérés, de nem feldolgozható (pl. hibás adatformátum).

Szerveroldali hibák
    500 Internal Server Error – Általános szerverhiba.
    502 Bad Gateway – Hibás válasz egy másik szervertől.
    503 Service Unavailable – Szolgáltatás nem elérhető (pl. karbantartás miatt).
*/