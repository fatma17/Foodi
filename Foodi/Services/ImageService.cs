namespace Foodi.Services
{
    public static class ImageService
    {
        public static async Task<String> SaveImage(IFormFile image)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

            var path = Path.Combine("wwwroot/images", coverName);

            using var stream = File.Create(path);
            await image.CopyToAsync(stream);

            return coverName;
     
        }


        public static string delete(string imageName)
        {
            if (imageName == null)
            {
                return ("Image not found");
            }

            string imagePath = Path.Combine("wwwroot", "images", imageName);

            if (!System.IO.File.Exists(imagePath))
            {
                return ("Image not found");
            }
            System.IO.File.Delete(imagePath);

            return ($"Image '{imageName}' has been deleted");
        }
    }
}
