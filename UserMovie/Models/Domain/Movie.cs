using System.ComponentModel.DataAnnotations;

namespace UserMovie.Models.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Genre { get; set; }
        [DataType(DataType.Date)] public DateTime ReleaseDate { get; set; }
    }
}
