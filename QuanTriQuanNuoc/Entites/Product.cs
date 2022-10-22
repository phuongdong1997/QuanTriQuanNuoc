using QuanTriQuanNuoc.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanTriQuanNuoc.Entites
{
    [Table("Products")]
    public class Product : IDateTracking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        [Required]
        public decimal OriginalPrice { get; set; }

        [StringLength(-1)]
        public string Description { get; set; }

        [StringLength(-1)]
        public string CongThuc { get; set; }

        [StringLength(255)]
        public string Unit { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        [Required]
        public string SeoAlias { get; set; }

        [MaxLength(500)]
        public string SeoDescription { get; set; }

        [Required]
        public int SortOrder { get; set; }

        public bool Status { set; get; }

        [Required]
        [Range(1, Double.PositiveInfinity)]
        public int CategoryId { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}