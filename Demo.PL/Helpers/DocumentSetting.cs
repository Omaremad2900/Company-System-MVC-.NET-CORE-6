namespace Demo.PL.Helpers
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string foldername)
        {
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", foldername);
            string filename = $"{file.FileName}";
            string filepath = Path.Combine(FolderPath, filename);
            using var Fs = new FileStream(filepath, FileMode.Create);
            file.CopyTo(Fs);
            return filename;

        }
        public static void DeleteFile(string filename, string foldername)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Files", foldername, filename);
            if (File.Exists(filepath)) 
                File.Delete(filepath);
        
        }


    }
}
