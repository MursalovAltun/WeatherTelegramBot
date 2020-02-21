using System.Runtime.Serialization;

namespace Common.DTO
{
    /// <summary>
    /// This object represents a chat photo.
    /// </summary>
    public class ChatPhotoDTO
    {
        /// <summary>
        /// File identifier of small (160x160) chat photo. This file_id can be used only for photo download
        /// and only for as long as the photo is not changed.
        /// </summary>
        [DataMember(Name = "small_file_id")]
        public string SmallFileId { get; set; }

        /// <summary>
        /// Unique file identifier of small (160x160) chat photo,
        /// which is supposed to be the same over time and for different bots.
        /// Can't be used to download or reuse the file.
        /// </summary>
        [DataMember(Name = "small_file_unique_id")]
        public string SmallFileUniqueId { get; set; }

        /// <summary>
        /// File identifier of big (640x640) chat photo.
        /// This file_id can be used only for photo download and
        /// only for as long as the photo is not changed.
        /// </summary>
        [DataMember(Name = "big_file_id")]
        public string BigFileId { get; set; }

        /// <summary>
        /// Unique file identifier of big (640x640) chat photo,
        /// which is supposed to be the same over time and for different bots.
        /// Can't be used to download or reuse the file.
        /// </summary>
        [DataMember(Name = "big_file_unique_id")]
        public string BigFileUniqueId { get; set; }
    }
}