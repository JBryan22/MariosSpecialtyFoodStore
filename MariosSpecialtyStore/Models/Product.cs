using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MariosSpecialtyStore.Models                           
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Cost { get; set; }
        [Required]
        public string Country { get; set; }

        [Display(Name = "Product Image")]
        public byte[] ProductImg { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }


        public Product()
        {
        }

        public Product(string name, double cost, string country)
        {
            this.Name = name;
            this.Cost = cost;
            this.Country = country;
        }

        public double AverageRating()
        {
            if (Reviews.Count > 0)
            {
                return Math.Round(Reviews.Sum(review => review.Rating) / (double)Reviews.Count(), 2);

            }
            else
            {
                return 0;
            }
        }


		public override bool Equals(System.Object otherProduct)
		{
			if (!(otherProduct is Product))
			{
				return false;
			}
			else
			{
				Product newItem = (Product)otherProduct;
				return this.ProductId.Equals(newItem.ProductId);
			}
		}

		public override int GetHashCode()
		{
			return this.ProductId.GetHashCode();
		}
    }
}
