using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Core
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize) //get data from http query
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source,int pageNumber,int pageSize) 
        {
                var count = await source.CountAsync();//query on our data base -> how many item we have in the list totaly

                var items = await source.Skip((pageNumber-1) * pageSize).Take(pageSize).ToListAsync();  // pageNumber 1 => is page index 0

                return new PagedList<T>(items,count,pageNumber,pageSize); //**items** => items we will used.
        }
    }
}