using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanTriQuanNuoc.Models
{
    public class CategoryCreateRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string SeoAlias { get; set; }

        public string SeoDescription { get; set; }

        public int SortOrder { get; set; }

        public int? ParentId { get; set; }
    }
}