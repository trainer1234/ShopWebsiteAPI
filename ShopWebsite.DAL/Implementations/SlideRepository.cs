using Microsoft.EntityFrameworkCore;
using ShopWebsite.DAL.Context;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.SlideModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.DAL.Implementations
{
    public class SlideRepository : ISlideRepository
    {
        private ShopWebsiteSqlContext _context;

        public SlideRepository(ShopWebsiteSqlContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Slide newSlide)
        {
            var slideExist = await _context.Slides.Where(slide => slide.Id == newSlide.Id).ToListAsync();
            if (slideExist != null && slideExist.Count > 0)
            {
                return false;
            }
            else
            {
                var slides = await _context.Slides.CountAsync();
                newSlide.OrderId = slides + 1;

                _context.Add(newSlide);
                _context.SaveChanges();

                return true;
            }
        }

        public async Task<bool> Edit(Slide newSlide)
        {
            var slideExist = await _context.Slides.Where(slide => slide.Id == newSlide.Id).ToListAsync();
            if (slideExist != null && slideExist.Count > 0)
            {
                var searchSlide = slideExist[0];
                searchSlide.Title = newSlide.Title;
                searchSlide.Description = newSlide.Description;
                searchSlide.SlideImageUrl = newSlide.SlideImageUrl;

                _context.Update(searchSlide);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Slide> Get(string id)
        {
            var slideExist = await _context.Slides.Where(slide => slide.Id == id).ToListAsync();
            if (slideExist != null && slideExist.Count > 0)
            {
                return slideExist[0];
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Slide>> GetList()
        {
            var slideExist = await _context.Slides.OrderByDescending(slide => slide.OrderId).ToListAsync();
            if (slideExist != null && slideExist.Count > 0)
            {
                return slideExist;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Remove(string id)
        {
            var slideExist = await _context.Slides.Where(slide => slide.Id == id).ToListAsync();
            if (slideExist != null && slideExist.Count > 0)
            {
                _context.Remove(slideExist[0]);
                _context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
