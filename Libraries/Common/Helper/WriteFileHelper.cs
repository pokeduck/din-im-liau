using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace Common.Helper;


public class WriteFileHelper
{

    public static async Task<(string fullUrl, string path)> WriteImageFile(IFormFile file)
    {
        if (file.Length <= 0)
        {
            throw new InvalidDataException("File size less than 0.");
        }

        var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);

        var streamArray = memoryStream.ToArray();

        var hashed = HashHelper.Generate(streamArray);



        var folderPrefix = "upload";
        var fileFullName = file.FileName;
        var ext = Path.GetExtension(fileFullName);
        var fullFolderPrefixPath = ext switch
        {
            ".jpg" or ".jpeg" or ".png" => Path.Combine(folderPrefix, "images"),
            _ => throw new NotImplementedException("Unsupported file format."),
        };

        if (!Directory.Exists(fullFolderPrefixPath))
        {
            Directory.CreateDirectory(fullFolderPrefixPath);
        }

        var fullFileNamePath = Path.Combine(fullFolderPrefixPath, fileFullName);
        if (File.Exists(fullFileNamePath))
        {
            return ("", "");
        }


        Console.WriteLine("");
        return ("", "");
    }

}
