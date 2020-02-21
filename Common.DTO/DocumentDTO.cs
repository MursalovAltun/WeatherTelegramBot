using System.Runtime.Serialization;

namespace Common.DTO
{
    /// <summary>
    /// This object represents a general file (as opposed to photos, voice messages and audio files).
    /// </summary>
    public class DocumentDTO
    {
        /// <summary>
        /// Identifier for this file, which can be used to download or reuse the file
        /// </summary>
        [DataMember(Name = "file_id")]
        public string FileId { get; set; }

        /// <summary>
        /// Unique identifier for this file, which is supposed to be the same
        /// over time and for different bots.
        /// Can't be used to download or reuse the file.
        /// </summary>
        [DataMember(Name = "file_unique_id")]
        public string FileUniqueId { get; set; }

        /// <summary>
        /// Optional
        /// Document thumbnail as defined by sender
        /// </summary>
        [DataMember(Name = "thumb")]
        public PhotoSizeDTO Thumb { get; set; }


    }
}