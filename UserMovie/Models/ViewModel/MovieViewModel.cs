using System.ComponentModel.DataAnnotations;

namespace UserMovie.Models.ViewModel
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Release Date is required")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
    }
}
