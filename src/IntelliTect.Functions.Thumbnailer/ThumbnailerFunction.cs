using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Transforms;

namespace IntelliTect.Functions.Thumbnailer
{
    public static class ThumbnailerFunction
    {
        private const int Size = 200;
        private const int Quality = 75;

        [FunctionName(nameof(Run))]
        public static void Run(
            [QueueTrigger("thumbnailer-items")]
            string queueItem,
            [Blob("large-image-queue/{queueTrigger}", FileAccess.Read)]
            Stream largeImageStream,
            [Blob("thumbnail-result/{queueTrigger}", FileAccess.Write)]
            Stream imageSmall,
            TraceWriter log)
        {
            log.Info($"Thumbnailer function received blob\n Name:{queueItem} \n Size: {largeImageStream.Length} Bytes");


            /*using (Image<Rgba32> original = Image.Load(largeImageStream))
            {
                int width, height;
                if (original.Width > original.Height)
                {
                    log.Info($"Limiting width of {queueItem} to {Size} pixels.");
                    width = Size;
                    height = original.Height * Size / original.Width;
                }
                else
                {
                    log.Info($"Limiting height of {queueItem} to {Size} pixels.");
                    width = original.Width * Size / original.Height;
                    height = Size;
                }

                original.Mutate(ctx => ctx.Resize(width, height));

                var encoder = new JpegEncoder {Quality = Quality};

                original.SaveAsJpeg(imageSmall, encoder);*/
                log.Info($"Thumbnail saved to thumbnail-result/{queueItem} as quality {Quality}");
            //}
        }
    }
}