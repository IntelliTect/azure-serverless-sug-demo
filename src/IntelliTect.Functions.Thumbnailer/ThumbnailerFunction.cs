﻿using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SkiaSharp;

namespace IntelliTect.Functions.Thumbnailer
{
    public static class ThumbnailerFunction
    {
        private const int Size = 200;
        private const int Quality = 75;

        [FunctionName(nameof(Run))]
        public static void Run(
            [BlobTrigger("large-image-queue/{name}")]
            Stream largeImageStream,

            string name,

            [Blob("thumbnail-result/{name}", FileAccess.Write)]
            Stream imageSmall,

            TraceWriter log)
        {
            log.Info($"Thumbnailer function received blob\n Name:{name} \n Size: {largeImageStream.Length} Bytes");

            using (var inputStream = new SKManagedStream(largeImageStream, true))
            {
                using (SKBitmap original = SKBitmap.Decode(inputStream))
                {
                    int width, height;
                    if (original.Width > original.Height)
                    {
                        log.Info($"Limiting width of {name} to {Size} pixels.");
                        width = Size;
                        height = original.Height * Size / original.Width;
                    }
                    else
                    {
                        log.Info($"Limiting height of {name} to {Size} pixels.");
                        width = original.Width * Size / original.Height;
                        height = Size;
                    }

                    using (SKBitmap resized = original
                        .Resize(new SKImageInfo(width, height), SKBitmapResizeMethod.Lanczos3))
                    {
                        using (SKImage image = SKImage.FromBitmap(resized))
                        {
                            image.Encode(SKEncodedImageFormat.Jpeg, Quality).SaveTo(imageSmall);
                            log.Info($"Thumbnail encoded to thumbnail-result/{name} with quality: {Quality}");
                        }
                    }
                }
            }
        }
    }
}