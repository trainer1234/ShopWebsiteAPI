using ShopWebsite.BLL.Contracts;
using ShopWebsite.Common.Models.BaseModels;
using ShopWebsite.Common.Models.ServerOptions;
using ShopWebsite.Common.Utils;
using ShopWebsite.DAL.Contracts;
using ShopWebsite.DAL.Models.ImageModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebsite.BLL.Implementations
{
    public class ImageService : IImageService
    {
        const int HD_WIDTH = 1280;
        const int HD_HEIGHT = 720;

        private IImageRepository _imageRepository;
        private IErrorLogRepository _errorLogRepository;

        public ImageService(IImageRepository imageRepository, IErrorLogRepository errorLogRepository)
        {
            _imageRepository = imageRepository;
            _errorLogRepository = errorLogRepository;
        }

        public async Task<Result<string>> AddImage(ImageModel imageModel, byte[] imageData)
        {
            Result<string> imgUploadResult = new Result<string>();
            if (imageData == null)
            {
                imgUploadResult.Succeed = false;
                imgUploadResult.Errors = new Dictionary<int, string>();
                imgUploadResult.Errors.Add(15, "No image data");
            }
            else
            {
                try
                {
                    var handler = new ImageHandler();
                    Tuple<string, int, int> resizedImg = new Tuple<string, int, int>(imageModel.Name, HD_WIDTH, HD_HEIGHT);
                    resizedImg = handler.HandleImageUpload(ImageDirectoryOption.Original, imageData,
                        imageModel.Id, imageModel.Name, HD_WIDTH, HD_HEIGHT);
                    if (resizedImg == null)
                    {
                        imgUploadResult.Succeed = false;
                        imgUploadResult.Errors = new Dictionary<int, string>();
                        imgUploadResult.Errors.Add(16, "Folder image not exist");
                    }
                    else
                    {
                        imageModel.Name = resizedImg.Item1;
                        imageModel.Width = resizedImg.Item2;
                        imageModel.Height = resizedImg.Item3;
                        imageModel.CreatedTime = DateTime.UtcNow;

                        await _imageRepository.Add(imageModel);

                        imgUploadResult.Succeed = true;
                        imgUploadResult.Content = ImageUrlOption.Original + "/" + resizedImg.Item1;
                    }
                }
                catch (Exception ex)
                {
                    _errorLogRepository.Add(ex);
                    throw;
                }
            }
            return imgUploadResult;
        }

        public Task<Result<ImageModel>> GetImage(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> RemoveImage(string id)
        {
            throw new NotImplementedException();
        }
    }
}
