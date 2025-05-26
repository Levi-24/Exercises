using LINQ240910;

#region DOGS lista
List<Dog> dogs =
[
    new() //01
    {
        Name = "Kira",
        Birth = DateTime.Parse("2019-05-10"),
        Sex = false,
        Breed = "tacskó",
        Weight = 5.5F,
    },
    new() //02
    {
        Name = "Rex",
        Birth = DateTime.Parse("2014-04-20"),
        Sex = true,
        Breed = "németjuhász",
        Weight = 35.2F,
    },
    new() //03
    {
        Name = "Igor",
        Birth = DateTime.Parse("2017-11-20"),
        Sex = true,
        Breed = "kaukázusi farkasölő",
        Weight = 95.0F,
    },
    new() //04
    {
        Name = "Thomas Edison",
        Birth = DateTime.Parse("2001-02-07"),
        Sex = true,
        Breed = "németjuhász",
        Weight = 40.3F,
    },
    new() //05
    {
        Name = "Princess",
        Birth = DateTime.Parse("2022-12-24"),
        Sex = false,
        Breed = "palotapincsi",
        Weight = 4.2F,
    },
    new() //06
    {
        Name = "Démon",
        Birth = DateTime.Parse("2010-01-20"),
        Sex = true,
        Breed = "kaukázusi farkasölő",
        Weight = 82.0F,
    },
    new() //07
    {
        Name = "Nyomiii",
        Birth = DateTime.Parse("2011-02-21"),
        Sex = false,
        Breed = "németjuhász",
        Weight = 32.3F,
    },
    new() //08
    {
        Name = "Rongy",
        Birth = DateTime.Parse("2022-06-10"),
        Sex = false,
        Breed = "palotapincsi",
        Weight = 7.3F,
    },
];
#endregion

/* Milyen prog tételeket/nevezetes algoritmusokat ismerünk?
 * 
 * [x] megszámlálás
 * [x] sorozatszámítás (összegzés) -> átlagszámítás
 * [x] szélsőérték meghatározás (min, max) (hely és érték)
 * [x] lineáris keresés
 * [x] kiválasztás
 * [x] eldönttés
 * [x] "rendezések"
 * [x] kiválogatás
 * [x] szétválogatás (csoportosítás)
 * 
 * [ ] "halmaztételek"
*/

#region megszámlálás
// adott tulajdonságú elemek száma a kollekcióban
// 3 évnél öregebb kutyák száma
int haromEvnelIdosebb = 0;
foreach (var dog in dogs)
{
    if (dog.Age >= 3) haromEvnelIdosebb++;
}
Console.WriteLine($"3 évnél idősebb kutyák száma: {haromEvnelIdosebb} db");

int linqHaromEvnelIdosebb = dogs.Count(d => d.Age > 3);
Console.WriteLine($"eredmény count LINQ-val: {linqHaromEvnelIdosebb}");

// szukák száma
int szukakSzama = 0;
for (int i = 0; i < dogs.Count; i++)
{
    if (!dogs[i].Sex) szukakSzama++;
}
Console.WriteLine($"Szuka kutyák száma: {szukakSzama} db");
int linqSzukakSzama = dogs.Count(d => !d.Sex);
Console.WriteLine($"eredmény count LINQ-val: {linqSzukakSzama}");

//azon hím kutyák száma, akik neve tartalmaz 'e' betűt

int linqEbhk = dogs.Count(d => d.Sex && d.Name.ToLower().Contains('e'));
Console.WriteLine($"azon hím kutyák száma, akik nevében van 'e' betű: {linqEbhk}");

Console.WriteLine("-----------------------");
#endregion

#region összegzés
int kutyakOsszEletkora = 0;
foreach (var dog in dogs)
{
    kutyakOsszEletkora += dog.Age;
}
Console.WriteLine($"kutyák összéletkora: {kutyakOsszEletkora} év");

int linqKutyakOsszEletkora = dogs.Sum(d => d.Age);
Console.WriteLine($"eredmény sum LINQ-val: {linqKutyakOsszEletkora} év");

Console.WriteLine("-----------------------");
#endregion

# region átlagszámítás
float kutyakAtlagEletkora = kutyakOsszEletkora / (float)dogs.Count;
Console.WriteLine($"kutyák átlagéletkora: {kutyakAtlagEletkora} év");

double linqKutyakAtlagEletkora = dogs.Average(d => d.Age);
Console.WriteLine($"eredmény average LINQ-val {linqKutyakAtlagEletkora} év");

Console.WriteLine("-----------------------");
#endregion

#region szélsőérték meghatározás
//legnagyobb súlyú kutya súlya
//legnagyobb súlyú kutya neve
//legnagyobb súlyú kutya indexe a listában

int lnski = 0;
for (int i = 1; i < dogs.Count; i++)
{
    if (dogs[i].Weight > dogs[lnski].Weight)
    {
        lnski = i;
    }
}
Console.WriteLine($"legnagyobb súlyú kutya súlya: {dogs[lnski].Weight} Kg");
Console.WriteLine($"legnagyobb súlyú kutya neve: {dogs[lnski].Name} Kg");
Console.WriteLine($"legnagyobb súlyú kutya indexe: {lnski}");
Console.WriteLine($"legnagyobb súlyú kutya sorszáma: {lnski + 1}.");

float linqLnskS = dogs.Max(d => d.Weight);
Console.WriteLine($"eredmény max LINQ-val: {linqLnskS}");
Dog linqLnsk = dogs.MaxBy(d => d.Weight);
Console.WriteLine($"eredmény maxby LINQ-val: {linqLnsk.Name}");

DateTime linqLfkszd = dogs.Min(d => d.Birth);
Console.WriteLine($"eredmény min LINQ-val: {linqLfkszd:yyyy-MM-dd}");
Dog linqLfk = dogs.MinBy(d => d.Birth);
Console.WriteLine($"eredmény minby LINQ-val: {linqLfk.Name}");

Console.WriteLine("-----------------------");
#endregion

#region keresés (és "kiválasztás")
var linqFrst = dogs.First(d => d.Breed == "németjuhász");
Console.WriteLine($"a listában első nj: {linqFrst}");
//ha van egyezés, akkor rendre az ELSŐ matching elementet adja vissza
//ha nincs, "Sequence contains no matching element" exceptiont dob

var linqLst = dogs.Last(d => d.Breed == "németjuhász");
Console.WriteLine($"a listában utolsó nj: {linqLst}");
//ha van egyezés, akkor rendere az UTOLSÓ matching elementet adja vissza
//ha nincs, "Sequence contains no matching element" exceptiont dob

var linqSngl = dogs.Single(d => d.Breed == "németjuhász" && d.Age >= 20);
Console.WriteLine($"a listában egyetlen 20 évesnél öregebb nj: {linqSngl}");
//ha EGYETLEN egyezés van, akkor visszatér A matching elementel
//ha TÖBB egyezés is *LENNE*, akkor "Sequence contains more than one matching element" exceptiont dob
//ha nincs, "Sequence contains no matching element" exceptiont dob

//object x = 10;
//object y = "cica";
//object z = new Dog();
//Console.WriteLine((int)x + 2);

var linqFrstOrDef = dogs.FirstOrDefault(d => d.Breed == "unicorn");
Console.WriteLine($"az első unikornis a listában null?: {linqFrstOrDef is null}");
//ha van egyezés, akkor rendre az ELSŐ matching elementet adja vissza
//ha nincs találat, akkor ún. "default" értéket ad vissza, ami:
//   VALUE (struct) type esetén *általában* zéró
//   REFERENCE (class) type esetén mindig null

//int[] numbers = { 11, 26, 3, 132, 26, 44, 30, 1862, 50 };
//var res = numbers.FirstOrDefault(x => x % 200 == 0);
//Console.WriteLine($"result: {res}");

var linqLstOrDef = dogs.LastOrDefault(d => d.Breed == "tacskó");
Console.WriteLine($"utolsó tacskó: {linqLstOrDef}");
//ha van egyezés, akkor rendere az UTOLSÓ matching elementet adja vissza
//ha nincs találat, akkor ún. "default" értéket ad vissza, ami:
//   VALUE (struct) type esetén *általában* zéró
//   REFERENCE (class) type esetén mindig null

var linqSnglOrDef = dogs.SingleOrDefault(d => d.Breed == "pittbull");
Console.WriteLine(linqSnglOrDef is null ? "nincs pittbull" : $"egyetlen pittbull: {linqSnglOrDef}");
//ha EGYETLEN egyezés van, akkor visszatér A matching elementel
//ha TÖBB egyezés is *LENNE*, akkor "Sequence contains more than one matching element" exceptiont dob
//ha nincs találat, akkor ún. "default" értéket ad vissza, ami:
//   VALUE (struct) type esetén *általában* zéró
//   REFERENCE (class) type esetén mindig null

//dogs.Find()     <-- nem LINQ ua. mint a FirstOrDefrault()
//dogs.FindAll()  <-- nem LINQ ua. mint a Where()

//IndexOf()       <-- nem LINQ

////linker progtétel:
//int kerIndex = 0;
//while (kerIndex < dogs.Count && dogs[kerIndex].Breed != "pittbull") kerIndex++;
//Console.WriteLine($"output: {(kerIndex < dogs.Count ? kerIndex : -1)}");

var inst = dogs.FirstOrDefault(d => d.Breed == "pittbull");
int indexOfInst = dogs.IndexOf(inst);
Console.WriteLine($"első <condition> indexe: {indexOfInst}");
//ha indexof esetén nincs találat a keresett példányra, akkor -1-el tér vissza (nem exceptiont dob!)
Console.WriteLine("-----------------------");
#endregion

#region eldöntés
var linqAny01 = dogs.Any(d => d.Breed == "pittbull");
var linqAny02 = dogs.Any(d => d.Breed == "németjuhász");
Console.WriteLine($"{(linqAny01 ? "van" : "nincs")} pittbull");
Console.WriteLine($"{(linqAny02 ? "van" : "nincs")} németjuhász");
//ha van legalább egy matching element -> True
//ha nincs -> False
//List.Exist(<condition>) <== gyakorlatilag ugyan ez

var cont = dogs.Contains(linqFrst);
Console.WriteLine($"a {linqFrst}-ot {(cont ? "tartalmazza" : "nem tartalmazza")} a lista");
//contains paraméterben objektumot kap
//ha a par obj reverenciát, vagy a par struct értéket tartalmazza a lista, akkor -> True
//egyébként -> False

//akkor ad vissza TRUE-t, ha a kollekció minden elemére igaz a pred.
var linqAll = dogs.All(d => d.Birth.Year > 1950);
Console.WriteLine($"{(linqAll 
    ? "minden kutya" 
    : "van olyan kutya, aki nem")}" +
    $" 1950 után született");

var masikrex = new Dog()
{
    Name = "Rex",
    Birth = DateTime.Parse("2014-04-20"),
    Sex = true,
    Breed = "németjuhász",
};

//mivel Dog REF type, hiába PONTOSAN UGYAN AZOK a tulajdonságai, mint a lista 1-es indexű elemének, ez "nem az a rex", ezért a contains false-t ad vissza.
Console.WriteLine(dogs.Contains(masikrex));
#endregion

#region rendezés
//.Sort() <-- ez 'helyben' rendez, az elemek sorrendje megváltozik az eredeti adatszerkezetben

// az [orderby, orderbydescending, order, orderdescending] nem helyben rendez, hanem készül a kollekcióról egy olyan projekció, amiben az elemek sorrendje eltér (tehát az eredeti adatszerkezet elemeinek sorrendje NEM fog változni)

Console.WriteLine("---kutyusok névsorrendben---");
var linqOBNames = dogs.OrderBy(d => d.Name);
foreach (var dog in linqOBNames)
{
    Console.WriteLine($"[{dogs.IndexOf(dog)}] - {dog}");
}

Console.WriteLine("---kutyusok életkor szerint csökkenőben---");
var linqOBDAges = dogs.OrderByDescending(d => d.Age);
foreach (var dog in linqOBDAges)
{
    Console.WriteLine($"[{dogs.IndexOf(dog)}] - {dog}");
}

// van ThenBy(x => x) -> olyanon lehet lefuttatni, ami IOrderedEnumerable<T> kollekció, és a másodlagos rendezési szempontot tudod benne beállítani. [.ThenByDescanding(x => x)]

// Order() & OrderDescending()
// <- akkor használjuk, ha a kollekció elemei nem összetett adatszerkezetek
#endregion

#region kiválogatás
Console.WriteLine("-------------");
Console.WriteLine("2015 után született kutyák:");
var linqWhere = dogs
    .Where(d => d.Birth.Year >= 2015)
    .OrderBy(d => d.Age);
foreach (var dog in linqWhere)
{
    Console.WriteLine($"\t{dog}");
}
#endregion

#region szétválogatás (csoportosítás)
//IEnumerable<IGrouping<string, Dog>>
var linqGB = dogs.GroupBy(d => d.Breed);
Console.WriteLine("kutyusok csoportosítva fajta szerint");
foreach (var group in linqGB)
{
    Console.WriteLine($"\t{group.Key} ({group.Count()} db)");
    foreach (var dog in group.OrderBy(d => d.Name))
    {
        Console.WriteLine($"\t\t{dog}");
    }
}
#endregion

#region halmazműveleteket megvalósító LINQ függvények:
//.Union();
//.Intersect();
//.Except();
#endregion

var linqSelectDistinct = dogs
    .Select(d => d.Breed)
    .Distinct()
    .Order();

Console.WriteLine("kutyafajták:");
foreach (var breed in linqSelectDistinct)
{
    Console.WriteLine($"\t{breed}");
}