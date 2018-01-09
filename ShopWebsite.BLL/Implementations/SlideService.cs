using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.ServerOptions;
using ShopWebsite.Common.Utils;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.SlideModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Implementations
{
    public class SlideService : ISlideService
    {
        private ISlideRepository _slideRepository;
        private IErrorLogRepository _errorLogRepository;

        public SlideService(ISlideRepository slideRepository, IErrorLogRepository errorLogRepository)
        {
            _slideRepository = slideRepository;
            _errorLogRepository = errorLogRepository;
        }

        public async Task<Result<bool>> AddSlide(Slide newSlide, string userId)
        {
            var result = new Result<bool>();

            try
            {
                if (newSlide.SlideImageUrl != null)
                {
                    var handler = new ImageHandler();
                    var imageModelId = handler.GetImageId(ImageUrlOption.Original, newSlide.SlideImageUrl);
                    if (imageModelId != null)
                    {
                        newSlide.ImageModelId = imageModelId;
                    }
                }
                var addResult = await _slideRepository.Add(newSlide);
                if (addResult)
                {
                    result.Succeed = result.Content = true;
                }
                else
                {
                    result.Content = result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(20, "Slide existed");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<bool>> Edit(Slide newSlide, string userId)
        {
            var result = new Result<bool>();

            try
            {
                if (newSlide.SlideImageUrl != null)
                {
                    var handler = new ImageHandler();
                    var imageModelId = handler.GetImageId(ImageUrlOption.Original, newSlide.SlideImageUrl);
                    if (imageModelId != null)
                    {
                        newSlide.ImageModelId = imageModelId;
                    }
                }
                var editResult = await _slideRepository.Edit(newSlide);
                if (editResult)
                {
                    result.Succeed = result.Content = true;
                }
                else
                {
                    result.Content = result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(21, "No slides");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<Slide>> Get(string slideId)
        {
            var result = new Result<Slide>();

            try
            {
                var slide = await _slideRepository.Get(slideId);
                if (slide != null)
                {
                    result.Content = slide;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(21, "No slides");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<List<Slide>>> GetList()
        {
            var result = new Result<List<Slide>>();

            try
            {
                var slide = await _slideRepository.GetList();
                if (slide != null)
                {
                    result.Content = slide;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(21, "No slides");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }

        public async Task<Result<bool>> Remove(string id, string userId)
        {
            var result = new Result<bool>();

            try
            {
                var removeResult = await _slideRepository.Remove(id);
                if (removeResult)
                {
                    result.Content = true;
                    result.Succeed = true;
                }
                else
                {
                    result.Succeed = false;
                    result.Errors = new Dictionary<int, string>();
                    result.Errors.Add(21, "No slides");
                }
            }
            catch (Exception ex)
            {
                _errorLogRepository.Add(ex);
                throw;
            }
            return result;
        }
    }
}
