using System.ComponentModel.DataAnnotations;

namespace SnapShop.APIs.DTOs
{
    public class BasketItemDTO
    {
        [Required]
        public int Id { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        [Range(0.5,double.MaxValue,ErrorMessage = "Price Can't be less than 0.5LE")]
        public decimal Price { set; get; }
        [Required]
        public string PictureUrl { set; get; }
        [Required]
        public string Brand { set; get; }
        [Required]
        public string Typr { set; get; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "Quantity must be at least one iten")]
        public int Quantity { set; get; }
    }
}