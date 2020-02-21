using System.Runtime.Serialization;

namespace Common.DTO
{
    /// <summary>
    /// This object represents one size of a photo or a file / sticker thumbnail.
    /// </summary>
    public class PhotoSizeDTO
    {
        /// <summary>
        /// Identifier for this file, which can be used to download or reuse the file
        /// </summary>
        [DataMember(Name = "file_id")]
        public string FileId { get; set; }

        /// <summary>
        /// Unique identifier for this file, which is supposed to be the same over time and for different bots.
        /// Can't be used to download or reuse the file.
        /// </summary>
        [DataMember(Name = "file_unique_id")]
        public string FileUniqueId { get; set; }

        /// <summary>
        /// Photo width
        /// </summary>
        [DataMember(Name = "photo_width")]
        public int PhotoWidth { get; set; }

        /// <summary>
        /// Photo height
        /// </summary>
        [DataMember(Name = "photo_height")]
        public int PhotoHeight { get; set; }

        /// <summary>
        /// Optional. File size
        /// </summary>
        [DataMember(Name = "file_size")]
        public int Size { get; set; }
    }
}