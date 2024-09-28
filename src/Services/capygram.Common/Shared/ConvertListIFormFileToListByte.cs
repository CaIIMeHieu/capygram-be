namespace capygram.Common.Shared
{
    public static class ConvertListIFormFileToListByte
    {
        public static async Task<List<byte[]>> Convert( List<IFormFile> files )
        {
            List<byte[]> fileDataList = new List<byte[]>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var stream = file.OpenReadStream())
                    {
                        // Chuyển đổi Stream thành byte array
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            fileDataList.Add(memoryStream.ToArray());
                        }
                    }
                }
            }
            return fileDataList;
        }
    }
}
