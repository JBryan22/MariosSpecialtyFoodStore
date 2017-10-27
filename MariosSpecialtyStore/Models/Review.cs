using System;
using System.ComponentModel.DataAnnotations;

namespace MariosSpecialtyStore.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Content_Body { get; set; }
        [Required]
        [Range(1,5)]
        public int Rating { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public Review()
        {
        }

        public Review(string author, string contentBody, int rating)
        {
            this.Author = author;
            this.Content_Body = contentBody;
            this.Rating = rating;
        }

		public override bool Equals(System.Object otherReview)
		{
			if (!(otherReview is Review))
			{
				return false;
			}
			else
			{
				Review newItem = (Review)otherReview;
				return this.ReviewId.Equals(newItem.ReviewId);
			}
		}

		public override int GetHashCode()
		{
			return this.ReviewId.GetHashCode();
		}


	}
}
