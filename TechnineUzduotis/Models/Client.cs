namespace TechnineUzduotis.Models
{
    public class Client : Member
    {
        public bool Juridical { get; set; }
        public Client() { }
        public Client(string Name, bool TaxPayer, string Country, bool Juridical) : base(Name, TaxPayer, Country)
        {
            this.Juridical = Juridical;
        }
    }
}
