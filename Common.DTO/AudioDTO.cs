using System.Runtime.Serialization;

namespace Common.DTO
{
    /// <summary>
    /// This object represents an audio file to be treated as music by the Telegram clients.
    /// </summary>
    public class AudioDTO
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
        /// 
        /// </summary>
        [DataMember(Name = "duration")]
        public int Duration { get; set; }
    }
}