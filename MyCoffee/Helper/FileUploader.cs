namespace erp.BL.Helper
{
    public static class FileUploader
    {
        public static string UploadFile(string Folder,IFormFile FileName)
        {
            try {
                var Filepath = Directory.GetCurrentDirectory() + "/wwwroot/" + Folder;
                var Filename = Guid.NewGuid() + Path.GetFileName(FileName.FileName);
                var fPhoto = Path.Combine(Filepath, Filename);
                using (var stream = new FileStream(fPhoto, FileMode.Create))
                {
                    FileName.CopyTo(stream);
                }
                return Filename;
            } catch(Exception ex) {


                return ex.Message;
            }

        }
        public static string RemoveFile(string Folder, string FileName)
        {
            try
            {
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + "/wwwroot/"+ Folder + FileName))
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "/wwwroot/" + Folder + FileName);
                }
               
                return "File Removed";
            }
            catch (Exception ex)
            {


                return ex.Message;
            }

        }
    }
}
