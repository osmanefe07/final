using System.ComponentModel.DataAnnotations;

namespace final.ViewModels
{
    public class CarModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Araç Markası Giriniz!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Araç Modeli Giriniz!")]
        public string Title { get; set; }




        [Required(ErrorMessage = "Araç Açıklaması Giriniz!")]
        public string Description { get; set; }



        public bool Status { get; set; }



        [Required(ErrorMessage = "Araç Fiyatı Giriniz!")]
        public decimal Price { get; set; }

    }
}
