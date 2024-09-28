namespace capygram.Media.Service
{
    public interface IMediaService
    {
        public Task<List<string>> UploadImageToCloudinary(List<byte[]> dataByte, string folder);
    }
}
