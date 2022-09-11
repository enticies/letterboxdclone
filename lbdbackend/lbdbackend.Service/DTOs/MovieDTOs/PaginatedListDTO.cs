using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lbdbackend.Service.DTOs.MovieDTOs {
    public class PaginatedListDTO<TItem> {
        public List<TItem> Items { get; set; } = new List<TItem>();
        public int TotalPage { get; set; }
        public int PageIndex { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
        public PaginatedListDTO(List<TItem> ıtems, int pageIndex, int pageItmeCount) {
            TotalPage = (int)Math.Ceiling((double)ıtems.Count / pageItmeCount);
            PageIndex = pageIndex;
            Items.AddRange(ıtems.Skip((pageIndex - 1) * pageItmeCount).Take(pageItmeCount));
            HasNext = PageIndex < TotalPage;
            HasPrev = PageIndex > 1;
        }
    }
}
