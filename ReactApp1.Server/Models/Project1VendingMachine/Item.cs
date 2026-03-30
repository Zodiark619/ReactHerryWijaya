namespace ReactApp1.Server.Models.Project1VendingMachine
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId {  get; set; }
        public Category Category { get; set; }
    }
}
