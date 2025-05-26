// group by example with C# and LINQ


var grouped = cacti
    .GroupBy(c => c.TermesztesiNehezseg)
    .OrderBy(g => g.Key); // opcionális, hogy szép sorrendben legyen

foreach (var group in grouped)
{
    Console.WriteLine($"\t{group.Key}: {group.Count()}db");
}




//MySQL adatbázis kapcsolat és kutya osztály


private List<Kutya> kutyak = new List<Kutya>();

public MainForm()
{
    InitializeComponent();
    cbKutyak.SelectedIndexChanged += cbKutyak_SelectedIndexChanged;
}

private void btnBetoltes_Click(object sender, EventArgs e)
{
    kutyak.Clear();
    cbKutyak.Items.Clear();
    lbKutyaAdatok.Items.Clear();

    string connStr = "server=localhost;user=root;database=kutyak_db;port=3306;password=;Charset=utf8;";
    using (var conn = new MySqlConnection(connStr))
    {
        conn.Open();
        var cmd = new MySqlCommand(@"
                    SELECT k.id, k.nev, k.kan, k.kor, k.chipdatum, k.kepurl,
                           f.fajta, g.nev as gazda_nev, g.tel
                    FROM kutyak k
                    INNER JOIN fajtak f ON k.fajtaid = f.id
                    INNER JOIN gazdak g ON k.gazdaid = g.id
                ", conn);

        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                var kutya = new Kutya
                {
                    Nev = reader["nev"].ToString(),
                    Kan = (bool)reader["kan"],
                    Kor = Convert.ToInt32(reader["kor"]),
                    ChipDatum = Convert.ToDateTime(reader["chipdatum"]),
                    KepUrl = reader["kepurl"].ToString(),
                    Fajta = reader["fajta"].ToString(),
                    GazdaNev = reader["gazda_nev"].ToString(),
                    GazdaTel = reader["tel"].ToString()
                };
                kutyak.Add(kutya);
                cbKutyak.Items.Add(kutya.Nev);
            }
        }
    }

    lblAdatokSzama.Text = $"Beolvasott kutyák száma: {kutyak.Count}";
}


//WPF item is item?
private void appsComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
{
    if (appsComboBox.SelectedItem is GoogleApp selectedApp)
    {
        verzioLabel.Content = selectedApp.CurrentVer;
        tartalomLabel.Content = selectedApp.ContentRating.ContentRatingName;
        megtekintesLabel.Content = selectedApp.Reviews;
    }
    else
    {
        verzioLabel.Content = "";
        tartalomLabel.Content = "";
        megtekintesLabel.Content = "";
    }
} 