using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using TagLib;
using TagLib.Id3v2;
using YoutubeDownloader.Core.Utils;
using TagFile = TagLib.File;

namespace YoutubeDownloader.Core.Tagging;

internal partial class MediaFile(TagFile file) : IDisposable
{
    //public void SetThumbnail(byte[] thumbnailData) =>
    //    file.Tag.Pictures = [new Picture(thumbnailData)];

    public void SetThumbnail(byte[] thumbnailData)
    {
        var ms = new MemoryStream();

        using var image = Image.Load(thumbnailData);
        image.SaveAsJpeg(
            ms,
            new JpegEncoder()
            {
                //Method = WebpEncodingMethod.BestQuality
            }
        );

        thumbnailData = ms.ToArray();

        var picture = new AttachmentFrame
        {
            TextEncoding = FileEx.IsValidISO(thumbnailData) ? StringType.Latin1 : StringType.UTF8,
            Data = thumbnailData
        };

        file.Tag.Pictures = [picture];

        //image.SaveAsJpeg(@"D:\My Music2\July 3 2024\test.jpg");
    }

    public void SetArtist(string artist) => file.Tag.Performers = [artist];

    public void SetArtistSort(string artistSort) => file.Tag.PerformersSort = [artistSort];

    public void SetTitle(string title) => file.Tag.Title = title;

    public void SetAlbum(string album) => file.Tag.Album = album;

    public void SetDescription(string description) => file.Tag.Description = description;

    public void SetComment(string comment) => file.Tag.Comment = comment;

    public void Dispose()
    {
        file.Tag.DateTagged = DateTime.Now;
        file.Save();
        file.Dispose();
    }
}

internal partial class MediaFile
{
    public static MediaFile Create(string filePath) => new(TagFile.Create(filePath));
}
