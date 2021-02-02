namespace TechnineUzduotis.Models
{
    public abstract class Member
    {
        public string Name { get; set; }
        public bool TaxPayer { get; set; }
        public string Country { get; set; }
        protected Member() { }
        protected Member(string Name, bool TaxPayer, string Country)
        {
            this.Name = Name;
            this.TaxPayer = TaxPayer;
            this.Country = Country;
        }
    }
}
