using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    /// <summary>
    /// Represents a single game available for purchase
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The unique identifier for each game product
        /// </summary>
        [Key]
        public int GameId { get; set; }

        /// <summary>
        /// The title of the video game
        /// </summary>
        [Required]
        public string Title { get; set; }
        

        /// <summary>
        /// The price of the video game 
        /// </summary>
        [Range(0, 1000)]
        public double Price { get; set; }

        // Todo: add rating
    }
}
