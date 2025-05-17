<?php
if ($_SERVER["REQUEST_METHOD"] === "POST") {
    $nev = ucwords(strtolower(trim(htmlspecialchars($_POST['nev']))));
    $email = trim(htmlspecialchars($_POST['email']));
    $jelszo = htmlspecialchars($_POST['jelszo']);
    $kor = htmlspecialchars($_POST['kor']);
    $szuldatum = htmlspecialchars($_POST['szuldatum']);
    $orszag = htmlspecialchars($_POST['orszag']);
    $hobbi = isset($_POST['hobbi']) ? implode(", ", $_POST['hobbi']) : "Nincs megadva";
    $nem = htmlspecialchars($_POST['nem']);
    $bemutatkozas = htmlspecialchars($_POST['bemutatkozas']);

    $fenykepEleresiUt = "";

    if (isset($_FILES['fenykep']) && $_FILES['fenykep']['error'] === UPLOAD_ERR_OK) {
        $feltoltottFajl = $_FILES['fenykep']['tmp_name'];
        $celFajl = 'feltoltesek/' . basename($_FILES['fenykep']['name']);
        if (!file_exists('feltoltesek')) {
            mkdir('feltoltesek', 0777, true);
        }
        if (move_uploaded_file($feltoltottFajl, $celFajl)) {
            $fenykepEleresiUt = $celFajl;
        }
    }

    echo "<h1>Beküldött adatok</h1>";
    echo "<p><strong>Teljes név:</strong> $nev</p>";
    echo "<p><strong>Email:</strong> $email</p>";
    echo "<p><strong>Kor:</strong> $kor</p>";
    echo "<p><strong>Születési dátum:</strong> $szuldatum</p>";
    echo "<p><strong>Ország:</strong> $orszag</p>";
    echo "<p><strong>Hobbi:</strong> $hobbi</p>";
    echo "<p><strong>Nem:</strong> $nem</p>";
    echo "<p><strong>Bemutatkozás:</strong> $bemutatkozas</p>";

    if ($fenykepEleresiUt !== "") {
        echo "<p><strong>Feltöltött fénykép:</strong></p>";
        echo "<img src='$fenykepEleresiUt' alt='Feltöltött kép' style='max-width:300px; height:auto;'>";
    }
} else {
    echo "<p>Hibás kérés.</p>";
}
