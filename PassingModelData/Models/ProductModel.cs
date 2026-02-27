namespace PassingModelData.Models
{
    public enum Category
    {
        SHOES,
        T_SHIRTS,
        PANTS,
        SHORTS,
        SWEATSHIRT
    }
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Category Category { get; set; }
        public string? Description { get; set; }
        public float? Price { get; set; }
    }
}
