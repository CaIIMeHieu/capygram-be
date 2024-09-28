using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using static System.Net.Mime.MediaTypeNames;

namespace capygram.Media.Service
{
    public class MediaService : IMediaService
    {
        private readonly Cloudinary _cloudinary;
        public MediaService(Cloudinary cloudinary) { 
            _cloudinary = cloudinary;
        }
        public async Task<List<string>> UploadImageToCloudinary( List<byte[]> dataByte , string folder )
        {
            var listUrlResult = new List<string>();
            foreach (var media in dataByte)
            {
                using (MemoryStream memoryStream = new MemoryStream(media))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription($"{new Guid()}", memoryStream),
                        Folder = folder,
                        Overwrite = true,
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    listUrlResult.Add(uploadResult.Url.ToString());
                }
            }
            return listUrlResult;
        }
    }
}
