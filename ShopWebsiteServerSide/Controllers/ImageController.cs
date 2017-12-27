using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebsite.BLL.Contracts;
using ShopWebsite.DAL.Models.ImageModels;
using ShopWebsiteServerSide.Models.ImageModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebsiteServerSide.Controllers
{
    [Authorize]
    [Route("api/image")]
    public class ImageController : BaseController
    {
        public ImageController()
        {

        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> ReceiveImage(ImageModelWrapper receiveImage)
        {
            if (ModelState.IsValid)
            {
                var imageService = GetService<IImageService>();
                if (receiveImage == null || receiveImage.ImgFile == null) return BadRequest();

                Guid guid = Guid.NewGuid();
                if (receiveImage.ImgModel == null) receiveImage.ImgModel = new ImageModel();
                receiveImage.ImgModel.Name = receiveImage.ImgFile.FileName;
                receiveImage.ImgModel.Extension = receiveImage.ImgFile.ContentType;

                byte[] imgData;
                using (var memoryStream = new MemoryStream())
                {
                    await receiveImage.ImgFile.CopyToAsync(memoryStream);
                    imgData = memoryStream.ToArray();
                    var serviceResult = await imageService.AddImage(receiveImage.ImgModel, imgData);

                    if (serviceResult.Succeed)
                    {
                        return Ok(serviceResult);
                    }
                    else
                    {
                        return BadRequest(serviceResult);
                    }
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
