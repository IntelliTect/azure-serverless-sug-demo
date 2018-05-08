using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace IntelliTect.Functions.Thumbnailer
{
    public static class ThumbnailerFunction
    {
        [FunctionName(nameof(Run))]
        public static void Run([BlobTrigger("large-image-queue/{name}", Connection =
                "DefaultEndpointsProtocol=https;AccountName=sugdemostorage;AccountKey=d9zKNkR9toGBpegqLlpZgVJjdJyKdnqt9O7FRGHI6SmPRNlZ6drxzxKYClQYqHOXsixDwN0sjKVPcmJYslBfYw==;EndpointSuffix=core.windows.net")]
            Stream myBlob, string name, TraceWriter log)
        {
            log.Info($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}