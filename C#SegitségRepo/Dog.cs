namespace LINQ240910
{
    internal class Dog
    {
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public bool Sex { get; set; }
        public string Breed { get; set; }
        public int Age => DateTime.Now.Year - Birth.Year;
        public float Weight { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Age} years old {Breed} {(Sex ? "boy" : "girl")})";
        }
    }
}
