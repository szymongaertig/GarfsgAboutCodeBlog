namespace AspNetCoreApp.Model
{
    public class Product
    {
        public Product(int id, string name, decimal price, decimal someVerySecretBasePrice, string otherFieldThatIsVeryReserved)
        {
            Id = id;
            Name = name;
            Price = price;
            SomeVerySecretBasePrice = someVerySecretBasePrice;
            OtherFieldThatIsVeryReserved = otherFieldThatIsVeryReserved;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal SomeVerySecretBasePrice { get; set; }
        public string OtherFieldThatIsVeryReserved { get; set; }
    }
}
