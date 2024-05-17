using System.Runtime.CompilerServices;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
namespace Services;

class FileUploader
{

  public static async Task Test()
  {
    var bucketName = "doc-example-bucket";
    var keyName = "samplefile.txt";
    var filePath = $"source\\{keyName}";
    var uploader = new FileUploader();

    await uploader.Upload(bucketName, keyName, filePath);

  }

  //The following code is from https://github.com/awsdocs/aws-doc-sdk-examples/blob/main/dotnetv3/S3/UploadUsingPresignedURLExample/UploadUsingPresignedURL.cs .
  private HttpClient httpClient = new HttpClient();
  public async Task Upload(string bucketName, string keyName, string filePath)
  {
    double timeoutDuration = 12;

    AWSConfigsS3.UseSignatureVersion4 = true;
    IAmazonS3 client = new AmazonS3Client(RegionEndpoint.USWest1);

    var url = GeneratePreSignedURL(client, bucketName, keyName, timeoutDuration);
    var isSuccess = await UploadObject(filePath, url);

    if (isSuccess)
    {
      Console.WriteLine("Upload successed.");
    }
    else
    {
      Console.WriteLine("Uplaod Failed.");
    }
  }

  private string GeneratePreSignedURL(IAmazonS3 client, string bucketName, string objectKey, double duration)
  {
    var request = new GetPreSignedUrlRequest
    {
      BucketName = bucketName,
      Key = objectKey,
      Verb = HttpVerb.PUT,
      Expires = DateTime.UtcNow.AddHours(duration)
    };

    string url = client.GetPreSignedURL(request);
    return url;
  }

  private async Task<bool> UploadObject(string filePath, string url)
  {
    using var streamContent = new StreamContent(new FileStream(filePath, FileMode.Open, FileAccess.Read));

    var response = await httpClient.PutAsync(url, streamContent);
    var respCode = response.IsSuccessStatusCode;
    return respCode;
  }
}
